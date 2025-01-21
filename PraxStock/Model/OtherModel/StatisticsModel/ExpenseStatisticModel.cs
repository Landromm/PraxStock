using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel.StatisticsModel;
internal class ExpenseStatisticModel : DialogViewModel
{
	public required string NamePosition { get; set; }
	public required string UnitMeasure { get; set; }

	#region ReceiptSumm : double - Сумма поступившей позиции.

	/// <summary>Сумма поступившей позиции. - поле.</summary>
	private double _ReceiptSumm;

	/// <summary>Сумма поступившей позиции. - свойство.</summary>
	public double ReceiptSumm
	{
		get => _ReceiptSumm;
		set
		{
			_ReceiptSumm = value;
			OnPropertyChanged(nameof(ReceiptSumm));
		}
	}
	#endregion

	#region MoveInPostSumm : double - Сумма перемещенной позиции.

	/// <summary>Сумма перемещенной позиции. - поле.</summary>
	private double _MoveInPostSumm;

	/// <summary>Сумма перемещенной позиции. - свойство.</summary>
	public double MoveInPostSumm
	{
		get => _MoveInPostSumm;
		set
		{
			_MoveInPostSumm = value;
			OnPropertyChanged(nameof(MoveInPostSumm));
		}
	}
	#endregion


	#region ReceiptListItems : List<ReceiptListItem> - Список поступлений позиции.

	/// <summary>Список поступлений позиции. - поле.</summary>
	private List<ReceiptListItem> _ReceiptListItems;

	/// <summary>Список поступлений позиции. - свойство.</summary>
	public List<ReceiptListItem> ReceiptListItems
	{
		get => _ReceiptListItems ?? (_ReceiptListItems = new List<ReceiptListItem>());
		set
		{
			_ReceiptListItems = value;
			OnPropertyChanged(nameof(ReceiptListItems));
			ReceiptSumm = resultReceiptSumm();
		}
	}
	#endregion

	#region MoveListItems : List<MoveListItem> - Список перемещений позиции.

	/// <summary>Список перемещений позиции. - поле.</summary>
	private List<MoveListItem> _MoveListItems;

	/// <summary>Список перемещений позиции. - свойство.</summary>
	public List<MoveListItem> MoveListItems
	{
		get => _MoveListItems ?? (_MoveListItems = new List<MoveListItem>());
		set
		{
			_MoveListItems = value;
			OnPropertyChanged(nameof(MoveListItems));
			MoveInPostSumm = resultMoveInPostSumm();
		}
	}
	#endregion


	//public List<ReceiptListItem>? ReceiptListItems { get; set; }
	//public List<MoveListItem>? MoveListItems { get; set; }

	private double resultReceiptSumm()
	{
		double resultSumm = 0;
		if (ReceiptListItems is not null && ReceiptListItems.Count > 0)
		{
			foreach(var item in ReceiptListItems)
				resultSumm += item.UnitCount;
		}
		return resultSumm;
	}
	private double resultMoveInPostSumm()
	{
		double resultSumm = 0;
		if (MoveListItems is not null && MoveListItems.Count > 0)
		{
			foreach (var item in MoveListItems)
				resultSumm += item.UnitCount;
		}
		return resultSumm;
	}
}
