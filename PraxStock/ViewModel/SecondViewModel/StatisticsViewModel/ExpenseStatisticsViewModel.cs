using PraxStock.Communication.Repositories;
using PraxStock.Model.OtherModel.StatisticsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.ViewModel.SecondViewModel.StatisticsViewModel;
internal class ExpenseStatisticsViewModel : ViewModel.Base.ViewModel
{
	private readonly IAdminRepositories _repositoriesDB = null!;

	#region StartDateStatistic : DateOnly - Начальная дата формирования отчета.

	/// <summary>Начальная дата формирования отчета. - поле.</summary>
	private DateOnly _StartDateStatistic;

	/// <summary>Начальная дата формирования отчета. - свойство.</summary>
	public DateOnly StartDateStatistic
	{
		get => _StartDateStatistic;
		set
		{
			_StartDateStatistic = value;
			OnPropertyChanged(nameof(StartDateStatistic));
		}
	}
	#endregion

	#region EndDateStatistic : DateOnly - Конечная дата формирования отчета.

	/// <summary>Конечная дата формирования отчета. - поле.</summary>
	private DateOnly _EndDateStatistic;

	/// <summary>Конечная дата формирования отчета. - свойство.</summary>
	public DateOnly EndDateStatistic
	{
		get => _EndDateStatistic;
		set
		{
			_EndDateStatistic = value;
			OnPropertyChanged(nameof(EndDateStatistic));
		}
	}
	#endregion

	#region StatisticMainCollection : ObservableCollection<ExpenseStatisticModel> - Основная расходная коллекция.

	/// <summary>Основная расходная коллекция. - поле.</summary>
	private ObservableCollection<ExpenseStatisticModel> _StatisticMainCollection;

	/// <summary>Основная расходная коллекция. - свойство.</summary>
	public ObservableCollection<ExpenseStatisticModel> StatisticMainCollection
	{
		get => _StatisticMainCollection ?? (_StatisticMainCollection = new ObservableCollection<ExpenseStatisticModel>());
		set
		{
			_StatisticMainCollection = value;
			OnPropertyChanged(nameof(StatisticMainCollection));
		}
	}
    #endregion

    public ExpenseStatisticsViewModel()
    {
		_repositoriesDB = new AdminRepositories();

		StatisticMainCollection = [];

		GetAllStatisticData();
	}

	private void GetAllStatisticData()
	{
		DateOnly start = DateOnly.Parse("2023-09-01");
		DateOnly end = DateOnly.Parse("2025-11-01");
		StatisticMainCollection = _repositoriesDB.GetExpenseStatisticModels(start, end);
	}
}
