using AuthService.Views.BaseViews;

namespace AuthService.Views.UserControllerViews
{
    public class GetTokenRequestView : BaseRequestView
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

    }
}