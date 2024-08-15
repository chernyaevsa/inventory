using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Range
{
    public class RangeAddRequestView : BaseRequestView
    {

        public int Id { get; set; } = 0;
        public string Name { get; set; } = null!;

        public DateTime DatetimeFrom { get; set; }

        public DateTime DatetimeTo { get; set; }
        public Models.Range ToObj(bool withId = false){
            var obj = new Models.Range(){
                Name = this.Name,
                DatetimeFrom = this.DatetimeFrom,
                DatetimeTo = this.DatetimeTo
            };
            if (withId) obj.Id = this.Id;
            return obj;
        }
    }
}