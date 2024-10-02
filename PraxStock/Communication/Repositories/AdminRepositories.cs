using Microsoft.Data.SqlClient;
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

		public ObservableCollection<MainListItems> GetBySearchNameItemMainList(string searchName)
		{
			ObservableCollection<MainListItems> resultCollection = [];
			using var context = new PraxixSkladContext();
			{
				var resultSearchCollection = from dataStock in context.DataStocks
											 join items in context.Items on dataStock.IdItem equals items.IdItem
											 join receipt in context.Receipts on dataStock.IdItemStock equals receipt.IdReceipt
											 where EF.Functions.Like(items.NameItem, "%" + searchName + "%")
											 select new
											 {
												 IdItemStock = dataStock.IdItemStock,
												 IdItem = dataStock.IdItem,
												 NameItem = items.NameItem,
												 UnitMeasure = items.UnitMeasure,
												 RemainingStock = dataStock.RemainingStock,
												 ExpirationDate = receipt.ExprirationDate,
												 DateReceipt = receipt.DateReceipt
											 };
				foreach (var item in resultSearchCollection)
					resultCollection.Add(new MainListItems()
					{
						IdDataStock = item.IdItemStock,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.RemainingStock,
						ExpirationDate = item.ExpirationDate,
						DateReceipt = item.DateReceipt
					});
				return resultCollection;
			}
		}

		public ObservableCollection<MainListItems> GetBySearchRemainingStockMainList(string remainingStock)
		{
			ObservableCollection<MainListItems> resultCollection = [];
			using var context = new PraxixSkladContext();
			{
				var resultSearchCollection = from dataStock in context.DataStocks
											 join items in context.Items on dataStock.IdItem equals items.IdItem
											 join receipt in context.Receipts on dataStock.IdItemStock equals receipt.IdReceipt
											 where EF.Functions.Like(dataStock.RemainingStock.ToString(), remainingStock + "%")
											 select new
											 {
												 IdItemStock = dataStock.IdItemStock,
												 IdItem = dataStock.IdItem,
												 NameItem = items.NameItem,
												 UnitMeasure = items.UnitMeasure,
												 RemainingStock = dataStock.RemainingStock,
												 ExpirationDate = receipt.ExprirationDate,
												 DateReceipt = receipt.DateReceipt
											 };
				foreach (var item in resultSearchCollection)
					resultCollection.Add(new MainListItems()
					{
						IdDataStock = item.IdItemStock,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.RemainingStock,
						ExpirationDate = item.ExpirationDate,
						DateReceipt = item.DateReceipt
					});
				return resultCollection;
			}
		}

		public ObservableCollection<MainListItems> GetBySearchExpirationDateMainList(DateOnly expirationDate)
		{
			throw new NotImplementedException();
		}

		public ObservableCollection<MainListItems> GetBySearchDateReceiptMainList(DateOnly dateReceipt)
		{
			throw new NotImplementedException();
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

		public ObservableCollection<MainListItems> GetDataStockList()
		{
			var mainCollection = new ObservableCollection<MainListItems>();
			using var context = new PraxixSkladContext();
			{
				var result = from dataStoks in context.DataStocks
							 join items in context.Items on dataStoks.IdItem equals items.IdItem
							 join receipt in context.Receipts on dataStoks.IdItemStock equals receipt.IdReceipt
							 select new
							 {
								 IdItemStock = dataStoks.IdItemStock,
								 IdItem = dataStoks.IdItem,
								 NameItem = items.NameItem,
								 UnitMeasure = items.UnitMeasure,
								 RemainingStock = dataStoks.RemainingStock,
								 ExpirationDate = receipt.ExprirationDate,
								 DateReceipt = receipt.DateReceipt
							 };
				foreach (var item in result)
				{
					mainCollection.Add(new MainListItems()
					{
						IdDataStock = item.IdItemStock,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.RemainingStock,
						ExpirationDate= item.ExpirationDate,
						DateReceipt = item.DateReceipt
					});
				}
			}
			return mainCollection;
		}

		public ObservableCollection<ReceiptListItem> GetReseiptList()
		{
			var receiptCollection = new ObservableCollection<ReceiptListItem>();
			using var context = new PraxixSkladContext();
			{
				var result = from receipt in context.Receipts
							 join items in context.Items on receipt.IdItem equals items.IdItem
							 select new
							 {
								 IdReceipt = receipt.IdReceipt,
								 NameItem = items.NameItem,
								 UnitMeasure = items.UnitMeasure,
								 QuantityReceipt = receipt.QuantityReceipt,
								 ExpirationDate = receipt.ExprirationDate,
								 DateReceipt = receipt.DateReceipt
							 };
				foreach (var item in result)
				{
					receiptCollection.Add(new ReceiptListItem()
					{
						IdReceipt = item.IdReceipt,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.QuantityReceipt,
						ExpirationDate = item.ExpirationDate,
						DateReceipt = item.DateReceipt
					});
				}
			}
			return receiptCollection;
		}

		public bool AddMoveInPost(MoveListItem moveListItem)
		{
			try
			{
				using var context = new PraxixSkladContext();
				{
					SqlParameter[] param =
					{
					new ()
					{
						ParameterName = "@idItem",
						SqlDbType = System.Data.SqlDbType.Int,
						Value = moveListItem.IdItem,
					},
					new ()
					{
						ParameterName = "@idItemStock",
						SqlDbType = System.Data.SqlDbType.Int,
						Value = moveListItem.IdDataStock

					},
					new ()
					{
						ParameterName = "@quantityMove",
						SqlDbType = System.Data.SqlDbType.Float,
						Value = moveListItem.UnitCount
					},
					new ()
					{
						ParameterName = "@NamePost",
						SqlDbType = System.Data.SqlDbType.VarChar,
						Size = 50,
						Value = moveListItem.NamePost
					},
					new ()
					{
						ParameterName = "@dateMove",
						SqlDbType = System.Data.SqlDbType.Date,
						Value = moveListItem.DateMove
					},
					new ()
					{
						ParameterName = "@result",
						SqlDbType = System.Data.SqlDbType.Int,
						Direction = System.Data.ParameterDirection.Output
					}
				};
					context.Database.ExecuteSqlRaw(
						"update_dataStock @idItem, @idItemStock, @quantityMove, @NamePost, @dateMove, @result output", param);

					return Convert.ToInt32(param[5].Value) == 1 ? true : false;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public double GetRemainingStock(int idDataStock)
		{
			using var context = new PraxixSkladContext();
			{
				var result = context.DataStocks.Where(id => id.IdItemStock == idDataStock).First();
			
				if(result != null)
					return result.RemainingStock;
				else 
					return 0;
			}
			
		}

		public ObservableCollection<MoveListItem> GetMoveInPostList()
		{
			ObservableCollection<MoveListItem> moveList = new();
			using var context = new PraxixSkladContext();
			{
				var result = from moveInPost in context.MoveInPosts
							 join items in context.Items on moveInPost.IdItem equals items.IdItem
							 select new
							 {
								 IdMove = moveInPost.IdMove,
								 IdItem = moveInPost.IdItem,
								 NameItem = items.NameItem,
								 UnitMeasure = items.UnitMeasure,
								 QuantityMove = moveInPost.QuantityMove,
								 DateMove = moveInPost.DateMove,
								 NamePost = moveInPost.NamePost
							 };
				foreach (var item in result)
					moveList.Add(new MoveListItem
					{
						IdMove = item.IdMove,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,	
						UnitCount = item.QuantityMove,
						DateMove = item.DateMove,
						NamePost = item.NamePost
					});

				return moveList;
			}
		}

	}
}
