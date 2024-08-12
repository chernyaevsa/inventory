using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Cabinet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int BuildingId { get; set; }

    public virtual Building Building { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual ICollection<Responsible> Responsibles { get; set; } = new List<Responsible>();
}
