using PraxStock.Communication.Repositories;
using PraxStock.Model.OtherModel.StatisticsModel;
using PraxStock.Services;
using PraxStock.View.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PraxStock.ViewModel.SecondViewModel.StatisticsViewModel;
internal class ExpenseStatisticsViewModel : ViewModel.Base.ViewModel
{
	private readonly IAdminRepositories _repositoriesDB = null!;
	private IReportExcel _reportExcel = null!;

	#region StartDateStatistic : DateOnly - Начальная дата формирования отчета.

	/// <summary>Начальная дата формирования отчета. - поле.</summary>
	private DateTime _StartDateStatistic;

	/// <summary>Начальная дата формирования отчета. - свойство.</summary>
	public DateTime StartDateStatistic
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
	private DateTime _EndDateStatistic;

	/// <summary>Конечная дата формирования отчета. - свойство.</summary>
	public DateTime EndDateStatistic
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

	#region SelectedStatisticMainCollection : ExpenseStatisticModel - Выбранный элемент основной коллекции.

	/// <summary>Выбранный элемент основной коллекции. - поле.</summary>
	private ExpenseStatisticModel _SelectedStatisticMainCollection;

	/// <summary>Выбранный элемент основной коллекции. - свойство.</summary>
	public ExpenseStatisticModel SelectedStatisticMainCollection
	{
		get => _SelectedStatisticMainCollection;
		set
		{
			_SelectedStatisticMainCollection = value;
			OnPropertyChanged(nameof(SelectedStatisticMainCollection));
		}
	}
	#endregion


	public ExpenseStatisticsViewModel()
	{
		_repositoriesDB = new AdminRepositories();
		StatisticMainCollection = [];
		GetAllStatisticData();
	}


	#region Command GenerationStatisticsCommand - Генерация и отображение статистики по заданным датам.

	/// <summary>Генерация и отображение статистики по заданным датам.</summary>
	private LambdaCommand? _GenerationStatisticsCommand;

	/// <summary>Генерация и отображение статистики по заданным датам.</summary>
	public ICommand GenerationStatisticsCommand => _GenerationStatisticsCommand ??= new(ExecutedGenerationStatisticsCommand);

	/// <summary>Логика выполнения - Генерация и отображение статистики по заданным датам.</summary>
	private void ExecutedGenerationStatisticsCommand()
	{
		var tempResultCollection = new ObservableCollection<ExpenseStatisticModel>();
		var tempCollection= _repositoriesDB.GetExpenseStatisticModels(DateOnly.FromDateTime(StartDateStatistic), DateOnly.FromDateTime(EndDateStatistic));
		
		foreach (var model in tempCollection)
			if(model.MoveInPostSumm != 0)
				tempResultCollection.Add(model);
		StatisticMainCollection = tempResultCollection;
	}
	#endregion

	#region Command GenerationReportCommand - Экспорт текущих данных в файл EXCEL с последующим сохранением.

	/// <summary>Экспорт текущих данных в файл EXCEL с последующим сохранением.</summary>
	private LambdaCommand? _GenerationReportCommand;

	/// <summary>Экспорт текущих данных в файл EXCEL с последующим сохранением.</summary>
	public ICommand GenerationReportCommand => _GenerationReportCommand ??= new(ExecutedGenerationReportCommand);

	/// <summary>Логика выполнения - Экспорт текущих данных в файл EXCEL с последующим сохранением.</summary>
	private void ExecutedGenerationReportCommand()
	{
		_reportExcel = new ReportExcel(StartDateStatistic, EndDateStatistic);
		var path = _reportExcel.PathFolderSaveFileMergeDate("Отчет по расходам");
		if(path != null && !path.Equals(""))
		{
			_reportExcel.GenerationReport(StatisticMainCollection);
			_reportExcel.OpenReportFile();
		}
	}
	#endregion


	private void GetAllStatisticData()
	{
		StartDateStatistic = DateTime.Now.AddDays(-30);
		EndDateStatistic = DateTime.Now;
		StatisticMainCollection = _repositoriesDB.GetExpenseStatisticModels(DateOnly.FromDateTime(StartDateStatistic), DateOnly.FromDateTime(EndDateStatistic));
	}
}
