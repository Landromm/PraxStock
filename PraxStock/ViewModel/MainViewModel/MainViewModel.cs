using PraxStock.Communication.Repositories;
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
internal class MainViewModel : DialogViewModel
{
	private readonly IUserDialog _userDialog = null!;
	private readonly IAdminRepositories _repositoriesDB = null!;

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


	#region ObservableCollection<ReceiptListItem> : ReceiptListItem -  Перечень поступлений.

	///<summary>Перечень поступлений. - поле.</summary>
	private ObservableCollection<ReceiptListItem> _ReceiptList;

	///<summary>Перечень поступлений. - свойство.</summary>
	public ObservableCollection<ReceiptListItem> ReceiptList
	{
		get => _ReceiptList ?? (_ReceiptList = new ObservableCollection<ReceiptListItem>());
		set
		{
			_ReceiptList = value;
			OnPropertyChanged(nameof(ReceiptList));
		}
	}
	#endregion


	public MainViewModel(IUserDialog userDialog)
	{
		_userDialog = userDialog;
		_repositoriesDB = new AdminRepositories();
		Inicialization();
	}

	private void Inicialization()
	{
		DataStockList = _repositoriesDB.GetDataStockList();
		ReceiptList = _repositoriesDB.GetReseiptList();
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

	#region Command OpenItemsListWindowCommand - Открытие окна отображения и редактирования списка основных позиций.

	/// <summary>Открытие окна отображения и редактирования списка основных позиций.</summary>
	private LambdaCommand? _OpenItemsListWindowCommand;

	/// <summary>Открытие окна отображения и редактирования списка основных позиций.</summary>
	public ICommand OpenItemsListWindowCommand => _OpenItemsListWindowCommand ??= new(ExecutedOpenItemsListWindowCommand);

	/// <summary>Логика выполнения - Открытие окна отображения и редактирования списка основных позиций.</summary>
	private void ExecutedOpenItemsListWindowCommand()
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			_userDialog.OpenItemsListWindow();
		});
	}
	#endregion

	#region Command OpenReceiptListWindowCommand - Открытие окна добавления поступления.

	/// <summary>Открытие окна добавления поступления.</summary>
	private LambdaCommand? _OpenReceiptListWindowCommand;

	/// <summary>Открытие окна добавления поступления.</summary>
	public ICommand OpenReceiptListWindowCommand => _OpenReceiptListWindowCommand ??= new(ExecutedOpenReceiptListWindowCommand);

	/// <summary>Логика выполнения - Открытие окна добавления поступления.</summary>
	private void ExecutedOpenReceiptListWindowCommand()
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			_userDialog.OpenAddReceiptWindow();
		});
	}
	#endregion

	#region Command OpenMoveListWindowCommand - Открытие окна добавления перемещения позиции.

	/// <summary>Открытие окна добавления перемещения позиции.</summary>
	private LambdaCommand? _OpenMoveListWindowCommand;

	/// <summary>Открытие окна добавления перемещения позиции.</summary>
	public ICommand OpenMoveListWindowCommand => _OpenMoveListWindowCommand ??= new(ExecutedOpenMoveListWindowCommand);

	/// <summary>Логика выполнения - Открытие окна добавления перемещения позиции.</summary>
	private void ExecutedOpenMoveListWindowCommand()
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			_userDialog.OpenMoveAddWindow();
		});
	}
	#endregion

	#region Command RefreshMainCommand - Обновление всей информации во всех собирательных списках.

	/// <summary>Обновление всей информации во всех собирательных списках.</summary>
	private LambdaCommand? _RefreshMainCommand;

	/// <summary>Обновление всей информации во всех собирательных списках.</summary>
	public ICommand RefreshMainCommand => _RefreshMainCommand ??= new(ExecutedRefreshMainCommand);

	/// <summary>Логика выполнения - Обновление всей информации во всех собирательных списках.</summary>
	private void ExecutedRefreshMainCommand()
	{
		DataStockList.Clear();
		ReceiptList.Clear();
		DataStockList = _repositoriesDB.GetDataStockList();
		ReceiptList = _repositoriesDB.GetReseiptList();
	}
	#endregion
	#endregion

}