using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Responsible
{
    public class ResponsibleAddRequestView : BaseRequestView
    {
        public int Id { get; set; } = 0;
        public int EmployeeId { get; set; }

        public int CabinetId { get; set; }

        public DateTime Datetime { get; set; }
        public Models.Responsible ToObj(bool withId = false){
            var obj = new Models.Responsible(){
                EmployeeId = this.EmployeeId,
                CabinetId = this.CabinetId,
                Datetime = this.Datetime
            };
            if (withId) obj.Id = this.Id;
            return obj;
        }
    }
}