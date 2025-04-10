using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.OtherModel;
internal class MoveListItem : ItemStock
{
	public int IdMove { get; set; }

	public string? NamePost { get; set; }

	public DateOnly DateMove { get; set; }
}
