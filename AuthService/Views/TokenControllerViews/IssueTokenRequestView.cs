using AuthService.Views.BaseViews;

namespace AuthService.Views.UserControllerViews
{
    public class IssueTokenRequestView : BaseRequestView
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

    }
}