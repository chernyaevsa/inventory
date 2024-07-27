using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace AuthService.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    [XmlIgnore]
    [JsonIgnore]
    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public sbyte IsBlocked { get; set; }
    
    [XmlIgnore]
    [JsonIgnore]
    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
}
