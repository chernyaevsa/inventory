using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Status
{
    public int Id { get; set; }

    public int EquipmentId { get; set; }

    public int Status1 { get; set; }

    public string Description { get; set; } = null!;

    public string Datetime { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public virtual Equipment Equipment { get; set; } = null!;
}
