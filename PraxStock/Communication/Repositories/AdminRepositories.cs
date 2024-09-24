using Microsoft.EntityFrameworkCore;
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
	class AdminRepositories : IAdminRepositories
	{
		public ObservableCollection<Item> GetAllItemsList()
		{
			ObservableCollection<Item> tempResult = new ();
			using var contex = new PraxixSkladContext();
			{
				var result = contex.Items.ToList();

				foreach (var item in result)
					tempResult.Add(item);
			}
			return tempResult;
		}

		public void AddItemsList(string nameItem, string unitMeasure)
		{
			using var contex = new PraxixSkladContext();
			{
				var tempItem = new Item() { NameItem = nameItem, UnitMeasure = unitMeasure };
				contex.Add(tempItem);
				contex.SaveChanges();
			}
		}

		public void ChangedItemList(string nameItem, string unitMeasure, int idItem)
		{
			using var contex = new PraxixSkladContext();
			{
				var item = new Item()
				{
					IdItem = idItem,
					NameItem = nameItem,
					UnitMeasure = unitMeasure
				};
				contex.Update(item);
				contex.SaveChanges();
			}
		}

		public ObservableCollection<Item> GetBySearchNumberItem(string searchNumber)
		{
			ObservableCollection<Item> resultCollection = [];
			using var context = new PraxixSkladContext();
			{
				var selectSearchCollection = from item in context.Items
											 where EF.Functions.Like(item.IdItem.ToString(), searchNumber + "%")
											 select item;

				foreach (var item in selectSearchCollection)
					resultCollection.Add(item);
			}

			return resultCollection;
		}

		public ObservableCollection<Item> GetBySearchNameItem(string searchName)
		{
			ObservableCollection<Item> resultCollection = [];
			using var context = new PraxixSkladContext();
			{
				var selectSearchCollection = from item in context.Items
											 where EF.Functions.Like(item.NameItem, searchName + "%")
											 select item;

				foreach (var item in selectSearchCollection)
					resultCollection.Add(item);
			}

			return resultCollection;
		}

		public ObservableCollection<Item> GetBySearchUnitMeasureItem(string searchUnitMeasure)
		{
			ObservableCollection<Item> resultCollection = [];
			using var context = new PraxixSkladContext();
			{
				var selectSearchCollection = from item in context.Items
											 where EF.Functions.Like(item.UnitMeasure, searchUnitMeasure + "%")
											 select item;

				foreach (var item in selectSearchCollection)
					resultCollection.Add(item);
			}

			return resultCollection;
		}

		public List<string> GetAllNameItem()
		{
			var result = new List<string>();
			using var context = new PraxixSkladContext();
			{
				result = context.Items
					.Select(n =>  n.NameItem)
					.ToList();
			}

			return result;
		}

		public void AddReceiptItem(ReceiptListItem receiptListItem)
		{
			Receipt receipt = new();
			DataStock dataStock = new();
			using var context = new PraxixSkladContext();
			{
				var idItem = (from item in context.Items
							  where item.NameItem == receiptListItem.Name
							  select item.IdItem).First();

				receipt = new Receipt()
				{
					IdItem = idItem,
					QuantityReceipt = receiptListItem.UnitCount,
					ExprirationDate = receiptListItem.ExpirationDate,
					DateReceipt = receiptListItem.DateReceipt,
				};


				context.Receipts.Add(receipt);
				context.SaveChanges();

				using var context2 = new PraxixSkladContext();
				{
					var idStock = context.Receipts
						.Where(id => id.IdItem == idItem)
						.OrderBy(x => x.IdReceipt)
						.Select(idS => idS.IdReceipt)
						.LastOrDefault();

					dataStock = new DataStock()
					{
						IdItemStock = idStock,
						IdItem = idItem,
						RemainingStock = receiptListItem.UnitCount
					};

					context.DataStocks.Add(dataStock);
					context.SaveChanges();
				}
			}
		}
	}
}
