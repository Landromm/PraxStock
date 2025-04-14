using System;
using System.Collections.Generic;

namespace PraxStock.Model.DBModels;

public partial class MoveInPost
{
    public int IdMove { get; set; }

    public int IdItems { get; set; }

    public double QuantityMove { get; set; }

    public string? NamePost { get; set; }

    public string? DateMove { get; set; }

    public int IdItemStock { get; set; }

    public virtual DataStock IdItemStockNavigation { get; set; } = null!;

    public virtual Item IdItemsNavigation { get; set; } = null!;
}
