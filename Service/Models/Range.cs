using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class Range
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DatetimeFrom { get; set; }

    public DateTime DatetimeTo { get; set; }
}
