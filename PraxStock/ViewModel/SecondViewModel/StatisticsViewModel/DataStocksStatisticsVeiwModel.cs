using PraxStock.Communication.Repositories;
using PraxStock.Model.OtherModel;
using PraxStock.Model.OtherModel.StatisticsModel;
using PraxStock.Services;
using PraxStock.View.Commands;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PraxStock.ViewModel.SecondViewModel.StatisticsViewModel;
internal class DataStocksStatisticsViewModel : DialogViewModel
{
	private readonly IAdminRepositories _repositoriesDB = null!;
	private IReportExcel _reportExcel = null!;

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
			for(int i = 0; i < DataStocksList.Count; i++)
			{
				DataStocksList[i].StatusExport = SelectedAll == true ? true : false;
			}
		}
	}
	#endregion


	#region ShortCollectionData : ObservableCollection<MainListItems> - Неполная коллекция перечня остатков.

	/// <summary>Неполная коллекция перечня остатков. - поле.</summary>
	private ObservableCollection<MainListItems> _ShortCollectionData;

	/// <summary>Неполная коллекция перечня остатков. - свойство.</summary>
	public ObservableCollection<MainListItems> ShortCollectionData
	{
		get => _ShortCollectionData ?? (_ShortCollectionData = new ObservableCollection<MainListItems>());
		set
		{
			_ShortCollectionData = value;
			OnPropertyChanged(nameof(ShortCollectionData));
		}
	}
	#endregion

	#region DataStocksList : ObservableCollection<DataStocksStatisticModel> - Перечень остатков.

	/// <summary>Перечень остатков. - поле.</summary>
	private ObservableCollection<DataStocksStatisticModel> _DataStocksCollection;

	/// <summary>Перечень остатков. - свойство.</summary>
	public ObservableCollection<DataStocksStatisticModel> DataStocksList
	{
		get => _DataStocksCollection ?? (_DataStocksCollection = new ObservableCollection<DataStocksStatisticModel>());
		set
		{
			_DataStocksCollection = value;
			OnPropertyChanged(nameof(DataStocksList));
		}
	}
	#endregion


	#region Command GenerationReportCommand - Создание отчета остатков.

	/// <summary>Создание отчета остатков.</summary>
	private LambdaCommand? _GenerationReportCommand;

	/// <summary>Создание отчета остатков.</summary>
	public ICommand GenerationReportCommand => _GenerationReportCommand ??= new(ExecutedGenerationReportCommand);

	/// <summary>Логика выполнения - Создание отчета остатков.</summary>
	private void ExecutedGenerationReportCommand()
	{
		ObservableCollection<DataStocksStatisticModel> tempDataStockStatistic = [];
		foreach (var item in DataStocksList)
			if (item.StatusExport == true)
				tempDataStockStatistic.Add(item);

		_reportExcel = new ReportExcel();
		var path = _reportExcel.PathFolderSaveFileNowDate("Отчет по остаткам");
		if (path != null && !path.Equals(""))
		{
			_reportExcel.GenarationReport(tempDataStockStatistic);
			_reportExcel.OpenReportFile();
		}
	}
	#endregion


	public DataStocksStatisticsViewModel()
    {
		_repositoriesDB = new AdminRepositories();
		DataStocksList = [];
		ShortCollectionData = [];
		SelectedAll = true;
		Initialization();
	}

	private void Initialization()
	{
		try
		{
			ShortCollectionData = _repositoriesDB.GetDataStockList();

			foreach(var item in ShortCollectionData)
			{
				DataStocksList.Add(new DataStocksStatisticModel()
				{
					StatusExport = true,
					IdDataStock = item.IdDataStock,
					Name = item.Name,
					UnitMeasure = item.UnitMeasure,
					UnitCount = item.UnitCount,
					ExpirationDate = item.ExpirationDate,
					DateReceipt = item.DateReceipt
				});
			}

		}
		catch (Exception ex)
		{

		}
	}

}
