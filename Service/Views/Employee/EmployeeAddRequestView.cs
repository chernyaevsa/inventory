using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Employee
{
    public class EmployeeAddRequestView : BaseRequestView
    {

        public int Id { get; set; } = 0;
        public string Surname { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Patronymic { get; set; } = null!;
        public Models.Employee ToObj(bool withId = false){
            var obj = new Models.Employee(){
                Name = this.Name,
                Surname = this.Surname,
                Patronymic = this.Patronymic
            };
            if (withId) obj.Id = this.Id;
            return obj;
        }
    }
}