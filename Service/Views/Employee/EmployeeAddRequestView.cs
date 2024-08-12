using Service.Models;
using Service.Views.BaseViews;

namespace Service.Views.Building
{
    public class EmployeeAddRequestView : BaseRequestView
    {

        public string Surname { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Patronymic { get; set; } = null!;
    }
}