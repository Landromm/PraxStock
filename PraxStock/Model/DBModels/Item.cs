using System;
using System.Collections.Generic;

namespace PraxStock.Model.DBModels;

public partial class Item
{
    public int IdItem { get; set; }

    public string NameItem { get; set; } = null!;

    public string UnitMeasure { get; set; } = null!;

    public virtual ICollection<DataStock> DataStocks { get; set; } = new List<DataStock>();

    public virtual ICollection<MoveInPost> MoveInPosts { get; set; } = new List<MoveInPost>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual ICollection<WriteOff> WriteOffs { get; set; } = new List<WriteOff>();
}
