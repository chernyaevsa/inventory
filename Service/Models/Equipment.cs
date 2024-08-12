using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Equipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Number { get; set; } = null!;

    public int Count { get; set; }

    public float Price { get; set; }

    public int CabinetId { get; set; }

    public string? _1cKod { get; set; }

    public int TypeId { get; set; }

    public virtual Cabinet Cabinet { get; set; } = null!;

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();

    public virtual Type Type { get; set; } = null!;
}
