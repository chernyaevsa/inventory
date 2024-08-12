using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class CabinetAddRequestView : BaseRequestView
    {

        public string Name { get; set; } = null!;

        public int BuildingId { get; set; }
    }
}