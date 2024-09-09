using System;
using System.Collections.Generic;

namespace PraxStock.Model.DBModels;

public partial class WriteOff
{
    public int IdWriteOff { get; set; }

    public int IdItem { get; set; }

    public double QuantityWriteOff { get; set; }

    public string DateWriteOff { get; set; } = null!;

    public virtual Item IdItemNavigation { get; set; } = null!;
}
