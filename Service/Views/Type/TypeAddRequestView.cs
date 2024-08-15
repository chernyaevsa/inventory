using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Type
{
    public class TypeAddRequestView : BaseRequestView
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = null!;
        public Models.Type ToObj(){
            var obj = new Models.Type(){
                Name = this.Name,
            };
            return obj;
        }
        public void Edit(ref Models.Type type){
            type.Name = this.Name;
        }
    }
}