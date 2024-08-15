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
        public Models.Range ToObj(){
            var obj = new Models.Range(){
                Name = this.Name,
                DatetimeFrom = this.DatetimeFrom,
                DatetimeTo = this.DatetimeTo
            };
            return obj;
        }
        public void Edit(ref Models.Range range){
            range.Name = this.Name;
            range.DatetimeFrom = this.DatetimeFrom;
            range.DatetimeTo = this.DatetimeTo;
        }
    }
}