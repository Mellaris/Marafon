using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Runner
{
    public int Runnerid { get; set; }

    public string Email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime? Dateofbirth { get; set; }

    public string Countrycode { get; set; } = null!;

    public virtual Country CountrycodeNavigation { get; set; } = null!;

    public virtual User EmailNavigation { get; set; } = null!;

    public virtual Gender GenderNavigation { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
