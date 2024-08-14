using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class ResponsibleAddRequestView : BaseRequestView
    {
        public int Id { get; set; } = 0;
        public int EmployeeId { get; set; }

        public int CabinetId { get; set; }

        public DateTime Datetime { get; set; }
    }
}