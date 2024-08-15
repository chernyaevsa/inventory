using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Building
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Cabinet> Cabinets { get; set; } = new List<Cabinet>();
}
