using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Volunteer
{
    public int Volunteerid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string Countrycode { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public virtual Country CountrycodeNavigation { get; set; } = null!;

    public virtual Gender GenderNavigation { get; set; } = null!;
}
