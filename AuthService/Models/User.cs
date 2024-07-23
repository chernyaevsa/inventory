using System;
using System.Collections.Generic;

namespace AuthService.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public sbyte IsBlocked { get; set; }

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
}
