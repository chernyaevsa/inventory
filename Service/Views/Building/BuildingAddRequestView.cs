using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class BuildingAddRequestView : BaseRequestView
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}