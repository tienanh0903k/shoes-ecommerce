namespace ShoesStoreApp.BLL.ViewModels.Auth
{
    public class UserVm
    {
        public Guid Id {get; set; }    
        public string RoleName { get; set; }
        public string FullName { get; set; }
        public string Avatar {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
