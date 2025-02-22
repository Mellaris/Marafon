using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Country
{
    public string Countrycode { get; set; } = null!;

    public string Countryname { get; set; } = null!;

    public string Countryflag { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Marathon> Marathons { get; set; } = new List<Marathon>();

    public virtual ICollection<Runner> Runners { get; set; } = new List<Runner>();

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
