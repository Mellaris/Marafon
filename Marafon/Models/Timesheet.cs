using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Timesheet
{
    public int Timesheetid { get; set; }

    public int Staffid { get; set; }

    public DateTime? Startdatetime { get; set; }

    public DateTime? Enddatetime { get; set; }

    public decimal? Payamount { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
