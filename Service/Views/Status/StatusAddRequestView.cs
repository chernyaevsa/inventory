using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Status
{
    public class StatusAddRequestView : BaseRequestView
    {
        public int Id { get; set; } = 0;
        public int EquipmentId { get; set; }

        public int Status1 { get; set; }

        public string Description { get; set; } = null!;
        public Models.Status ToObj(){
            var obj = new Models.Status(){
                EquipmentId = this.EquipmentId,
                Status1 = this.Status1,
                Description = this.Description
            };
            return obj;
        }
        public void Edit(ref Models.Status status){
            status.EquipmentId = this.EquipmentId;
            status.Status1 = this.Status1;
            status.Description = this.Description;
        }
    }
}