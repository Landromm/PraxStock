using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel;
public class ItemStock : DialogViewModel
{
	public int IdDataStock { get; set; }

	public int IdItem { get; set; }

	public string? Name { get; set; }

	public string? UnitMeasure { get; set; }

	public double UnitCount { get; set; }

	public DateOnly? ExpirationDate { get; set; }

}
