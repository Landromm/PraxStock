using PraxStock.Communication.Repositories;
using PraxStock.Model.Message;
using PraxStock.Model.OtherModel;
using PraxStock.Services;
using PraxStock.Services.Implementations;
using PraxStock.View.Commands;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PraxStock.ViewModel.MainViewModel;
internal class MainViewModel : DialogViewModel
{
	private readonly IUserDialog _userDialog = null!;
	private readonly IMessageBus _messageBus = null!;
	private readonly IDisposable _subscription = null!; 
	private readonly IAdminRepositories _repositoriesDB = null!;


	#region ItemCBMainList : List<string> -  Перечень параметров поиска основного списка позиций.

	///<summary>Перечень параметров поиска основного списка позиций. - поле.</summary>
	private List<string> _ItemCBMainList;

	///<summary>Перечень параметров поиска основного списка позиций. - свойство.</summary>
	public List<string> ItemCBMainList
	{
		get => _ItemCBMainList;
		set
		{
			_ItemCBMainList = value;
			OnPropertyChanged(nameof(ItemCBMainList));
		}
	}
	#endregion

	#region SelectedSearchMainList : string -  Текущий выбор из перечня параметров поиска по основному списку.

	///<summary>Текущий выбор из перечня параметров поиска по основному списку. - поле.</summary>
	private string _SelectedSearchMainList;

	///<summary>Текущий выбор из перечня параметров поиска по основному списку. - свойство.</summary>
	public string SelectedSearchMainList
	{
		get => _SelectedSearchMainList;
		set
		{
			_SelectedSearchMainList = value;
			OnPropertyChanged(nameof(SelectedSearchMainList));
		}
	}
	#endregion

	#region SearchMainList : string -  Поле строки поиска по основному перечню позиций.

	///<summary>Поле строки поиска по основному перечню позиций. - поле.</summary>
	private string _SearchMainList;

	///<summary>Поле строки поиска по основному перечню позиций. - свойство.</summary>
	public string SearchMainList
	{
		get => _SearchMainList;
		set
		{
			_SearchMainList = value;
			OnPropertyChanged(nameof(SearchMainList));
			ExecuteShowListCatalogAfterSearch();
		}
	}
	#endregion

	#region ObservableCollection<MainListItems> : DataStockList -  Перечень основных позиций.

	///<summary>Перечень основных позиций. - поле.</summary>
	private ObservableCollection<MainListItems> _DataStockList;

	///<summary>Перечень основных позиций. - свойство.</summary>
	public ObservableCollection<MainListItems> DataStockList
	{
		get => _DataStockList ?? (_DataStockList = new ObservableCollection<MainListItems>());
		set
		{
			_DataStockList = value;
			OnPropertyChanged(nameof(DataStockList));
		}
	}
	#endregion

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

	#region ObservableCollection<MoveListItem> : MoveList -  Перечень перемещений позиций.

	///<summary>Перечень перемещений позиций. - поле.</summary>
	private ObservableCollection<MoveListItem> _MoveList;

	///<summary>Перечень перемещений позиций. - свойство.</summary>
	public ObservableCollection<MoveListItem> MoveList
	{
		get => _MoveList ?? (_MoveList = new ObservableCollection<MoveListItem>());
		set
		{
			_MoveList = value;
			OnPropertyChanged(nameof(MoveList));
		}
	}
	#endregion

	#region CurrentDataStockList : MainListItems - Текущий выбор позициив перечне.

	/// <summary>Текущий выбор позициив перечне. - поле.</summary>
	private MainListItems _CurrentDataStockList;

	/// <summary>Текущий выбор позициив перечне. - свойство.</summary>
	public MainListItems CurrentDataStockList
	{
		get => _CurrentDataStockList;
		set
		{
			_CurrentDataStockList = value;
			OnPropertyChanged(nameof(CurrentDataStockList));
		}
	}
	#endregion



	public MainViewModel(IUserDialog userDialog, IMessageBus MessageBus)
	{
		_userDialog = userDialog;
		_messageBus = MessageBus;
		_repositoriesDB = new AdminRepositories();
		
		ItemCBMainList = new List<string>
		{
			"Наименование",
			"Остаток",
			"Срок годности",
			"Дата поступления"
		};
		SelectedSearchMainList = "Наименование";
		Inicialization();
	}

	private void Inicialization()
	{
		DataStockList = _repositoriesDB.GetDataStockList();
		ReceiptList = _repositoriesDB.GetReseiptList();
		MoveList = _repositoriesDB.GetMoveInPostList();
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
		_userDialog.OpenItemsListWindow();
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
		_userDialog.OpenAddReceiptWindow();
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
		if(CurrentDataStockList is null)
		{
			MessageBox.Show("Выберите позицию из списка, чтобы открылось окно перемещения позиций.",
				"Совет!",
				MessageBoxButton.OK,
				MessageBoxImage.Exclamation);
		}
		else
		{
			_userDialog.OpenMoveAddWindow();
			_messageBus.Send(new CurrentlyMainItemList(CurrentDataStockList));
		}
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
		MoveList.Clear();
		DataStockList = _repositoriesDB.GetDataStockList();
		ReceiptList = _repositoriesDB.GetReseiptList();
		MoveList = _repositoriesDB.GetMoveInPostList();
	}
	#endregion
	#endregion

	/// <summary>
	/// Метод выборки основного перечня позиций по введенным данным.
	/// </summary>
	private void ExecuteShowListCatalogAfterSearch()
	{
		DataStockList = [];
		switch (SelectedSearchMainList)
		{
			case "Наименование" :
				{
					var tempMainList = _repositoriesDB.GetBySearchNameItemMainList(SearchMainList);
					foreach (var item in tempMainList)
						DataStockList.Add(item);
					tempMainList = null;
					break;
				}
			case "Остаток":
				{
					var tempMainList = _repositoriesDB.GetBySearchRemainingStockMainList(SearchMainList);
					foreach (var item in tempMainList)
						DataStockList.Add(item);
					tempMainList = null;
					break;
				}
			case "Срок годности":
				{
					break;
				}
			case "Дата поступления":
				{
					break;
				}
		}
	}

}