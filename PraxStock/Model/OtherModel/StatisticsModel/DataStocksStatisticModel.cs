using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel.StatisticsModel;
internal class DataStocksStatisticModel : MainListItems
{

	#region StatusExport : bool - Флаг добавления параметра в объект экспорта.

	/// <summary>Флаг добавления параметра в объект экспорта. - поле.</summary>
	private bool _StatusExport;

	/// <summary>Флаг добавления параметра в объект экспорта. - свойство.</summary>
	public bool StatusExport
	{
		get => _StatusExport;
		set
		{
			_StatusExport = value;
			OnPropertyChanged(nameof(StatusExport));
		}
	}
	#endregion	
}
