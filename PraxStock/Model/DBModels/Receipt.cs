using System;
using System.Collections.Generic;

namespace PraxStock.Model.DBModels;

public partial class Receipt
{
    public int IdReceipt { get; set; }

    public int IdItem { get; set; }

    public double QuantityReceipt { get; set; }

    public DateOnly? ExprirationDate { get; set; }

    public DateOnly DateReceipt { get; set; }

    public virtual DataStock? DataStock { get; set; }

    public virtual Item IdItemNavigation { get; set; } = null!;
}
