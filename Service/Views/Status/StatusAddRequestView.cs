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
        public Models.Status ToObj(bool withId = false){
            var obj = new Models.Status(){
                EquipmentId = this.EquipmentId,
                Status1 = this.Status1,
                Description = this.Description
            };
            if (withId) obj.Id = this.Id;
            return obj;
        }
    }
}