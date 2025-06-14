namespace ShoesStoreApp.BLL.ViewModels.Auth
{
    public class AuthResultVm
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Message { get; set; }
    }
}
