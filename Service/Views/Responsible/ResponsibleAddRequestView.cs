using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class ResponsibleAddRequestView : BaseRequestView
    {
        public int EmployeeId { get; set; }

        public int CabinetId { get; set; }

        public DateTime Datetime { get; set; }
    }
}