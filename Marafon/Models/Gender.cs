using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Gender
{
    public string Gender1 { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Runner> Runners { get; set; } = new List<Runner>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
