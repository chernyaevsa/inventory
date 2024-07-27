using AuthService.Views.BaseViews;

namespace AuthService.Views.UserControllerViews
{
    public class GetTokenRequestView : BaseRequestView
    {
        public string Token { get; set; } = "";
    }
}