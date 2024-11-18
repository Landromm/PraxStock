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
			ExecutedTestCommand();
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


	#region Command TestCommand - test

	/// <summary>test</summary>
	private LambdaCommand? _TestCommand;

	/// <summary>test</summary>
	public ICommand TestCommand => _TestCommand ??= new(ExecutedTestCommand);

	/// <summary>Логика выполнения - test</summary>
	private void ExecutedTestCommand()
	{
		CurrentChildView = new CalculationStatisticsViewModel();
	}
	#endregion


	private void OpenChildView()
	{
		
	}
}
