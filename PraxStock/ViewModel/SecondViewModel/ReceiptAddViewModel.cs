﻿using PraxStock.Communication.Repositories;
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
				ItemListCollection = _repositoriesDB.GetBySearchNameItem(value);
				foreach (var item in ItemListCollection)
				{
					NameItem = item.NameItem;
					UnitMeasure = item.UnitMeasure;
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

		#region ExpirationDate : DateTime - Срок годности.

		/// <summary>Срок годности. - поле.</summary>
		private DateTime _ExpirationDate;

		/// <summary>Срок годности. - свойство.</summary>
		public DateTime ExpirationDate
		{	
			get => _ExpirationDate;
			set
			{
				_ExpirationDate = value;
				OnPropertyChanged(nameof(ExpirationDate));
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

		public ReceiptAddViewModel()
		{
			_repositoriesDB = new AdminRepositories();
			InicializationMain();
		}


		#region Command DropdownSelectionChanged - Раскрытие выпадающего списка наименований.

		/// <summary>Раскрытие выпадающего списка наименований.</summary>
		private LambdaCommand? _DropdownSelectionChanged;

		/// <summary>Раскрытие выпадающего списка наименований.</summary>
		public ICommand DropdownSelectionChanged => _DropdownSelectionChanged ??= new(ExecutedDropdownSelectionChanged);

		/// <summary>Логика выполнения - Раскрытие выпадающего списка наименований.</summary>
		private void ExecutedDropdownSelectionChanged()
		{
			NameItemList = _repositoriesDB.GetAllNameItem();
		}
		#endregion

		#region Command AddReceiptCommand - Добавление нового поступления позиции.

		/// <summary>Добавление нового поступления позиции.</summary>
		private LambdaCommand? _AddReceiptCommand;

		/// <summary>Добавление нового поступления позиции.</summary>
		public ICommand AddReceiptCommand => _AddReceiptCommand ??= new(ExecutedAddReceiptCommand);

		/// <summary>Логика выполнения - Добавление нового поступления позиции.</summary>
		private void ExecutedAddReceiptCommand()
		{
			ReceiptListItem fullInfoItem = new ReceiptListItem();
			fullInfoItem.Name = NameItem;
			fullInfoItem.UnitMeasure = UnitMeasure;
			fullInfoItem.UnitCount = QuantityReceipt;
			fullInfoItem.ExpirationDate = DateOnly.FromDateTime(ExpirationDate);
			fullInfoItem.DateReceipt = DateOnly.FromDateTime(DateReceipt);

			_repositoriesDB.AddReceiptItem(fullInfoItem);

			NameItem = "";
			UnitMeasure = "";
			QuantityReceipt = 0;
		}
		#endregion

		#region Command ResetCommand - Очистить форму для добавления нового поступления позиции.

		/// <summary>Очистить форму для добавления нового поступления позиции.</summary>
		private LambdaCommand? _ResetCommand;

		/// <summary>Очистить форму для добавления нового поступления позиции.</summary>
		public ICommand ResetCommand => _ResetCommand ??= new(ExecutedResetCommand);

		/// <summary>Логика выполнения - Очистить форму для добавления нового поступления позиции.</summary>
		private void ExecutedResetCommand()
		{
			NameItem = "";
			UnitMeasure = "";
			QuantityReceipt = 0;
			ExpirationDate = DateTime.Now;
			DateReceipt = DateTime.Now;
		}
		#endregion


		private void InicializationMain()
		{
			NameItemList = _repositoriesDB.GetAllNameItem();
			NameItemList = [];
			ItemListCollection = [];
			ExpirationDate = DateTime.Now;
			DateReceipt = DateTime.Now;
		}
	}
}
