using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Equipment
{
    public class EquipmentAddRequestView : BaseRequestView
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = null!;

        public string Number { get; set; } = null!;

        public int Count { get; set; }

        public float Price { get; set; }

        public int CabinetId { get; set; }

        public string? _1cKod { get; set; }

        public int TypeId { get; set; }

        public Models.Equipment ToObj(bool withId = false){
            var obj = new Models.Equipment(){
                Name = this.Name,
                Number = this.Number,
                Count = this.Count,
                Price = this.Price,
                CabinetId = this.CabinetId,
                _1cKod = this._1cKod,
                TypeId = this.TypeId
            };
            if (withId) obj.Id = this.Id;
            return obj;
        }
    }
}