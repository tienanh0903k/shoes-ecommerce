using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.DAL.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
{
    // Sử dụng Guid cứng
    var adminRoleId = new Guid("11111111-1111-1111-1111-111111111111");
    var userRoleId  = new Guid("22222222-2222-2222-2222-222222222222");

    modelBuilder.Entity<Role>().HasData(
        new Role
        {
            Id = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new Role
        {
            Id = userRoleId,
            Name = "User",
            NormalizedName = "USER"
        }
    );

    var adminId = new Guid("33333333-3333-3333-3333-333333333333");
    var passwordHasher = new PasswordHasher<User>();

    var adminUser = new User
    {
        Id = adminId,
        UserName = "admin",
        NormalizedUserName = "ADMIN",
        Email = "admin@gmail.com",
        NormalizedEmail = "ADMIN@GMAIL.COM",
        EmailConfirmed = true,
        RoleId = adminRoleId,
        Address = "Default Address",
        FullName = "Administrator"
    };

    adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");
    modelBuilder.Entity<User>().HasData(adminUser);
}

    }
}
