using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class StatusAddRequestView : BaseRequestView
    {
        public int EquipmentId { get; set; }

        public int Status1 { get; set; }

        public string Description { get; set; } = null!;
    }
}