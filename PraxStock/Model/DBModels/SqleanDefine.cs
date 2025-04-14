using System;
using System.Collections.Generic;

namespace PraxStock.Model.DBModels;

public partial class SqleanDefine
{
    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? Body { get; set; }
}
