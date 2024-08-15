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
        public Models.Responsible ToObj(){
            var obj = new Models.Responsible(){
                EmployeeId = this.EmployeeId,
                CabinetId = this.CabinetId,
                Datetime = this.Datetime
            };
            return obj;
        }
        public void Edit(ref Models.Responsible responsible){
            responsible.EmployeeId = this.EmployeeId;
            responsible.CabinetId = this.CabinetId;
            responsible.Datetime = this.Datetime;
        }
    }
}