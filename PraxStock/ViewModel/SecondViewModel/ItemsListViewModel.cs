using Microsoft.Web.WebView2.Core;
using PraxStock.Communication.Repositories;
using PraxStock.Model.DBModels;
using PraxStock.View.Commands;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PraxStock.ViewModel.SecondViewModel
{
    class ItemsListViewModel : DialogViewModel
    {
		private readonly IAdminRepositories _repositoriesDB = null!;

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

		public ItemsListViewModel()
		{
			_repositoriesDB = new AdminRepositories();
			InicializationMain();
		}

		private void InicializationMain()
		{
			MainItemsList = _repositoriesDB.GetAllItemsList();
		}


		#region Command AddItemCommand - Добавление новой позиции.

		///<summary>Добавление новой позиции. - поле.</summary>
		private LambdaCommand? _AddItemCommand;

		///<summary>Добавление новой позиции. - Реализация интерфейса</summary>
		public ICommand AddItemCommand => _AddItemCommand ??= new(ExecuteAddItemCommand);

		///<summary>Логикак выполнения - Добавление новой позиции</summary>
		private void ExecuteAddItemCommand()
		{
			if(NameItemList != null && UnitMeasureList != null)
			{
				_repositoriesDB.AddItemsList(NameItemList, UnitMeasureList);
				MainItemsList.Clear();
				NameItemList = "";
				UnitMeasureList = "";
				CurrentDataStockList = new Item();
				MainItemsList = _repositoriesDB.GetAllItemsList();
			}
		}
		#endregion

		#region Command ResetAddFieldCommand - Очистка полей для добавления новой позиции.

		///<summary>Очистка полей для добавления новой позиции. - поле.</summary>
		private LambdaCommand? _ResetAddFieldCommand;

		///<summary>Очистка полей для добавления новой позиции. - Реализация интерфейса</summary>
		public ICommand ResetAddFieldCommand => _ResetAddFieldCommand ??= new(ExecuteResetAddFieldCommand);

		///<summary>Логикак выполнения - Очистка полей для добавления новой позиции</summary>
		private void ExecuteResetAddFieldCommand()
		{
			if (NameItemList != null)
				NameItemList = "";
			if (UnitMeasureList != null)
				UnitMeasureList = "";
			CurrentDataStockList = new Item();
		}
		#endregion


	}
}
