using Microsoft.AspNetCore.Identity;

namespace ShoesStoreApp.DAL.Models
{
    public class User:IdentityUser<Guid>
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public bool Status { get; set; } = true;

        public Cart Cart { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Order> Orders { get; set; }
    }
}
