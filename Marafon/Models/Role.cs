using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Role
{
    public char Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
