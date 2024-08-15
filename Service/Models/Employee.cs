using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public virtual ICollection<Responsible> Responsibles { get; set; } = new List<Responsible>();
}
