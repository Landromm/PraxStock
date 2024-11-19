using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel.StatisticsModel;
internal class ExpenseStatisticModel 
{
	public required string NamePosition { get; set; }
	public required string UnitMeasure { get; set; }
	public double ReceiptSumm { get => resultReceiptSumm();}
	public double MoveInPostSumm { get => resultMoveInPostSumm();}

	public List<ReceiptListItem>? ReceiptListItems { get; set; }
	public List<MoveListItem>? MoveListItems { get; set; }

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
