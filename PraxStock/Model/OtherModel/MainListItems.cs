﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel;
public class MainListItems : ItemStock
{
	//public string? NameItem { get; set; }

	//public string? UnitMeasure { get; set; }

	//public double? RemainingStock { get; set; }

	//public DateOnly ExpirationDate { get; set; }

	public DateOnly DateReceipt { get; set; }

	public int MinValue { get; set; }
}
