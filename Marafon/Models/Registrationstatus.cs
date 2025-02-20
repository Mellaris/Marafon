using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Registrationstatus
{
    public int Registrationstatusid { get; set; }

    public string Registrationstatus1 { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
