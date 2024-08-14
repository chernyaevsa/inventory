using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class RangeAddRequestView : BaseRequestView
    {

        public int Id { get; set; } = 0;
        public string Name { get; set; } = null!;

        public DateTime DatetimeFrom { get; set; }

        public DateTime DatetimeTo { get; set; }
    }
}