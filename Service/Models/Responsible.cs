using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Responsible
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int CabinetId { get; set; }

    public DateTime Datetime { get; set; }

    public virtual Cabinet Cabinet { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
