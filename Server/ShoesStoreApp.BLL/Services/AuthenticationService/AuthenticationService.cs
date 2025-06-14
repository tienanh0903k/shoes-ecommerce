using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoesStoreApp.BLL.ViewModels.Auth;
using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoesStoreApp.BLL.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ShoesStoreAppDbContext _context;

        public AuthenticationService(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration,
                                     ShoesStoreAppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> RegisterUserAsync(RegisterVm registerVm)
        {
            var userExists = await _userManager.FindByEmailAsync(registerVm.Email);
            if (userExists != null)
            {
                throw new Exception($"User {registerVm.Email} already exists!");
            }

            var role = await _roleManager.FindByNameAsync("User");

            var newUser = new User
            {
                RoleId = role.Id,
                FullName = registerVm.FullName,
                Email = registerVm.Email,
                UserName = registerVm.Email,
                Cart = new Cart(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (result.Succeeded)
            {
                return $"User {registerVm.Email} created!";
            }

            throw new Exception("User could not be created!");
        }

        public async Task<AuthResultVm> LoginUserAsync(LoginVm loginVm)
        {
            var user = await _userManager.FindByEmailAsync(loginVm.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginVm.Password))
            {
                throw new Exception("Invalid login credentials!");
            }

            return await GenerateJwtTokenAsync(user);
        }


        public async Task<AuthResultVm> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _context.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedToken == null || storedToken.IsRevoked || storedToken.DateExpire <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token");
            }

            var user = await _userManager.FindByIdAsync(storedToken.UserId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            // Revoke old token
            storedToken.IsRevoked = true;
            _context.RefreshToken.Update(storedToken);

            var jwtResult = await GenerateJwtTokenAsync(user);
            await _context.SaveChangesAsync();

            return jwtResult;

        }

        private async Task<AuthResultVm> GenerateJwtTokenAsync(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roleName = await _context.Roles
                                .Where(r => r.Id == user.RoleId)
                                .Select(r => r.Name)
                                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(roleName))
            {
                authClaims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddDays(7)

            };

            await _context.RefreshToken.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResultVm
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };
        }

        public async Task<UserVm> GetUserInfoAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return new UserVm
            {
                Id = user.Id,
                FullName = user.FullName,
                Avatar = user.Avatar,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };
        }

        public async Task<bool> UpdateUserInfoAsync(Guid userId, UpdateUserVm updateUserVm)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.FullName = updateUserVm.FullName;
            user.PhoneNumber = updateUserVm.PhoneNumber;
            user.Address = updateUserVm.Address;
            user.Avatar = updateUserVm.Avatar;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }


        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordVm passwordVm)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var isOldPasswordValid = await _userManager.CheckPasswordAsync(user, passwordVm.OldPassword);
            if (!isOldPasswordValid)
            {
                throw new Exception("Old password is incorrect.");
            }

            var result = await _userManager.ChangePasswordAsync(user, passwordVm.OldPassword, passwordVm.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to change password: {errors}");
            }

            return result.Succeeded;
        }
    }
}
