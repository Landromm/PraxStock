using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel;
internal class ReceiptListItem : ItemStock
{
	public int IdReceipt { get; set; }

	public DateOnly DateReceipt { get; set; }
}
