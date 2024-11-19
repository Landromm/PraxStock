using PraxStock.View.Commands;
using PraxStock.View.SecondViews.StatisticsFrame;
using PraxStock.ViewModel.Base;
using PraxStock.ViewModel.SecondViewModel;
using PraxStock.ViewModel.SecondViewModel.StatisticsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PraxStock.ViewModel.SecondViewModel;
internal class StatisticMainViewModel : DialogViewModel
{

	#region ItemsTypeStatisticsList : List<string> - Список типов фрэйма статистики.

	/// <summary>Список типов фрэйма статистики. - поле.</summary>
	private List<string> _ItemsTypeStatisticsList;

	/// <summary>Список типов фрэйма статистики. - свойство.</summary>
	public List<string> ItemsTypeStatisticsList
	{
		get => _ItemsTypeStatisticsList;
		set
		{
			_ItemsTypeStatisticsList = value;
			OnPropertyChanged(nameof(ItemsTypeStatisticsList));
		}
	}
	#endregion

	#region SelectedItemsTypeStatisticsList : string? - Выбранный тип фрэйма статистики из списка.

	/// <summary>Выбранный тип фрэйма статистики из списка. - поле.</summary>
	private string? _SelectedItemsTypeStatisticsList;

	/// <summary>Выбранный тип фрэйма статистики из списка. - свойство.</summary>
	public string? SelectedItemsTypeStatisticsList
	{
		get => _SelectedItemsTypeStatisticsList;
		set
		{
			_SelectedItemsTypeStatisticsList = value;
			OnPropertyChanged(nameof(SelectedItemsTypeStatisticsList));
			ExecutedLoadedFrameCommand();
		}
	}
	#endregion

	#region CurrentChildView : DialogViewModel - Дочернее представление (Фрэйм)

	/// <summary>Дочернее представление (Фрэйм) - поле.</summary>
	private ViewModel.Base.ViewModel _CurrentChildView;

	/// <summary>Дочернее представление (Фрэйм) - свойство.</summary>
	public ViewModel.Base.ViewModel CurrentChildView
	{
		get => _CurrentChildView;
		set
		{
			_CurrentChildView = value;
			OnPropertyChanged(nameof(CurrentChildView));
		}
	}
	#endregion


	public StatisticMainViewModel()
	{
		ItemsTypeStatisticsList = ["Расходная", "Рассчетная", "Графическая"];
		SelectedItemsTypeStatisticsList = "Расходная";
	}


	#region Command LoadedFrameCommand - Загрузка выбранного фрэйма.

	/// <summary>Загрузка выбранного фрэйма.</summary>
	private LambdaCommand? _LoadedFrameCommand;

	/// <summary>Загрузка выбранного фрэйма.</summary>
	public ICommand LoadedFrameCommand => _LoadedFrameCommand ??= new(ExecutedLoadedFrameCommand);

	/// <summary>Логика выполнения - Загрузка выбранного фрэйма.</summary>
	private void ExecutedLoadedFrameCommand()
	{
		switch(SelectedItemsTypeStatisticsList)
		{
			case "Расходная": 
				{ 
					CurrentChildView = new ExpenseStatisticsViewModel();
					break; 
				};
			case "Рассчетная": 
				{ 
					CurrentChildView = new CalculationStatisticsViewModel();
					break; 
				};
			case "Графическая": 
				{ 
					CurrentChildView = new GraphicalStatisticsViewModel();
					break; 
				};
		}
	}
	#endregion



}
