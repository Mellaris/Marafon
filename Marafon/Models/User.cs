using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class User
{
    public string Email { get; set; } = null!;

    public string Userpassword { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public char Roleid { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Runner> Runners { get; set; } = new List<Runner>();

    public string FullName => $"{Firstname} {Lastname}";
}
