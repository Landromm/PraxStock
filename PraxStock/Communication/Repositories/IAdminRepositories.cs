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
        ObservableCollection<Item> GetAllItemsList();                                                                   // OK
        ObservableCollection<Item> GetBySearchNumberItem(string searchNumber);                                          // OK
        ObservableCollection<Item> GetBySearchNameItem(string searchName);                                              // OK
        ObservableCollection<Item> GetBySearchUnitMeasureItem(string searchUnitMeasure);                                // OK

        ObservableCollection<MainListItems> GetBySearchNameItemMainList(string searchName);                             // OK
        ObservableCollection<MainListItems> GetBySearchRemainingStockMainList(string remainingStock);                   // OK
		ObservableCollection<MainListItems> GetBySearchExpirationDateMainList(DateOnly expirationDate);                 // OK
		ObservableCollection<MainListItems> GetBySearchDateReceiptMainList(string dateReceipt);                         // OK

		ObservableCollection<ReceiptListItem> GetBySearchNameItemReceiptList(string searchName);                        // OK ???
		ObservableCollection<ReceiptListItem> GetBySearchUnitCountReceiptList(string unitCount);                        // OK ???
		ObservableCollection<ReceiptListItem> GetBySearchDateReceiptReceiptList(string dateReceipt);                    // OK ???

        ObservableCollection<MoveListItem> GetBySearchNameItemMoveList(string searchName);                              // OK ???
        ObservableCollection<MoveListItem> GetBySearchDateReceiptMoveList(string dateReceipt);                          // OK ???
        ObservableCollection<MoveListItem> GetBySearchNamePostMoveList(string namePost);                                // OK ???

		ObservableCollection<MainListItems> GetDataStockList();                                                         // OK
		ObservableCollection<ReceiptListItem> GetReseiptList();                                                         // OK
        ObservableCollection<MoveListItem> GetMoveInPostList();                                                         // OK ???

        ObservableCollection<ExpenseStatisticModel> GetExpenseStatisticModels(DateOnly startDate, DateOnly endDate);    // OK ???


		List<string> GetAllNameItem();                                                                                  // OK
		List<string> GetAllNamePost();                                                                                  // OK

		List<MainListItems> GetAllNameItemSecond(string nameItem);                                                      // OK
		MainListItems GetBySearchIdStockItem(int searchIdStock);                                                        // OK ???
        int GetBySearchIdItem(string searchName);                                                                       // OK
		void AddItemsList(string nameItem, string unitMeasure);                                                         // OK
        void ChangedItemList(string nameItem, string unitMeasure, int idItem);                                          // OK
        bool UpdateControlValueDataStock(int idItem, double minValue);                                                  // OK

        void AddReceiptItem(ReceiptListItem receiptListItem);                                                           // OK
        bool AddReceiptItemSecond(ReceiptListItem receiptListItem, int idStockItem);                                    // proc

		bool AddMoveInPost(MoveListItem moveListItem);                                                                  // OK
        double GetRemainingStock(int idDataStock);                                                                      // OK ???

        bool AddWriteOff(MainListItems writeOffItem);                                                                   // proc

	}
}
