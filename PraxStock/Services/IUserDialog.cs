using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Services;
public interface IUserDialog
{
	void OpenMainWindow();
	void OpenSettingsWindow();
	void OpenItemsListWindow();
	void OpenAddReceiptWindow();
	void OpenMoveAddWindow();
	void OpenStatisticsMainWindow();
	void OpenDataStocksStatisticsWindow();
}
