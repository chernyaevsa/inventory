using AuthService.Views.BaseViews;

namespace AuthService.Views.UserControllerViews
{
    public class CheckTokenRequestView : BaseRequestView
    {
        public string Token { get; set; } = "";
    }
}