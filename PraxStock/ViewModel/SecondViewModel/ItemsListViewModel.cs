using Microsoft.Web.WebView2.Core;
using PraxStock.Communication.Repositories;
using PraxStock.Model.DBModels;
using PraxStock.Services;
using PraxStock.View.Commands;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PraxStock.ViewModel.SecondViewModel
{
    class ItemsListViewModel : DialogViewModel
    {
		private readonly IAdminRepositories _repositoriesDB = null!;
		private readonly IUserDialog _userDialog = null!;

		#region ItemCBMainList : List<string> - Перечень параметров поиска.

		/// <summary>Перечень параметров поиска. - поле.</summary>
		private List<string> _ItemCBMainList;

		/// <summary>Перечень параметров поиска. - свойство.</summary>
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

		#region SelectedSearchMainList : string - Текущий выбор из перечня параметров поиска.

		/// <summary>Текущий выбор из перечня параметров поиска. - поле.</summary>
		private string _SelectedSearchMainList;

		/// <summary>Текущий выбор из перечня параметров поиска. - свойство.</summary>
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

		#region Item : CurrentDataStockList -  Коллекция основных элементов.

		///<summary>Коллекция основных элементов. - поле.</summary>
		private Item _CurrentDataStockList;

		///<summary>Коллекция основных элементов. - свойство.</summary>
		public Item CurrentDataStockList
		{
			get => _CurrentDataStockList;
			set
			{
				_CurrentDataStockList = value;
				OnPropertyChanged(nameof(CurrentDataStockList));
				if(_CurrentDataStockList != null)
				{
					NameItemList = _CurrentDataStockList.NameItem;
					UnitMeasureList = _CurrentDataStockList.UnitMeasure;
				}
			}
		}
		#endregion

		#region ObservableCollection<Item> : MainItemsList -  Коллекция основных элементов.

		///<summary>Коллекция основных элементов. - поле.</summary>
		private ObservableCollection<Item> _MainItemsList;

		///<summary>Коллекция основных элементов. - свойство.</summary>
		public ObservableCollection<Item> MainItemsList
		{
			get => _MainItemsList ?? (_MainItemsList = new ObservableCollection<Item>());
			set
			{
				_MainItemsList = value;
				OnPropertyChanged(nameof(MainItemsList));
			}
		}
		#endregion

		#region NameItemList : string -  Наименование позиции.

		///<summary>Наименование позиции. - поле.</summary>
		private string _NameItemList;

		///<summary>Наименование позиции. - свойство.</summary>
		public string NameItemList
		{
			get => _NameItemList;
			set
			{
				_NameItemList = value;
				OnPropertyChanged(nameof(NameItemList));
			}
		}
		#endregion

		#region UnitMeasureList : string -  Еденица измерения позиции.

		///<summary>Еденица измерения позиции. - поле.</summary>
		private string _UnitMeasureList;

		///<summary>Еденица измерения позиции. - свойство.</summary>
		public string UnitMeasureList
		{
			get => _UnitMeasureList;
			set
			{
				_UnitMeasureList = value;
				OnPropertyChanged(nameof(UnitMeasureList));
			}
		}
		#endregion

		#region SearchMainList : string - Поле строки поиска.

		/// <summary>Поле строки поиска. - поле.</summary>
		private string _SearchMainList;

		/// <summary>Поле строки поиска. - свойство.</summary>
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

		public ItemsListViewModel(IUserDialog userDialog)
		{
			_repositoriesDB = new AdminRepositories();
			_userDialog = userDialog;
			InicializationMain();
		}

		/// <summary>
		/// Инициализация основных параметров.
		/// </summary>
		private void InicializationMain()
		{
			MainItemsList = _repositoriesDB.GetAllItemsList();
			ItemCBMainList = new List<string>() { "№ Позиции", "Наименование", "Ед.измер." };
			SelectedSearchMainList = "Наименование";
		}

		#region Command AddItemCommand - Добавление новой позиции.

		///<summary>Добавление новой позиции. - поле.</summary>
		private LambdaCommand? _AddItemCommand;

		///<summary>Добавление новой позиции. - Реализация интерфейса</summary>
		public ICommand AddItemCommand => _AddItemCommand ??= new(ExecuteAddItemCommand, CanTestCommandExecute);

		///<summary>Логикак выполнения - Добавление новой позиции</summary>
		private void ExecuteAddItemCommand(object p)
		{
			if(NameItemList != null && UnitMeasureList != null && CurrentDataStockList is null)
			{
				_repositoriesDB.AddItemsList(NameItemList, UnitMeasureList);
				MainItemsList.Clear();
				NameItemList = "";
				UnitMeasureList = "";
				CurrentDataStockList = new Item();
				MainItemsList = _repositoriesDB.GetAllItemsList();
			}
			if(NameItemList != null && UnitMeasureList != null && CurrentDataStockList is not null)
			{
				_repositoriesDB.ChangedItemList(NameItemList, UnitMeasureList, CurrentDataStockList.IdItem);
				MainItemsList.Clear();
				NameItemList = "";
				UnitMeasureList = "";
				CurrentDataStockList = new Item();
				MainItemsList = _repositoriesDB.GetAllItemsList();
			}
		}
		#endregion

		private bool CanTestCommandExecute(object p) => true;
		#region Command ResetAddFieldCommand - Очистка полей для добавления новой позиции.

		///<summary>Очистка полей для добавления новой позиции. - поле.</summary>
		private LambdaCommand? _ResetAddFieldCommand;

		///<summary>Очистка полей для добавления новой позиции. - Реализация интерфейса</summary>
		public ICommand ResetAddFieldCommand => _ResetAddFieldCommand ??= new(ExecuteResetAddFieldCommand, CanTestCommandExecute);

		///<summary>Логикак выполнения - Очистка полей для добавления новой позиции</summary>
		private void ExecuteResetAddFieldCommand(object p)
		{
			if (NameItemList != null)
				NameItemList = "";
			if (UnitMeasureList != null)
				UnitMeasureList = "";
			CurrentDataStockList = new Item();
			SelectedSearchMainList = "Наименование";
			SearchMainList = "";
		}
		#endregion

		/// <summary>
		/// Метод выборки позиций по введенным данным.
		/// </summary>
		private void ExecuteShowListCatalogAfterSearch()
		{
			MainItemsList = [];
			switch(SelectedSearchMainList)
			{
				case "№ Позиции": 
					{
						var tempMainList = _repositoriesDB.GetBySearchNumberItem(SearchMainList);
						foreach (var tempItem in tempMainList)
							MainItemsList.Add(tempItem);

						break;	
					}
				case "Наименование":
					{
						var tempMainList = _repositoriesDB.GetBySearchNameItem(SearchMainList);
						foreach (var tempItem in tempMainList)
							MainItemsList.Add(tempItem);

						break;
					}
				case "Ед.измер.":
					{
						var tempMainList = _repositoriesDB.GetBySearchUnitMeasureItem(SearchMainList);
						foreach (var tempItem in tempMainList)
							MainItemsList.Add(tempItem);

						break;
					}
			}
		}
	}
}
