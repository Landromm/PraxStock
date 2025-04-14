using System;
using System.Collections.Generic;

namespace PraxStock.Model.DBModels;

public partial class Receipt
{
    public int IdReceipt { get; set; }

    public int IdItem { get; set; }

    public double QuantityReceipt { get; set; }

    public string? ExpirationDate { get; set; }

    public string DateReceipt { get; set; } = null!;

    public virtual Item IdItemNavigation { get; set; } = null!;

    public virtual DataStock IdReceiptNavigation { get; set; } = null!;
}
