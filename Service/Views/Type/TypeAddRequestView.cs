using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Type
{
    public class TypeAddRequestView : BaseRequestView
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = null!;
        public Models.Type ToObj(bool withId = false){
            var obj = new Models.Type(){
                Name = this.Name,
            };
            if (withId) obj.Id = this.Id;
            return obj;
        }
    }
}