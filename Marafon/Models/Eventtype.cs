using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Eventtype
{
    public string Eventtypeid { get; set; } = null!;

    public string Eventtypename { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
