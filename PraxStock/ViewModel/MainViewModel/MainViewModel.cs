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

	#region ItemCBReceiptList : List<string> - Перечень параметров поиска списка поступивших позиций.

	/// <summary>Перечень параметров поиска списка поступивших позиций. - поле.</summary>
	private List<string> _ItemCBReceiptList;

	/// <summary>Перечень параметров поиска списка поступивших позиций. - свойство.</summary>
	public List<string> ItemCBReceiptList
	{
		get => _ItemCBReceiptList;
		set
		{
			_ItemCBReceiptList = value;
			OnPropertyChanged(nameof(ItemCBReceiptList));
		}
	}
	#endregion
	#region SelectedSearchReceiptList : string - Текущий выбор из перечня параметров поиска списка поступивших позиций.

	/// <summary>Текущий выбор из перечня параметров поиска списка поступивших позиций. - поле.</summary>
	private string _SelectedSearchReceiptList;

	/// <summary>Текущий выбор из перечня параметров поиска списка поступивших позиций. - свойство.</summary>
	public string SelectedSearchReceiptList
	{
		get => _SelectedSearchReceiptList;
		set
		{
			_SelectedSearchReceiptList = value;
			OnPropertyChanged(nameof(SelectedSearchReceiptList));
		}
	}
	#endregion
	#region SearchReceiptList : string - Поле сроки поиска по перечню поступлений.

	/// <summary>Поле сроки поиска по перечню поступлений. - поле.</summary>
	private string _SearchReceiptList;

	/// <summary>Поле сроки поиска по перечню поступлений. - свойство.</summary>
	public string SearchReceiptList
	{
		get => _SearchReceiptList;
		set
		{
			_SearchReceiptList = value;
			OnPropertyChanged(nameof(SearchReceiptList));
			ExecuteShowListReceiptAfterSearch();
		}
	}
	#endregion

	#region ItemCBMoveList : List<string> - Перечень параметров поска списка перемещенных позиций.

	/// <summary>Перечень параметров поска списка перемещенных позиций. - поле.</summary>
	private List<string> _ItemCBMoveList;

	/// <summary>Перечень параметров поска списка перемещенных позиций. - свойство.</summary>
	public List<string> ItemCBMoveList
	{
		get => _ItemCBMoveList;
		set
		{
			_ItemCBMoveList = value;
			OnPropertyChanged(nameof(ItemCBMoveList));
		}
	}
	#endregion
	#region SelectedSearchMoveList : string - Перечень параметров поиска списка перемещенных позиций.

	/// <summary>Перечень параметров поиска списка перемещенных позиций. - поле.</summary>
	private string _SelectedSearchMoveList;

	/// <summary>Перечень параметров поиска списка перемещенных позиций. - свойство.</summary>
	public string SelectedSearchMoveList
	{
		get => _SelectedSearchMoveList;
		set
		{
			_SelectedSearchMoveList = value;
			OnPropertyChanged(nameof(SelectedSearchMoveList));
		}
	}
	#endregion
	#region SearchMoveList : string - Поле строки поиска по перечню перемещений.

	/// <summary>Поле строки поиска по перечню перемещений. - поле.</summary>
	private string _SearchMoveList;

	/// <summary>Поле строки поиска по перечню перемещений. - свойство.</summary>
	public string SearchMoveList
	{
		get => _SearchMoveList;
		set
		{
			_SearchMoveList = value;
			OnPropertyChanged(nameof(SearchMoveList));
			ExecuteShowListMoveAfterSearch();
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

	#region VisibleWriteOffButton : bool - Флаг видимости кнопки списания позиции.

	/// <summary>Флаг видимости кнопки списания позиции. - поле.</summary>
	private bool _VisibleWriteOffButton;

	/// <summary>Флаг видимости кнопки списания позиции. - свойство.</summary>
	public bool VisibleWriteOffButton
	{
		get => _VisibleWriteOffButton;
		set
		{
			_VisibleWriteOffButton = value;
			OnPropertyChanged(nameof(VisibleWriteOffButton));
		}
	}
	#endregion
	



	public MainViewModel(IUserDialog userDialog, IMessageBus MessageBus)
	{
		_userDialog = userDialog;
		_messageBus = MessageBus;
		_repositoriesDB = new AdminRepositories();
		
		ItemCBMainList = new List<string>{"Наименование", "Остаток", "Дата поступления"};
		ItemCBReceiptList = new List<string> {"Наименование", "Количество", "Дата поступления"};
		ItemCBMoveList = new List<string> {"Наименование", "Дата перемещения", "Пост"};
		SelectedSearchMainList = "Наименование";
		SelectedSearchReceiptList = "Наименование";
		SelectedSearchMoveList = "Наименование";
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
			MessageBox.Show("Выберите позицию из ОСНОВНОГО СПИСКА, чтобы открылось окно перемещения позиций.",
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

	#region Command OnLoadedMainTabCommand - Метод вызываемый при загрузке отображения основного перечня позиций.

	/// <summary>Метод вызываемый при загрузке отображения основного перечня позиций.</summary>
	private LambdaCommand? _OnLoadedMainTabCommand;

	/// <summary>Метод вызываемый при загрузке отображения основного перечня позиций.</summary>
	public ICommand OnLoadedMainTabCommand => _OnLoadedMainTabCommand ??= new(ExecutedOnLoadedMainTabCommand);

	/// <summary>Логика выполнения - Метод вызываемый при загрузке отображения основного перечня позиций.</summary>
	private void ExecutedOnLoadedMainTabCommand()
	{
		if(VisibleWriteOffButton != true)
			VisibleWriteOffButton = true;
	}
	#endregion

	#region Command OnLoadedReceiptTabCommand - Метод вызываемой при загрузке отображения перечня поступлений позиций.

	/// <summary>Метод вызываемой при загрузке отображения перечня поступлений позиций.</summary>
	private LambdaCommand? _OnLoadedReceiptTabCommand;

	/// <summary>Метод вызываемой при загрузке отображения перечня поступлений позиций.</summary>
	public ICommand OnLoadedReceiptTabCommand => _OnLoadedReceiptTabCommand ??= new(ExecutedOnLoadedReceiptTabCommand);

	/// <summary>Логика выполнения - Метод вызываемой при загрузке отображения перечня поступлений позиций.</summary>
	private void ExecutedOnLoadedReceiptTabCommand()
	{
		if(VisibleWriteOffButton)
			VisibleWriteOffButton = false;
	}
	#endregion

	#region Command OnLoadedMoveTabCommand - Метод вызываемый при загрузке отображения перечня перемещений позициий.

	/// <summary>Метод вызываемый при загрузке отображения перечня перемещений позициий.</summary>
	private LambdaCommand? _OnLoadedMoveTabCommand;

	/// <summary>Метод вызываемый при загрузке отображения перечня перемещений позициий.</summary>
	public ICommand OnLoadedMoveTabCommand => _OnLoadedMoveTabCommand ??= new(ExecutedOnLoadedMoveTabCommand);

	/// <summary>Логика выполнения - Метод вызываемый при загрузке отображения перечня перемещений позициий.</summary>
	private void ExecutedOnLoadedMoveTabCommand()
	{
		if(VisibleWriteOffButton)
			VisibleWriteOffButton = false;
	}
	#endregion

	#region Command WriteOffCommand - Команда списания позиции.

	/// <summary>Команда списания позиции.</summary>
	private LambdaCommand? _WriteOffCommand;

	/// <summary>Команда списания позиции.</summary>
	public ICommand WriteOffCommand => _WriteOffCommand ??= new(ExecutedWriteOffCommand);

	/// <summary>Логика выполнения - Команда списания позиции.</summary>
	private void ExecutedWriteOffCommand()
	{
		if (CurrentDataStockList is null)
		{
			MessageBox.Show("Выберите позицию из списка.",
				"Совет!",
				MessageBoxButton.OK,
				MessageBoxImage.Exclamation);
			return;
		}
		
		var result = MessageBox.Show("Вы уверены что хотите списать (удалить) выбранную позициию?",
			"Удаление позиции",
			MessageBoxButton.YesNo,
			MessageBoxImage.Question);
		if (result == MessageBoxResult.Yes)
		{
			var resultWriteOff = _repositoriesDB.AddWriteOff(CurrentDataStockList);
			if(resultWriteOff)
			{
				MessageBox.Show(
					"Позиция списана(удалена) УСПЕШНО!",
					"Результат удаления",
					MessageBoxButton.OK,
					MessageBoxImage.Information);
				DataStockList.Clear();
				DataStockList = _repositoriesDB.GetDataStockList();
			}
			else
			{
				MessageBox.Show(
					"В процессе удаления произошла ОШИБКА!\nПроверьте данные по перечням.",
					"Результат удаления",
					MessageBoxButton.OK,
					MessageBoxImage.Warning);
				DataStockList.Clear();
				DataStockList = _repositoriesDB.GetDataStockList();
			}
		}
		else
			return;
	}
	#endregion

	#region Command ShowActionSelectedDataStockItemCommand - Просмотр действий по выбранной позиции.

	/// <summary>Просмотр действий по выбранной позиции.</summary>
	private LambdaCommand? _ShowActionSelectedDataStockItemCommand;

	/// <summary>Просмотр действий по выбранной позиции.</summary>
	public ICommand ShowActionSelectedDataStockItemCommand => _ShowActionSelectedDataStockItemCommand ??= new(ExecutedShowActionSelectedDataStockItemCommand);

	/// <summary>Логика выполнения - Просмотр действий по выбранной позиции.</summary>
	private void ExecutedShowActionSelectedDataStockItemCommand()
	{

	}
	#endregion

	#region Command SetValueControlCommand - Открытие окна установки ограничения контроля количества позиции.

	/// <summary>Открытие окна установки ограничения контроля количества позиции.</summary>
	private LambdaCommand? _SetValueControlCommand;

	/// <summary>Открытие окна установки ограничения контроля количества позиции.</summary>
	public ICommand SetValueControlCommand => _SetValueControlCommand ??= new(ExecutedSetValueControlCommand);

	/// <summary>Логика выполнения - Открытие окна установки ограничения контроля количества позиции.</summary>
	private void ExecutedSetValueControlCommand()
	{

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
					break;
				}
			case "Остаток":
				{
					var tempMainList = _repositoriesDB.GetBySearchRemainingStockMainList(SearchMainList);
					foreach (var item in tempMainList)
						DataStockList.Add(item);
					break;
				}
			case "Дата поступления":
				{
					var resultParse = DateOnly.TryParse(SearchMainList, out var dateString);
					if (resultParse)
					{
						var tempMainList = _repositoriesDB.GetBySearchDateReceiptMainList(dateString);
						foreach (var item in tempMainList)
							DataStockList.Add(item);
					}
					break;
				}
		}
	}

	/// <summary>
	/// Метод выборки перечня поступиших позиций по введенным данным.
	/// </summary>
	private void ExecuteShowListReceiptAfterSearch()
	{
		ReceiptList = [];
		switch (SelectedSearchReceiptList)
		{
			case "Наименование":
				{
					var tempReceiptList = _repositoriesDB.GetBySearchNameItemReceiptList(SearchReceiptList);
					foreach (var item in tempReceiptList)
						ReceiptList.Add(item);
					break;
				}
			case "Количество":
				{
					var tempReceiptList = _repositoriesDB.GetBySearchUnitCountReceiptList(SearchReceiptList);
					foreach (var item in tempReceiptList)
						ReceiptList.Add(item);
                    break;
				}
			case "Дата поступления":
				{
					var resultParse = DateOnly.TryParse(SearchReceiptList, out var dateString);
					if (resultParse)
					{
						var tempReceiptList = _repositoriesDB.GetBySearchDateReceiptReceiptList(dateString);
						foreach (var item in tempReceiptList)
							ReceiptList.Add(item);
					}
					break;
				}
		}
	}

	/// <summary>
	/// Метод выборки перечня перемещенных позиций по введенным данным.
	/// </summary>
	private void ExecuteShowListMoveAfterSearch()
	{
		MoveList = [];
		switch (SelectedSearchMoveList)
		{
			case "Наименование": 
				{
					var tempMovetList = _repositoriesDB.GetBySearchNameItemMoveList(SearchMoveList);
					foreach (var item in tempMovetList)
						MoveList.Add(item);

					break; 
				}
			case "Дата перемещения":
				{
					var resultParse = DateOnly.TryParse(SearchMoveList, out var dateString);
					if (resultParse)
					{
						var tempmoveList = _repositoriesDB.GetBySearchDateReceiptMoveList(dateString);
						foreach (var item in tempmoveList)
							MoveList.Add(item);
					}
					break;
				}
			case "Пост":
				{
					var tempMovetList = _repositoriesDB.GetBySearchNamePostMoveList(SearchMoveList);
					foreach (var item in tempMovetList)
						MoveList.Add(item);

					break;
				}

		}
	}

}