using AuthService.Views.BaseViews;

namespace AuthService.Views.UserControllerViews
{
    public class ExpireTokenRequestView : BaseRequestView
    {
        public string Token { get; set; } = "";

    }
}