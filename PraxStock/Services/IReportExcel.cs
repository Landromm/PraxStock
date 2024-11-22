using System;
using System.Collections.Generic;
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
}
