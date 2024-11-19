using PraxStock.Model.DBModels;
using PraxStock.Model.OtherModel;
using PraxStock.Model.OtherModel.StatisticsModel;
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

        ObservableCollection<MainListItems> GetBySearchNameItemMainList(string searchName);
        ObservableCollection<MainListItems> GetBySearchRemainingStockMainList(string remainingStock);
		ObservableCollection<MainListItems> GetBySearchExpirationDateMainList(DateOnly expirationDate);
		ObservableCollection<MainListItems> GetBySearchDateReceiptMainList(DateOnly dateReceipt);

		ObservableCollection<ReceiptListItem> GetBySearchNameItemReceiptList(string searchName);
		ObservableCollection<ReceiptListItem> GetBySearchUnitCountReceiptList(string unitCount);
		ObservableCollection<ReceiptListItem> GetBySearchDateReceiptReceiptList(DateOnly dateReceipt);

        ObservableCollection<MoveListItem> GetBySearchNameItemMoveList(string searchName);
        ObservableCollection<MoveListItem> GetBySearchDateReceiptMoveList(DateOnly dateReceipt);
        ObservableCollection<MoveListItem> GetBySearchNamePostMoveList(string namePost);

		ObservableCollection<MainListItems> GetDataStockList();
		ObservableCollection<ReceiptListItem> GetReseiptList();
        ObservableCollection<MoveListItem> GetMoveInPostList();

        ObservableCollection<ExpenseStatisticModel> GetExpenseStatisticModels(DateOnly startDate, DateOnly endDate);


		List<string> GetAllNameItem();
		List<string> GetAllNamePost();

		List<MainListItems> GetAllNameItemSecond(string nameItem);
		MainListItems GetBySearchIdStockItem(int searchIdStock);
        int GetBySearchIdItem(string searchName);
		void AddItemsList(string nameItem, string unitMeasure);
        void ChangedItemList(string nameItem, string unitMeasure, int idItem);
        bool UpdateControlValueDataStock(int idItem, double minValue);

        void AddReceiptItem(ReceiptListItem receiptListItem);
        bool AddReceiptItemSecond(ReceiptListItem receiptListItem, int idStockItem);

		bool AddMoveInPost(MoveListItem moveListItem);
        double GetRemainingStock(int idDataStock);

        bool AddWriteOff(MainListItems writeOffItem);

	}
}
