using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.ViewModel.SecondViewModel.StatisticsViewModel;
internal class DataStocksStatisticsViewModel : DialogViewModel
{

	#region SelectedAll : bool - Статус выделения всех элементов перечня.

	/// <summary>Статус выделения всех элементов перечня. - поле.</summary>
	private bool _SelectedAll;

	/// <summary>Статус выделения всех элементов перечня. - свойство.</summary>
	public bool SelectedAll
	{
		get => _SelectedAll;
		set
		{
			_SelectedAll = value;
			OnPropertyChanged(nameof(SelectedAll));
		}
	}
    #endregion

    public DataStocksStatisticsViewModel()
    {
		SelectedAll = true;
    }

}
