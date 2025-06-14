using ShoesStoreApp.BLL.ViewModels.Payment;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.ViewModels;

public class UserVM
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public bool Status { get; set; }
}