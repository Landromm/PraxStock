
using Microsoft.EntityFrameworkCore;
using PraxStock.Model.DBModels;
using PraxStock.Model.OtherModel;
using PraxStock.Model.OtherModel.StatisticsModel;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PraxStock.Communication.Repositories
{
	class AdminRepositories : IAdminRepositories
	{
		public void AddItemsList(string nameItem, string unitMeasure)
		{
			using var contex = new PraxixSkladContext();
			{
				var tempItem = new Item() { NameItem = nameItem, UnitMeasure = unitMeasure };
				contex.Add(tempItem);
				contex.SaveChanges();
			}
		}

		public bool AddMoveInPost(MoveListItem moveListItem)
		{
			try
			{
				using var context = new PraxixSkladContext();
				{
					var resultUpdate = context.Database.ExecuteSql($"UPDATE DataStock SET RemainingStock = RemainingStock - {moveListItem.UnitCount} WHERE idItemStock = {moveListItem.IdDataStock}");
					if(Convert.ToBoolean(resultUpdate))
					{
						context.Add(new MoveInPost()
						{
							IdItems = moveListItem.IdItem,
							QuantityMove = moveListItem.UnitCount,
							NamePost = moveListItem.NamePost,
							DateMove = moveListItem.DateMove.ToString(),
							IdItemStock = moveListItem.IdDataStock
						});
						return Convert.ToBoolean(context.SaveChanges());
					}
					else
						return false;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
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
					ExpirationDate = receiptListItem.ExpirationDate.ToString(),
					DateReceipt = receiptListItem.DateReceipt.ToString(),
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

		public bool AddReceiptItemSecond(ReceiptListItem receiptListItem, int idStockItem)
		{
			//try
			//{
			//	using var context = new PraxixSkladContext();
			//	{
			//		SqlParameter[] param =
			//		{
			//				new ()
			//				{
			//					ParameterName = "@idItem",
			//					SqlDbType = System.Data.SqlDbType.Int,
			//					Value = receiptListItem.IdItem,
			//				},
			//				new ()
			//				{
			//					ParameterName = "@idItemStock",
			//					SqlDbType = System.Data.SqlDbType.Int,
			//					Value = idStockItem

			//				},
			//				new ()
			//				{
			//					ParameterName = "@quatityReceipt",
			//					SqlDbType = System.Data.SqlDbType.Float,
			//					Value = receiptListItem.UnitCount
			//				},
			//				new ()
			//				{
			//					ParameterName = "@expirrationDate",
			//					SqlDbType = System.Data.SqlDbType.Date,
			//					Value = receiptListItem.ExpirationDate
			//				},
			//				new ()
			//				{
			//					ParameterName = "@dateReceip",
			//					SqlDbType = System.Data.SqlDbType.Date,
			//					Value = receiptListItem.DateReceipt
			//				},
			//				new ()
			//				{
			//					ParameterName = "@result",
			//					SqlDbType = System.Data.SqlDbType.Int,
			//					Direction = System.Data.ParameterDirection.Output
			//				}
			//			};

			//		if (param[3].Value == null)
			//			param[3].Value = DBNull.Value;

			//		context.Database.ExecuteSqlRaw(
			//			"update_dataStock_afterReceipt @idItem, @idItemStock, @quatityReceipt, @expirrationDate, @dateReceip, @result output", param);

			//		return Convert.ToInt32(param[5].Value) == 1 ? true : false;
			//	}
			//}
			//catch (Exception ex)
			//{
			//	return false;
			//}
			return false;
		}

		public bool AddWriteOff(MainListItems writeOffItem)
		{
			//		try
			//		{
			//			using var context = new PraxixSkladContext();
			//			{
			//				SqlParameter[] param =
			//				{
			//				new ()
			//				{
			//					ParameterName = "@idItem",
			//					SqlDbType = System.Data.SqlDbType.Int,
			//					Value = writeOffItem.IdItem,
			//				},
			//				new ()
			//				{
			//					ParameterName = "@idItemStock",
			//					SqlDbType = System.Data.SqlDbType.Int,
			//					Value = writeOffItem.IdDataStock

			//				},
			//				new ()
			//				{
			//					ParameterName = "@countWriteOff",
			//					SqlDbType = System.Data.SqlDbType.Float,
			//					Value = writeOffItem.UnitCount
			//				},
			//				new ()
			//				{
			//					ParameterName = "@dateWriteOff",
			//					SqlDbType = System.Data.SqlDbType.Date,
			//					Value = DateOnly.FromDateTime(DateTime.Now)
			//				},
			//				new ()
			//				{
			//					ParameterName = "@result",
			//					SqlDbType = System.Data.SqlDbType.Int,
			//					Direction = System.Data.ParameterDirection.Output
			//				}
			//			};
			//				context.Database.ExecuteSqlRaw(
			//					"update_dataStock_afterWriteOff @idItem, @idItemStock, @countWriteOff, @dateWriteOff, @result output", param);

			//				return Convert.ToInt32(param[4].Value) == 1 ? true : false;
			//			}
			//		}
			//		catch (Exception ex)
			//		{
			//			return false;
			//		}
			return false;
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

		public ObservableCollection<Item> GetAllItemsList()
		{
			ObservableCollection<Item> tempResult = new();
			using var contex = new PraxixSkladContext();
			{
				var result = contex.Items.ToList();

				foreach (var item in result)
					tempResult.Add(item);
			}
			return tempResult;
		}

		public List<string> GetAllNameItem()
		{
			var result = new List<string>();
			using var context = new PraxixSkladContext();
			{
				result = context.Items
					.OrderBy(x => x.NameItem)
					.Select(n => n.NameItem)
					.ToList();
			}

			return result;
		}

		public List<MainListItems> GetAllNameItemSecond(string nameItem)
		{
			var mainCollection = new List<MainListItems>();
			var dateEx = new DateOnly();

			using var context = new PraxixSkladContext();
			{
				var result = from dataStoks in context.DataStocks
							 join items in context.Items on dataStoks.IdItem equals items.IdItem
							 join receipt in context.Receipts on dataStoks.IdItemStock equals receipt.IdReceipt
							 where items.NameItem == nameItem
							 select new
							 {
								 IdItemStock = dataStoks.IdItemStock,
								 IdItem = dataStoks.IdItem,
								 NameItem = items.NameItem,
								 UnitMeasure = items.UnitMeasure,
								 RemainingStock = dataStoks.RemainingStock,
								 ExpirationDate = receipt.ExpirationDate,
								 DateReceipt = receipt.DateReceipt,
								 MinValue = dataStoks.MinValue,
								 FlagSett = dataStoks.FlagSett
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
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt),
						MinValue = item.MinValue,
						FlagSett = item.FlagSett
					});
				}
			}
			return mainCollection;
		}

		public List<string> GetAllNamePost()
		{
			var result = new List<string>();
			using var context = new PraxixSkladContext();
			{
				result = [.. context.MoveInPosts.Select(nP => nP.NamePost).Distinct()];
			}

			return result;
		}

		public ObservableCollection<MainListItems> GetBySearchDateReceiptMainList(string dateReceipt)
		{
			ObservableCollection<MainListItems> resultCollection = [];
			var dateEx = new DateOnly();
			
			using var context = new PraxixSkladContext();
			{
				var resultSearchCollection = from dataStock in context.DataStocks
											 join items in context.Items on dataStock.IdItem equals items.IdItem
											 join receipt in context.Receipts on dataStock.IdItemStock equals receipt.IdReceipt
											 where EF.Functions.Like(receipt.DateReceipt.ToString(), "%" + dateReceipt + "%")
											 select new
											 {
												 IdItemStock = dataStock.IdItemStock,
												 IdItem = dataStock.IdItem,
												 NameItem = items.NameItem,
												 UnitMeasure = items.UnitMeasure,
												 RemainingStock = dataStock.RemainingStock,
												 ExpirationDate = receipt.ExpirationDate,
												 DateReceipt = receipt.DateReceipt,
												 MinValue = dataStock.MinValue,
												 FlagSett = dataStock.FlagSett
											 };
				foreach (var item in resultSearchCollection)
					resultCollection.Add(new MainListItems()
					{
						IdDataStock = item.IdItemStock,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.RemainingStock,
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt),
						MinValue = item.MinValue,
						FlagSett = item.FlagSett
					});
				return resultCollection;
			}
		}

		public ObservableCollection<MoveListItem> GetBySearchDateReceiptMoveList(string dateMove)
		{
			ObservableCollection<MoveListItem> moveList = new();
			using var context = new PraxixSkladContext();
			{
				var result = from moveInPost in context.MoveInPosts
							 join items in context.Items on moveInPost.IdItems equals items.IdItem
							 where EF.Functions.Like(moveInPost.DateMove.ToString(), "%" + dateMove + "%")
							 select new
							 {
								 IdMove = moveInPost.IdMove,
								 IdItem = moveInPost.IdItems,
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
						DateMove = DateOnly.Parse(item.DateMove),
						NamePost = item.NamePost
					});

				return moveList;
			}
		}

		public ObservableCollection<ReceiptListItem> GetBySearchDateReceiptReceiptList(string dateReceipt)
		{
			var receiptCollection = new ObservableCollection<ReceiptListItem>();
			var dateEx = new DateOnly();
			
			using var context = new PraxixSkladContext();
			{
				var result = from receipt in context.Receipts
							 join items in context.Items on receipt.IdItem equals items.IdItem
							 where EF.Functions.Like(receipt.DateReceipt.ToString(), "%" + dateReceipt + "%")
							 select new
							 {
								 IdReceipt = receipt.IdReceipt,
								 NameItem = items.NameItem,
								 UnitMeasure = items.UnitMeasure,
								 QuantityReceipt = receipt.QuantityReceipt,
								 ExpirationDate = receipt.ExpirationDate,
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
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt)
					});
				}
			}
			return receiptCollection;
		}

		public ObservableCollection<MainListItems> GetBySearchExpirationDateMainList(DateOnly expirationDate)
		{
			ObservableCollection<MainListItems> resultCollection = [];
			var dateEx = new DateOnly();

			using var context = new PraxixSkladContext();
			{
				var resultSearchCollection = from dataStock in context.DataStocks
											 join items in context.Items on dataStock.IdItem equals items.IdItem
											 join receipt in context.Receipts on dataStock.IdItemStock equals receipt.IdReceipt
											 where EF.Functions.Like(receipt.ExpirationDate.ToString(), expirationDate.ToString("yyyy-MM-dd") + "%")
											 select new
											 {
												 IdItemStock = dataStock.IdItemStock,
												 IdItem = dataStock.IdItem,
												 NameItem = items.NameItem,
												 UnitMeasure = items.UnitMeasure,
												 RemainingStock = dataStock.RemainingStock,
												 ExpirationDate = receipt.ExpirationDate,
												 DateReceipt = receipt.DateReceipt,
												 MinValue = dataStock.MinValue,
												 FlagSett = dataStock.FlagSett
											 };
				foreach (var item in resultSearchCollection)
					resultCollection.Add(new MainListItems()
					{
						IdDataStock = item.IdItemStock,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.RemainingStock,
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt),
						MinValue = item.MinValue,
						FlagSett = item.FlagSett
					});
				return resultCollection;
			}
		}

		public int GetBySearchIdItem(string searchName)
		{
			using var context = new PraxixSkladContext();
			{
				try
				{
					var idItem = (from item in context.Items
								  where item.NameItem == searchName
								  select item.IdItem).First();

					return idItem;
				}
				catch (Exception ex)
				{
					return 0;
				}
			}
		}

		public MainListItems GetBySearchIdStockItem(int searchIdStock)
		{
			MainListItems resultCollection = new();
			var dateEx = new DateOnly();

			using var context = new PraxixSkladContext();
			{
				var resultSearchCollection = from dataStock in context.DataStocks
											 join items in context.Items on dataStock.IdItem equals items.IdItem
											 join receipt in context.Receipts on dataStock.IdItemStock equals receipt.IdReceipt
											 where dataStock.IdItemStock == searchIdStock
											 select new
											 {
												 IdItemStock = dataStock.IdItemStock,
												 IdItem = dataStock.IdItem,
												 NameItem = items.NameItem,
												 UnitMeasure = items.UnitMeasure,
												 RemainingStock = dataStock.RemainingStock,
												 ExpirationDate = receipt.ExpirationDate,
												 DateReceipt = receipt.DateReceipt,
												 MinValue = dataStock.MinValue,
												 FlagSett = dataStock.FlagSett
											 };
				foreach (var item in resultSearchCollection)
					resultCollection = new MainListItems()
					{
						IdDataStock = item.IdItemStock,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.RemainingStock,
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt),
						MinValue = item.MinValue,
						FlagSett = item.FlagSett
					};
				return resultCollection;
			}
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

		public ObservableCollection<MainListItems> GetBySearchNameItemMainList(string searchName)
		{
			ObservableCollection<MainListItems> resultCollection = [];
			var dateEx = new DateOnly();

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
												 ExpirationDate = receipt.ExpirationDate,
												 DateReceipt = receipt.DateReceipt,
												 MinValue = dataStock.MinValue,
												 FlagSett = dataStock.FlagSett
											 };
				foreach (var item in resultSearchCollection)
					resultCollection.Add(new MainListItems()
					{
						IdDataStock = item.IdItemStock,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.RemainingStock,
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt),
						MinValue = item.MinValue,
						FlagSett = item.FlagSett
					});
				return resultCollection;
			}
		}

		public ObservableCollection<MoveListItem> GetBySearchNameItemMoveList(string searchName)
		{
			ObservableCollection<MoveListItem> moveList = new();
			using var context = new PraxixSkladContext();
			{
				var result = from moveInPost in context.MoveInPosts
							 join items in context.Items on moveInPost.IdItems equals items.IdItem
							 where EF.Functions.Like(items.NameItem, "%" + searchName + "%")
							 select new
							 {
								 IdMove = moveInPost.IdMove,
								 IdItem = moveInPost.IdItems,
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
						DateMove = DateOnly.Parse(item.DateMove),
						NamePost = item.NamePost
					});

				return moveList;
			}
		}

		public ObservableCollection<ReceiptListItem> GetBySearchNameItemReceiptList(string searchName)
		{
			var receiptCollection = new ObservableCollection<ReceiptListItem>();
			var dateEx = new DateOnly();

			using var context = new PraxixSkladContext();
			{
				var result = from receipt in context.Receipts
							 join items in context.Items on receipt.IdItem equals items.IdItem
							 where EF.Functions.Like(items.NameItem, "%" + searchName + "%")
							 select new
							 {
								 IdReceipt = receipt.IdReceipt,
								 NameItem = items.NameItem,
								 UnitMeasure = items.UnitMeasure,
								 QuantityReceipt = receipt.QuantityReceipt,
								 ExpirationDate = receipt.ExpirationDate,
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
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt)
					});
				}
			}
			return receiptCollection;
		}

		public ObservableCollection<MoveListItem> GetBySearchNamePostMoveList(string namePost)
		{
			ObservableCollection<MoveListItem> moveList = new();
			using var context = new PraxixSkladContext();
			{
				var result = from moveInPost in context.MoveInPosts
							 join items in context.Items on moveInPost.IdItems equals items.IdItem
							 where EF.Functions.Like(moveInPost.NamePost, "%" + namePost + "%")
							 select new
							 {
								 IdMove = moveInPost.IdMove,
								 IdItem = moveInPost.IdItems,
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
						DateMove = DateOnly.Parse(item.DateMove),
						NamePost = item.NamePost
					});

				return moveList;
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

		public ObservableCollection<MainListItems> GetBySearchRemainingStockMainList(string remainingStock)
		{
			ObservableCollection<MainListItems> resultCollection = [];
			var dateEx = new DateOnly();

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
												 ExpirationDate = receipt.ExpirationDate,
												 DateReceipt = receipt.DateReceipt,
												 MinValue = dataStock.MinValue,
												 FlagSett = dataStock.FlagSett
											 };
				foreach (var item in resultSearchCollection)
					resultCollection.Add(new MainListItems()
					{
						IdDataStock = item.IdItemStock,
						IdItem = item.IdItem,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.RemainingStock,
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt),
						MinValue = item.MinValue,
						FlagSett = item.FlagSett
					});
				return resultCollection;
			}
		}

		public ObservableCollection<ReceiptListItem> GetBySearchUnitCountReceiptList(string unitCount)
		{
			var receiptCollection = new ObservableCollection<ReceiptListItem>();
			var dateEx = new DateOnly();
			
			using var context = new PraxixSkladContext();
			{
				var result = from receipt in context.Receipts
							 join items in context.Items on receipt.IdItem equals items.IdItem
							 where EF.Functions.Like(receipt.QuantityReceipt.ToString(), unitCount + "%")
							 select new
							 {
								 IdReceipt = receipt.IdReceipt,
								 NameItem = items.NameItem,
								 UnitMeasure = items.UnitMeasure,
								 QuantityReceipt = receipt.QuantityReceipt,
								 ExpirationDate = receipt.ExpirationDate,
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
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt)
					});
				}
			}
			return receiptCollection;
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

		public ObservableCollection<MainListItems> GetDataStockList()
		{
			var mainCollection = new ObservableCollection<MainListItems>();
			var dateEx = new DateOnly();

			using var context = new PraxixSkladContext();
			{
				var result = from dataStoks in context.DataStocks
							 join items in context.Items on dataStoks.IdItem equals items.IdItem
							 join receipt in context.Receipts on dataStoks.IdItemStock equals receipt.IdReceipt
							 orderby items.NameItem
							 select new
							 {
								 IdItemStock = dataStoks.IdItemStock,
								 IdItem = dataStoks.IdItem,
								 NameItem = items.NameItem,
								 UnitMeasure = items.UnitMeasure,
								 RemainingStock = dataStoks.RemainingStock,
								 ExpirationDate = receipt.ExpirationDate,
								 DateReceipt = receipt.DateReceipt,
								 MinValue = dataStoks.MinValue,
								 FlagSett = dataStoks.FlagSett
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
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt),
						MinValue = item.MinValue,
						FlagSett = item.FlagSett
					});
				}
			}
			return mainCollection;
		}

		// !!! Необходимо обработать исключение !!!
		public ObservableCollection<ExpenseStatisticModel> GetExpenseStatisticModels(DateOnly startDate, DateOnly endDate)
		{
			ObservableCollection<ExpenseStatisticModel> originList = new();
			List<Tuple<int, string, string>> idMoveItems = new();

			try
			{
				using var context = new PraxixSkladContext();
				{
					var result = (from moveInPost in context.MoveInPosts
								  join items in context.Items on moveInPost.IdItems equals items.IdItem
								  select new
								  {
									  IdItem = moveInPost.IdItems,
									  NameItem = items.NameItem,
									  UnitMeasure = items.UnitMeasure
								  }).Distinct();

					foreach (var item in result)
						idMoveItems.Add(Tuple.Create(item.IdItem, item.NameItem, item.UnitMeasure));
				}

				foreach (var itemMove in idMoveItems)
				{
					List<MoveListItem> moveList = new();

					using var context1 = new PraxixSkladContext();
					{
						var result = from moveInPost in context1.MoveInPosts
									 join items in context1.Items on moveInPost.IdItems equals items.IdItem
									 where moveInPost.IdItems == itemMove.Item1 &&
									 (DateOnly.Parse(moveInPost.DateMove!) >= startDate && DateOnly.Parse(moveInPost.DateMove!) <= endDate)
									 select new
									 {
										 IdMove = moveInPost.IdMove,
										 IdItem = moveInPost.IdItems,
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
								DateMove = DateOnly.Parse(item.DateMove),
								NamePost = item.NamePost
							});
					}

					originList.Add(new ExpenseStatisticModel()
					{
						NamePosition = itemMove.Item2,
						UnitMeasure = itemMove.Item3,
						MoveListItems = moveList
					});
				}
				return originList;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public ObservableCollection<MoveListItem> GetMoveInPostList()
		{
			ObservableCollection<MoveListItem> moveList = new();
			using var context = new PraxixSkladContext();
			{
				var result = from moveInPost in context.MoveInPosts
							 join items in context.Items on moveInPost.IdItems equals items.IdItem
							 join dataStock in context.DataStocks on moveInPost.IdItemStock equals dataStock.IdItemStock
							 select new
							 {
								 IdMove = moveInPost.IdMove,
								 IdItemStock = dataStock.IdItemStock,
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
						IdDataStock = item.IdItemStock,
						Name = item.NameItem,
						UnitMeasure = item.UnitMeasure,
						UnitCount = item.QuantityMove,
						DateMove = DateOnly.Parse(item.DateMove), 
						NamePost = item.NamePost
					});

				return moveList;
			}
		}

		public double GetRemainingStock(int idDataStock)
		{
			using var context = new PraxixSkladContext();
			{
				var result = context.DataStocks.Where(id => id.IdItemStock == idDataStock).First();

				if (result != null)
					return result.RemainingStock;
				else
					return 0;
			}
		}

		public ObservableCollection<ReceiptListItem> GetReseiptList()
		{
			var receiptCollection = new ObservableCollection<ReceiptListItem>();
			var dateEx = new DateOnly();

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
								 ExpirationDate = receipt.ExpirationDate,
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
						ExpirationDate = DateOnly.TryParse(item.ExpirationDate, out dateEx) == true ? DateOnly.Parse(item.ExpirationDate) : null,
						DateReceipt = DateOnly.Parse(item.DateReceipt)
					});
				}
			}
			return receiptCollection;
		}

		public bool UpdateControlValueDataStock(int idItem, double minValue)
		{
			try
			{
				using var context = new PraxixSkladContext();
				{
					var targetId = context.DataStocks
									.Where(id => id.IdItemStock == idItem)
									.First();

					using var context2 = new PraxixSkladContext();
					{
						var updatedItem = new DataStock
						{
							IdItemStock = targetId.IdItemStock,
							IdItem = targetId.IdItem,
							RemainingStock = targetId.RemainingStock,
							MinValue = minValue,
							FlagSett = minValue > 0 ? true : false
						};
						context2.Update(updatedItem);
						var result = context2.SaveChanges();
						return Convert.ToBoolean(result);
					}					
				}
			}
			catch (Exception ex)
			{
				return false;
			}			
		}
	}

}
