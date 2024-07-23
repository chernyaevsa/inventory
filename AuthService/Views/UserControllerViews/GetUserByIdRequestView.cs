using AuthService.Views.BaseViews;

namespace AuthService.Views.UserControllerViews
{
    public class GetUserByIdRequestView : BaseRequestView
    {
        public int UserId { get; set; }
    }
}