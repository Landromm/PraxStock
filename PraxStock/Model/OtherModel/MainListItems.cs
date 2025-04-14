using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel;
public class MainListItems : ItemStock
{
	//public string? NameItem { get; set; }

	//public string? UnitMeasure { get; set; }

	//public double? RemainingStock { get; set; }

	//public DateOnly? ExpirationDate { get; set; }

	public DateOnly DateReceipt { get; set; }

	#region MinValue : double? - Значение ограничения.

	/// <summary>Значение ограничения. - поле.</summary>
	private double? _MinValue;

	/// <summary>Значение ограничения. - свойство.</summary>
	public double? MinValue
	{
		get => _MinValue;
		set
		{
			_MinValue = value;
			OnPropertyChanged(nameof(MinValue));
		}
	}
	#endregion

	#region FlagSett : bool? - Флаг установки контроля

	/// <summary>Флаг установки контроля - поле.</summary>
	private bool? _FlagSett;

	/// <summary>Флаг установки контроля - свойство.</summary>
	public bool? FlagSett
	{
		get => _FlagSett;
		set
		{
			_FlagSett = value;
			OnPropertyChanged(nameof(FlagSett));
		}
	}
	#endregion
}
