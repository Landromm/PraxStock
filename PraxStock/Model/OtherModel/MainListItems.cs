using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel;
internal class MainListItems : ItemStock
{
	public int IdItem { get; set; }

	//public string? NameItem { get; set; }

	//public string? UnitMeasure { get; set; }

	//public double? RemainingStock { get; set; }

	//public DateOnly ExpirationDate { get; set; }

	public DateOnly DateReceipt { get; set; }
}
