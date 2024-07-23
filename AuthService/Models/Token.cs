using System;
using System.Collections.Generic;

namespace AuthService.Models;

public partial class Token
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime ExpairDate { get; set; }

    public string Token1 { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
