using PraxStock.Model.OtherModel.StatisticsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Services;
internal interface IReportExcel
{
	string? PathFolderSaveFile();
	/// <summary>
	/// Проверка существования директории. Если директории НЕТ, то создает её.
	/// </summary>
	void CheckExistsFile();
	void OpenReportFile();
	void GenerationReport(ObservableCollection<ExpenseStatisticModel> StatisticMainCollection);
}
