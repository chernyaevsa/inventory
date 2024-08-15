using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class BuildingAddRequestView : BaseRequestView
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;
        
        public Models.Building ToObj(){
            var obj = new Models.Building(){
                Name = this.Name,
                Address = this.Address
            };
            return obj;
        }

        public void Edit(ref Models.Building building){
            building.Name = this.Name;
            building.Address = this.Address;
        }
    }
}