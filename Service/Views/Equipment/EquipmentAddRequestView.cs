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

        public Models.Equipment ToObj(){
            var obj = new Models.Equipment(){
                Name = this.Name,
                Number = this.Number,
                Count = this.Count,
                Price = this.Price,
                CabinetId = this.CabinetId,
                _1cKod = this._1cKod,
                TypeId = this.TypeId
            };
            return obj;
        }
        public void Edit(ref Models.Equipment equipment){
            Name = this.Name;
            Number = this.Number;
            Count = this.Count;
            Price = this.Price;
            CabinetId = this.CabinetId;
            _1cKod = this._1cKod;
            TypeId = this.TypeId;
        }
    }
}