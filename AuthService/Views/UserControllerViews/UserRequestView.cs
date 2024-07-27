using AuthService.Views.BaseViews;

namespace AuthService.Views.UserControllerViews
{
    public class UserRequestView : BaseRequestView
    {
        public int UserId { get; set; }
    }
}