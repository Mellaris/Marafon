using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Registration
{
    public int Registrationid { get; set; }

    public int Runnerid { get; set; }

    public DateTime Registrationdatetime { get; set; }

    public char Racekitoptionid { get; set; }

    public int Registrationstatusid { get; set; }

    public decimal Cost { get; set; }

    public int Charityid { get; set; }

    public decimal Sponsorshiptarget { get; set; }

    public virtual Charity Charity { get; set; } = null!;

    public virtual Racekitoption Racekitoption { get; set; } = null!;

    public virtual ICollection<Registrationevent> Registrationevents { get; set; } = new List<Registrationevent>();

    public virtual Registrationstatus Registrationstatus { get; set; } = null!;

    public virtual Runner Runner { get; set; } = null!;

    public virtual ICollection<Sponsorship> Sponsorships { get; set; } = new List<Sponsorship>();
}
