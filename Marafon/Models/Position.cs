using System;
using System.Collections.Generic;

namespace Marafon.Models;

public partial class Position
{
    public int Positionid { get; set; }

    public string Positionname { get; set; } = null!;

    public string? Positiondescription { get; set; }

    public string Payperiod { get; set; } = null!;

    public decimal Payrate { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
