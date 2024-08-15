using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class CabinetAddRequestView : BaseRequestView
    {

        public int Id { get; set; } = 0;
        public string Name { get; set; } = null!;

        public int BuildingId { get; set; }
        public Models.Cabinet ToObj(bool withId = false){
            var obj = new Models.Cabinet(){
                Name = this.Name,
                BuildingId = this.BuildingId
            };
            if (withId) obj.Id = this.Id;
            return obj;
        }
    }
}