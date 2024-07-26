using AuthService.Models;
using AuthService.Views.BaseViews;

namespace AuthService.Views.UserControllerViews
{
    public class UserAddRequestView : BaseRequestView
    {
        public string Name { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
    }
}