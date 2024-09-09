using PraxStock.Model.OtherModel;
using PraxStock.Services;
using PraxStock.View.Commands;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PraxStock.ViewModel.MainViewModel;
public class MainViewModel : DialogViewModel
{
	private readonly IUserDialog _userDialog = null!;

	private ObservableCollection<MainListItems> _dataStockList;
	public ObservableCollection<MainListItems> DataStockList
	{
		get => _dataStockList ?? (_dataStockList = new ObservableCollection<MainListItems>());
		set
		{
			_dataStockList = value;
			OnPropertyChanged(nameof(DataStockList));
		}
	}


	public MainViewModel(IUserDialog userDialog)
	{
		_userDialog = userDialog;
		DataStockList =
		[
			new MainListItems()
			{
				IdItem = 1,
				NameItem = "Аспирин",
				UnitMeasure = "уп.",
				RemainingStock = 100.5,
				ExpirationDate = new DateOnly(2024, 12, 31),
				DateReceipt = new DateOnly(2024, 06, 05)
			},
			new MainListItems()
			{
				IdItem = 2,
				NameItem = "Перчатки",
				UnitMeasure = "уп.",
				RemainingStock = 5.5,
				ExpirationDate = new DateOnly(2026, 12, 31),
				DateReceipt = new DateOnly(2024, 06, 05),
				DateMove = new DateOnly(2024, 09, 09)
			},
			new MainListItems()
			{
				IdItem = 3,
				NameItem = "Иглы",
				UnitMeasure = "шт.",
				RemainingStock = 563.0,
				ExpirationDate = new DateOnly(2025, 12, 31),
				DateReceipt = new DateOnly(2024, 08, 01),
				DateMove = new DateOnly(2024, 09, 09)
			},
		];
	}

	#region Command's - Реализация комманд.
	#region Command OpenSecondFrameCommand - открытие окна настроек приложения.

	/// <summary>открытие окна настроек приложения.</summary>
	private LambdaCommand? _OpenSecondFrameCommand;

	/// <summary>открытие окна настроек приложения.</summary>
	public ICommand OpenSecondFrameCommand => _OpenSecondFrameCommand ??= new(ExecutedOpenSecondFrameCommand);

	/// <summary>Логика выполнения - открытие окна настроек приложения.</summary>
	private void ExecutedOpenSecondFrameCommand()
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			_userDialog.OpenSettingsWindow();
		});
	}
	#endregion
	#endregion

}