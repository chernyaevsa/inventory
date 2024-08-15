using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Cabinet
{
    public class CabinetAddRequestView : BaseRequestView
    {

        public int Id { get; set; } = 0;
        public string Name { get; set; } = null!;

        public int BuildingId { get; set; }
        public Models.Cabinet ToObj(){
            var obj = new Models.Cabinet(){
                Name = this.Name,
                BuildingId = this.BuildingId
            };
            return obj;
        }
        public void Edit(ref Models.Cabinet cabinet){
            cabinet.Name = this.Name;
            cabinet.BuildingId = this.BuildingId;
        }
    }
}