using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class EquipmentAddRequestView : BaseRequestView
    {

        public string Name { get; set; } = null!;

        public string Number { get; set; } = null!;

        public int Count { get; set; }

        public float Price { get; set; }

        public int CabinetId { get; set; }

        public string? _1cKod { get; set; }

        public int TypeId { get; set; }
    }
}