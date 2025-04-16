using System;
using System.Collections.Generic;

namespace PraxStock.Model.DBModels;

public partial class DataStock
{
    public int IdItemStock { get; set; }

    public int IdItem { get; set; }

    public double RemainingStock { get; set; }

    public double? MinValue { get; set; }

    public int? FlagSett { get; set; }

    public virtual Item IdItemNavigation { get; set; } = null!;

    public virtual ICollection<MoveInPost> MoveInPosts { get; set; } = new List<MoveInPost>();
}
