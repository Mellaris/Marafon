using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Sponsorship
{
    public int Sponsorshipid { get; set; }

    public string Sponsorname { get; set; } = null!;

    public int Registrationid { get; set; }

    public decimal Amount { get; set; }

    public virtual Registration Registration { get; set; } = null!;
}
