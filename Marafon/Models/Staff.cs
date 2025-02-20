using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Staff
{
    public int Staffid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dateofbirth { get; set; }

    public string Gender { get; set; } = null!;

    public int Postionid { get; set; }

    public string? Comments { get; set; }

    public virtual Gender GenderNavigation { get; set; } = null!;

    public virtual Position Postion { get; set; } = null!;

    public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
}
