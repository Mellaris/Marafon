using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Charity
{
    public int Charityid { get; set; }

    public string Charityname { get; set; } = null!;

    public string? Charitydescription { get; set; }

    public string? Charitylogo { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
