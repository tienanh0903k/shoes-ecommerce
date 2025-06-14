using Microsoft.AspNetCore.Identity;

namespace ShoesStoreApp.DAL.Models
{
    public class Role:IdentityRole<Guid>
    {
        public List<User> Users { get; set; }
    }
}
