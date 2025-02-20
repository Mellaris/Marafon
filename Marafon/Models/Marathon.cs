using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Marathon
{
    public int Marathonid { get; set; }

    public string Marathonname { get; set; } = null!;

    public string? Cityname { get; set; }

    public string Countrycode { get; set; } = null!;

    public int? Yearheld { get; set; }

    public virtual Country CountrycodeNavigation { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
