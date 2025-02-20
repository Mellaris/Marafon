using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Event
{
    public string Eventid { get; set; } = null!;

    public string Eventname { get; set; } = null!;

    public string Eventtypeid { get; set; } = null!;

    public int Marathonid { get; set; }

    public DateTime? Startdatetime { get; set; }

    public decimal? Cost { get; set; }

    public int? Maxparticipants { get; set; }

    public virtual Eventtype Eventtype { get; set; } = null!;

    public virtual Marathon Marathon { get; set; } = null!;

    public virtual ICollection<Registrationevent> Registrationevents { get; set; } = new List<Registrationevent>();
}
