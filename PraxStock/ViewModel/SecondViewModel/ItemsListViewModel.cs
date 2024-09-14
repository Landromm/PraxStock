using Microsoft.Web.WebView2.Core;
using PraxStock.Communication.Repositories;
using PraxStock.Model.DBModels;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.ViewModel.SecondViewModel
{
    class ItemsListViewModel : DialogViewModel
    {
		private readonly IAdminRepositories _repositoriesDB = null!;

		#region ObservableCollection<Item> : Item -  Коллекция основных элементов.

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

	}
}
