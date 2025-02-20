using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Registrationevent
{
    public int Registrationeventid { get; set; }

    public int Registrationid { get; set; }

    public string Eventid { get; set; } = null!;

    public short? Bibnumber { get; set; }

    public int? Racetime { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Registration Registration { get; set; } = null!;
}
