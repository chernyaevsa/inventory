using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class TypeAddRequestView : BaseRequestView
    {
        public string Name { get; set; } = null!;
    }
}