using PraxStock.Model.DBModels;
using PraxStock.Model.OtherModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Communication.Repositories
{
    interface IAdminRepositories
    {
        ObservableCollection<Item> GetAllItemsList();
        ObservableCollection<Item> GetBySearchNumberItem(string searchNumber);
        ObservableCollection<Item> GetBySearchNameItem(string searchName);
        ObservableCollection<Item> GetBySearchUnitMeasureItem(string searchUnitMeasure);
        ObservableCollection<MainListItems> GetDataStockList();
        ObservableCollection<ReceiptListItem> GetReseiptList();


		List<string> GetAllNameItem();
        void AddItemsList(string nameItem, string unitMeasure);
        void ChangedItemList(string nameItem, string unitMeasure, int idItem);

        void AddReceiptItem(ReceiptListItem receiptListItem);


	}
}
