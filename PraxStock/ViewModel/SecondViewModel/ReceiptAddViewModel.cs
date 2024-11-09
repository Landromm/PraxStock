using PraxStock.Communication.Repositories;
using PraxStock.Model.DBModels;
using PraxStock.Model.OtherModel;
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

namespace PraxStock.ViewModel.SecondViewModel
{
    class ReceiptAddViewModel : DialogViewModel
    {
		private readonly IAdminRepositories _repositoriesDB = null!;

		private ObservableCollection<Item> _ItemListCollection;
		public ObservableCollection<Item> ItemListCollection
		{
			get => _ItemListCollection ?? (_ItemListCollection = new ObservableCollection<Item>());
			set
			{
				_ItemListCollection = value;
				OnPropertyChanged(nameof(ItemListCollection));
			}
		}

		#region ItemListCollectionSecond : MainListItems -  Объект позиции для суммирования.

		///<summary>Объект позиции для суммирования. - поле.</summary>
		private MainListItems _ItemListCollectionSecond;

		///<summary>Объект позиции для суммирования. - свойство.</summary>
		public MainListItems ItemListCollectionSecond
		{
			get => _ItemListCollectionSecond;
			set
			{
				_ItemListCollectionSecond = value;
				OnPropertyChanged(nameof(ItemListCollectionSecond));
			}
		}
		#endregion

		#region NameItemList : List<string> - Список наименований.

		/// <summary>Список наименований. - поле.</summary>
		private List<string> _NameItemList;

		/// <summary>Список наименований. - свойство.</summary>
		public List<string> NameItemList
		{
			get => _NameItemList;
			set
			{
				_NameItemList = value;
				OnPropertyChanged(nameof(NameItemList));
			}
		}
		#endregion

		//Если к ComboBox'у привяжеться коллекция объектов то этот список надо удалить.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//#region NameItemListSecond : List<string, int> -  Список наименований для суммирования.

		/////<summary>Список наименований для суммирования. - поле.</summary>
		//private SortedList<int, string> _NameItemListSecond;

		/////<summary>Список наименований для суммирования. - свойство.</summary>
		//public SortedList<int, string> NameItemListSecond
		//{
		//	get => _NameItemListSecond;
		//	set
		//	{
		//		_NameItemListSecond = value;
		//		OnPropertyChanged(nameof(NameItemListSecond));
		//	}
		//}
		//#endregion

		#region NameItemListSecond : List<MainListItems> -  Список наименований для суммирования.

		///<summary>Список наименований для суммирования. - поле.</summary>
		private List<MainListItems> _NameItemListSecond;

		///<summary>Список наименований для суммирования. - свойство.</summary>
		public List<MainListItems> NameItemListSecond
		{
			get => _NameItemListSecond;
			set
			{
				_NameItemListSecond = value;
				OnPropertyChanged(nameof(NameItemListSecond));
			}
		}
		#endregion


		#region SelectedNameItem : string? - Выбранное наименование позиции.

		/// <summary>Выбранное наименование позиции. - поле.</summary>
		private string? _SelectedNameItem;

		/// <summary>Выбранное наименование позиции. - свойство.</summary>
		public string? SelectedNameItem
		{
			get => _SelectedNameItem;
			set
			{
				_SelectedNameItem = value;
				OnPropertyChanged(nameof(SelectedNameItem));
				if(value is not null)
				{
					ShowCheckBoxSecretPanel = true;
					ItemListCollection?.Clear();
					ItemListCollection = _repositoriesDB.GetBySearchNameItem(value);
					foreach (var item in ItemListCollection)
					{
						NameItem = item.NameItem;
						UnitMeasure = item.UnitMeasure;
					}
				}
			}
		}
		#endregion
		#region SelectedNameItemSecond : MainListItems -  Выбранное наименование позиции для суммирования.

		///<summary>Выбранное наименование позиции для суммирования. - поле.</summary>
		private MainListItems _SelectedNameItemSecond;

		///<summary>Выбранное наименование позиции для суммирования. - свойство.</summary>
		public MainListItems SelectedNameItemSecond
		{
			get => _SelectedNameItemSecond;
			set
			{
				_SelectedNameItemSecond = value;
				OnPropertyChanged(nameof(SelectedNameItemSecond));
				if(value is not null)
				{
					ItemListCollectionSecond = _repositoriesDB.GetBySearchIdStockItem(value.IdDataStock);
					NameItemSecond = ItemListCollectionSecond.Name;
					UnitMeasureSecond = ItemListCollectionSecond.UnitMeasure;
					QuantityReceiptSecond = ItemListCollectionSecond.UnitCount;
					var tempExpiration = ItemListCollectionSecond.ExpirationDate;
					if (tempExpiration is not null)
						ExpirationDateSecond = ((DateOnly)tempExpiration).ToDateTime(TimeOnly.MinValue);
					DateReceiptSecond = ItemListCollectionSecond.DateReceipt.ToDateTime(TimeOnly.MinValue);
				}

			}
		}
		#endregion



		#region NameItem : string? - Наименование позиции.

		/// <summary>Наименование позиции. - поле.</summary>
		private string? _NameItem;

		/// <summary>Наименование позиции. - свойство.</summary>
		public string? NameItem
		{
			get => _NameItem;
			set
			{
				_NameItem = value;
				OnPropertyChanged(nameof(NameItem));
			}
		}
		#endregion
		#region NameItemSecond : string? -  Наименование позиции для суммирования.

		///<summary>Наименование позиции для суммирования. - поле.</summary>
		private string? _NameItemSecond;

		///<summary>Наименование позиции для суммирования. - свойство.</summary>
		public string? NameItemSecond
		{
			get => _NameItemSecond;
			set
			{
				_NameItemSecond = value;
				OnPropertyChanged(nameof(NameItemSecond));
			}
		}
		#endregion

		#region UnitMeasure : string? - Единица измерения позиции.

		/// <summary>Единица измерения позиции. - поле.</summary>
		private string? _UnitMeasure;

		/// <summary>Единица измерения позиции. - свойство.</summary>
		public string? UnitMeasure
		{
			get => _UnitMeasure;
			set
			{
				_UnitMeasure = value;
				OnPropertyChanged(nameof(UnitMeasure));
			}
		}
		#endregion
		#region UnitMeasureSecond : string? -  Единица измерения позиции для суммирования.

		///<summary>Единица измерения позиции для суммирования. - поле.</summary>
		private string? _UnitMeasureSecond;

		///<summary>Единица измерения позиции для суммирования. - свойство.</summary>
		public string? UnitMeasureSecond
		{
			get => _UnitMeasureSecond;
			set
			{
				_UnitMeasureSecond = value;
				OnPropertyChanged(nameof(UnitMeasureSecond));
			}
		}
		#endregion

		#region QuantityReceipt : double - Количество поступившей позиции.

		/// <summary>Количество поступившей позиции. - поле.</summary>
		private double _QuantityReceipt;

		/// <summary>Количество поступившей позиции. - свойство.</summary>
		public double QuantityReceipt
		{
			get => _QuantityReceipt;
			set
			{
				_QuantityReceipt = value;
				OnPropertyChanged(nameof(QuantityReceipt));
			}
		}
		#endregion
		#region QuantityReceiptSecond : double -  Количество поступившей позиции.

		///<summary>Количество поступившей позиции. - поле.</summary>
		private double _QuantityReceiptSecond;

		///<summary>Количество поступившей позиции. - свойство.</summary>
		public double QuantityReceiptSecond
		{
			get => _QuantityReceiptSecond;
			set
			{
				_QuantityReceiptSecond = value;
				OnPropertyChanged(nameof(QuantityReceiptSecond));
			}
		}
		#endregion

		#region ExpirationDate : DateTime - Срок годности.

		/// <summary>Срок годности. - поле.</summary>
		private DateTime? _ExpirationDate;

		/// <summary>Срок годности. - свойство.</summary>
		public DateTime? ExpirationDate
		{	
			get => _ExpirationDate;
			set
			{
				_ExpirationDate = value;
				OnPropertyChanged(nameof(ExpirationDate));
			}
		}
		#endregion
		#region ExpirationDateSecond : DateTime? -  Срок годности для суммирования.

		///<summary>Срок годности для суммирования. - поле.</summary>
		private DateTime? _ExpirationDateSecond;

		///<summary>Срок годности для суммирования. - свойство.</summary>
		public DateTime? ExpirationDateSecond
		{
			get => _ExpirationDateSecond;
			set
			{
				_ExpirationDateSecond = value;
				OnPropertyChanged(nameof(ExpirationDateSecond));
			}
		}
		#endregion

		#region DateReceipt : DateTime - Дата поступления.

		/// <summary>Дата поступления. - поле.</summary>
		private DateTime _DateReceipt;

		/// <summary>Дата поступления. - свойство.</summary>
		public DateTime DateReceipt
		{
			get => _DateReceipt;
			set
			{
				_DateReceipt = value;
				OnPropertyChanged(nameof(DateReceipt));
			}
		}
		#endregion
		#region DateReceiptSecond : DateTime -  Дата поступления для суммирования.

		///<summary>Дата поступления для суммирования. - поле.</summary>
		private DateTime _DateReceiptSecond;

		///<summary>Дата поступления для суммирования. - свойство.</summary>
		public DateTime DateReceiptSecond
		{
			get => _DateReceiptSecond;
			set
			{
				_DateReceiptSecond = value;
				OnPropertyChanged(nameof(DateReceiptSecond));
			}
		}
		#endregion

		#region ShowSecretPanel : bool -  Флаг отображения скрытой панели.

		///<summary>Флаг отображения скрытой панели. - поле.</summary>
		private bool _ShowSecretPanel;

		///<summary>Флаг отображения скрытой панели. - свойство.</summary>
		public bool ShowSecretPanel
		{
			get => _ShowSecretPanel;
			set
			{

				_ShowSecretPanel = value;
				OnPropertyChanged(nameof(ShowSecretPanel));
			}
		}
		#endregion

		#region ShowCheckBoxSecretPanel : bool -  Флаг отображения checkbox-активации скрытой панели.

		///<summary>Флаг отображения checkbox-активации скрытой панели. - поле.</summary>
		private bool _ShowCheckBoxSecretPanel;

		///<summary>Флаг отображения checkbox-активации скрытой панели. - свойство.</summary>
		public bool ShowCheckBoxSecretPanel
		{
			get => _ShowCheckBoxSecretPanel;
			set
			{
				_ShowCheckBoxSecretPanel = value;
				OnPropertyChanged(nameof(ShowCheckBoxSecretPanel));
			}
		}
		#endregion




		public ReceiptAddViewModel()
		{
			_repositoriesDB = new AdminRepositories();
			InicializationMain();
		}

		private bool CanTestCommandExecute(object p) => true;

		#region Command DropdownSelectionChanged - Раскрытие выпадающего списка наименований.

		/// <summary>Раскрытие выпадающего списка наименований.</summary>
		private LambdaCommand? _DropdownSelectionChanged;

		/// <summary>Раскрытие выпадающего списка наименований.</summary>
		public ICommand DropdownSelectionChanged => _DropdownSelectionChanged ??= new(ExecutedDropdownSelectionChanged, CanTestCommandExecute);

		/// <summary>Логика выполнения - Раскрытие выпадающего списка наименований.</summary>
		private void ExecutedDropdownSelectionChanged(object p)
		{
			NameItemList = _repositoriesDB.GetAllNameItem();
		}
		#endregion

		#region Command DropdownSelectionSecondChanged - Раскрытие выпадающего списка позициий для суммирования.

		///<summary>Раскрытие выпадающего списка позициий для суммирования. - поле.</summary>
		private LambdaCommand? _DropdownSelectionSecondChanged;

		///<summary>Раскрытие выпадающего списка позициий для суммирования. - Реализация интерфейса</summary>
		public ICommand DropdownSelectionSecondChanged => _DropdownSelectionSecondChanged ??= new(ExecuteDropdownSelectionSecondChanged, CanTestCommandExecute);

		///<summary>Логикак выполнения - Раскрытие выпадающего списка позициий для суммирования</summary>
		private void ExecuteDropdownSelectionSecondChanged(object p)
		{
			NameItemListSecond = _repositoriesDB.GetAllNameItemSecond();
		}
		#endregion

		#region Command AddReceiptCommand - Добавление нового поступления позиции.

		/// <summary>Добавление нового поступления позиции.</summary>
		private LambdaCommand? _AddReceiptCommand;

		/// <summary>Добавление нового поступления позиции.</summary>
		public ICommand AddReceiptCommand => _AddReceiptCommand ??= new(ExecutedAddReceiptCommand, CanTestCommandExecute);

		/// <summary>Логика выполнения - Добавление нового поступления позиции.</summary>
		private void ExecutedAddReceiptCommand(object p)
		{
			
				ReceiptListItem fullInfoItem = new ReceiptListItem();
				fullInfoItem.IdItem = _repositoriesDB.GetBySearchIdItem(NameItem!);
				fullInfoItem.Name = NameItem;
				fullInfoItem.UnitMeasure = UnitMeasure;
				fullInfoItem.UnitCount = QuantityReceipt;
				if (ExpirationDate is not null)
					fullInfoItem.ExpirationDate = DateOnly.FromDateTime((DateTime)ExpirationDate);
				fullInfoItem.DateReceipt = DateOnly.FromDateTime(DateReceipt);


			try
			{
				if (!ShowSecretPanel)
					_repositoriesDB.AddReceiptItem(fullInfoItem);
				else
				{
					var resultReceipt = _repositoriesDB.AddReceiptItemSecond(fullInfoItem, SelectedNameItemSecond.IdDataStock);
					if (resultReceipt)
					{
						MessageBox.Show(
							"Позиция добавлена УСПЕШНО!",
							"Результат добавления",
							MessageBoxButton.OK,
							MessageBoxImage.Information);
					}
					else
					{
						MessageBox.Show(
							"В процессе добавления произошла ОШИБКА!",
							"Результат добавления",
							MessageBoxButton.OK,
							MessageBoxImage.Warning);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Добавление перемещение прошло с ошибкой. Проверьте правильность последних действий.",
					"Ошибка!",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
			}

				NameItem = null;
				UnitMeasure = null;
				QuantityReceipt = 0;
				ItemListCollection = null!;
				ItemListCollectionSecond = null!;
				SelectedNameItem = null;
				SelectedNameItemSecond = null!;
				ShowSecretPanel = false;
				NameItemSecond = null;
				UnitMeasureSecond = null;
				QuantityReceiptSecond = 0;
				ExpirationDate = null;
				ExpirationDateSecond = null;
				ShowCheckBoxSecretPanel = false;
					
		}
		#endregion

		#region Command ResetCommand - Очистить форму для добавления нового поступления позиции.

		/// <summary>Очистить форму для добавления нового поступления позиции.</summary>
		private LambdaCommand? _ResetCommand;

		/// <summary>Очистить форму для добавления нового поступления позиции.</summary>
		public ICommand ResetCommand => _ResetCommand ??= new(ExecutedResetCommand, CanTestCommandExecute);

		/// <summary>Логика выполнения - Очистить форму для добавления нового поступления позиции.</summary>
		private void ExecutedResetCommand(object p)
		{
			NameItem = null;
			UnitMeasure = null;
			QuantityReceipt = 0;
			ItemListCollection = null!;
			ItemListCollectionSecond = null!;
			SelectedNameItem = null;
			SelectedNameItemSecond = null!;
			ShowSecretPanel = false;
			NameItemSecond = null;
			UnitMeasureSecond = null;
			QuantityReceiptSecond = 0;
			DateReceipt = DateTime.Now;
			ExpirationDate = null;
			ExpirationDateSecond = null;
			ShowCheckBoxSecretPanel = false;
		}
		#endregion




		private void InicializationMain()
		{
			NameItemList = _repositoriesDB.GetAllNameItem();
			NameItemList = [];
			ItemListCollection = [];
			DateReceipt = DateTime.Now;
		}
	}
}
