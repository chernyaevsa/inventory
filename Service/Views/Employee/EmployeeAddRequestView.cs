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
        public Models.Employee ToObj(){
            var obj = new Models.Employee(){
                Name = this.Name,
                Surname = this.Surname,
                Patronymic = this.Patronymic
            };
            return obj;
        }
        public void Edit(ref Models.Employee employee){
            employee.Name = this.Name;
            employee.Surname = this.Surname;
            employee.Patronymic = this.Patronymic;
        }
    }
}