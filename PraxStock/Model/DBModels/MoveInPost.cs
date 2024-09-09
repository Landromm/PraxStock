using System;
using System.Collections.Generic;

namespace PraxStock.Model.DBModels;

public partial class MoveInPost
{
    public int IdMove { get; set; }

    public int IdItem { get; set; }

    public double QuantityMove { get; set; }

    public DateOnly DateMove { get; set; }

    public virtual Item IdItemNavigation { get; set; } = null!;
}
