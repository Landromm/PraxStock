using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using PraxStock.Model.OtherModel.StatisticsModel;
using System.Collections.ObjectModel;
using PraxStock.Model.OtherModel;
using System.Drawing;

namespace PraxStock.Services;
internal class ReportExcel : DialogViewModel, IReportExcel
{
	private string? pathFolder = "";
	private FileInfo fileInfo;

	#region StartDate : DateTime - Начальная дата формирования отчета.

	/// <summary>Начальная дата формирования отчета. - поле.</summary>
	private DateTime _StartDate;

	/// <summary>Начальная дата формирования отчета. - свойство.</summary>
	public DateTime StartDate
	{
		get => _StartDate;
		set
		{
			_StartDate = value;
			OnPropertyChanged(nameof(StartDate));
		}
	}
	#endregion

	#region EndDate : DateTime - Конечная дата формирования отчета.

	/// <summary>Конечная дата формирования отчета. - поле.</summary>
	private DateTime _EndDate;

	/// <summary>Конечная дата формирования отчета. - свойство.</summary>
	public DateTime EndDate
	{
		get => _EndDate;
		set
		{
			_EndDate = value;
			OnPropertyChanged(nameof(EndDate));
		}
	}
	#endregion

	public ReportExcel()
	{
		fileInfo = new FileInfo($"Отчет по остаткам материалов на {DateOnly.FromDateTime(DateTime.Now)}.xlsx");
	}

	public ReportExcel(DateTime startdate, DateTime endTime)
	{
		StartDate = startdate;
		EndDate = endTime;
		fileInfo = new FileInfo($"Расходный отчет материалов за {DateOnly.FromDateTime(StartDate)}-{DateOnly.FromDateTime(EndDate)}.xlsx");
	}

	public string? PathFolderSaveFileMergeDate(string fileName)
	{
		//FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
		//if (directchoosedlg.ShowDialog() == DialogResult.OK)
		//{
		//	return pathFolder = directchoosedlg.SelectedPath + fileInfo.Name;
		//}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = $"{fileName} за {DateOnly.FromDateTime(StartDate)}-{DateOnly.FromDateTime(EndDate)}";
		saveFileDialog.Filter = "Книга Excel (*.xlsx)|*.xlsx|Книга Excel 97-2003 (*.xls)|*.xls";
		if (saveFileDialog.ShowDialog() == true)
			return pathFolder = saveFileDialog.FileName;

		return "";
	}

	public string? PathFolderSaveFileNowDate(string fileName)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = $"{fileName} по состоянию на  {DateOnly.FromDateTime(DateTime.Now)}";
		saveFileDialog.Filter = "Книга Excel (*.xlsx)|*.xlsx|Книга Excel 97-2003 (*.xls)|*.xls";
		if (saveFileDialog.ShowDialog() == true)
			return pathFolder = saveFileDialog.FileName;

		return "";
	}

	/// <summary>
	/// Проверяет наличие файла.
	/// </summary>
	public void CheckExistsFile()
	{
		fileInfo = new FileInfo(pathFolder!);

		if (fileInfo.Exists) //Если файл существует -> удалить его.
		{ 
			fileInfo.Delete();
			fileInfo = new FileInfo(pathFolder!);
		}
	}

	/// <summary>
	/// Открывает сгенерированный отчет.
	/// </summary>
	public void OpenReportFile()
	{
		string commandText = fileInfo.FullName;
		var proc = new Process();
		proc.StartInfo.FileName = commandText;
		proc.StartInfo.UseShellExecute = true;
		proc.Start();
	}

	/// <summary>
	/// Метод генерации краткого расходного отчета по материалам.
	/// </summary>
	/// <param name="StatisticMainCollection">Составная коллекция израсходованных материалов.</param>
	public void GenerationReport(ObservableCollection<ExpenseStatisticModel> StatisticMainCollection)
	{
		CheckExistsFile();

		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

		using (var package = new ExcelPackage())
		{			
			
			//Добавление новой страницы в пустую книгу.
			var worksheet = package.Workbook.Worksheets.Add("Отчет по расходам.");
			
			//Добавление заголовка.
			worksheet.Cells[1, 1, 1, 4].Merge = true;
			worksheet.Cells[1, 1].Value = "Отчет по расходам материалов";
			worksheet.Cells[2, 1, 2, 4].Merge = true;
			worksheet.Cells[2, 1].Value = $"c {DateOnly.FromDateTime(StartDate)} по {DateOnly.FromDateTime(EndDate)}";
			worksheet.Cells[2, 1].Style.Numberformat.Format = "dd/mm/yyyy;@";
			worksheet.Cells[3, 2].Value = "Наименование";
			worksheet.Cells[3, 3].Value = "Ед.изм.";
			worksheet.Cells[3, 4].Value = "Списано (Сумма)";

			//Отрисовка линий-border.
			using (var range = worksheet.Cells[1, 1, 3 + StatisticMainCollection.Count, 4])
			{
				range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
				range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
				range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
				range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
			}

			//Add some items...
			for (int i = 0; i < StatisticMainCollection.Count; i++)
			{
				worksheet.Cells[4 + i, 1].Value = i+1;
				worksheet.Cells[4 + i, 2].Value = StatisticMainCollection[i].NamePosition;
				worksheet.Cells[4 + i, 3].Value = StatisticMainCollection[i].UnitMeasure;
				worksheet.Cells[4 + i, 4].Value = StatisticMainCollection[i].MoveInPostSumm;
			}

			// Общий стиль для всех ячеек.
			using (var range = worksheet.Cells[1, 1, 3 + StatisticMainCollection.Count, 4])
			{
				range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				range.Style.Font.Name = "Arial";
			}

			worksheet.Cells[1, 1, 3, 4].Style.Border.Top.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 1, 3, 4].Style.Border.Right.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 1, 3, 4].Style.Font.Italic = true;
			worksheet.Cells[1, 1, 3, 4].Style.Font.Bold = true;
			worksheet.Cells[3, 1, 3, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 4, StatisticMainCollection.Count + 3, 4].Style.Border.Right.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[StatisticMainCollection.Count + 3, 1, StatisticMainCollection.Count + 3, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 1, StatisticMainCollection.Count + 3, 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
			worksheet.Cells.AutoFitColumns();  //Autofit columns for all cells

			//worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
			//worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

			// Change the sheet view to show it in page layout mode
			worksheet.View.PageLayoutView = false;

			// Set some custom property values
			package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Igar Radkevich");

			// Save our new workbook in the output directory and we are done!
			package.SaveAs(fileInfo);
		}
	}

	/// <summary>
	/// Метод генерации подробного расходного отчета по материалам.
	/// </summary>
	/// <param name="StatisticMainCollection">Составная коллекция израсходованных материалов.</param>
	/// <param name="statusExport">Флаг активации подробного отчета по израсходованным материалам.</param>
	public void GenerationReport(ObservableCollection<ExpenseStatisticModel> StatisticMainCollection, bool statusExport)
	{
		CheckExistsFile();

		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

		using (var package = new ExcelPackage())
		{
			//Добавление новой страницы в пустую книгу.
			var worksheet = package.Workbook.Worksheets.Add("Отчет по расходам.");

			//Добавление заголовка.
			worksheet.Cells[1, 1, 1, 6].Merge = true;
			worksheet.Cells[1, 1].Value = "Отчет по расходам материалов";
			worksheet.Cells[2, 1, 2, 6].Merge = true;
			worksheet.Cells[2, 1].Value = $"c {DateOnly.FromDateTime(StartDate)} по {DateOnly.FromDateTime(EndDate)}";
			worksheet.Cells[2, 1].Style.Numberformat.Format = "dd/mm/yyyy;@";
			worksheet.Cells[3, 2].Value = "Наименование";
			worksheet.Cells[3, 3].Value = "Ед.изм.";
			worksheet.Cells[3, 4].Value = "Списано (Сумма)";
			worksheet.Cells[3, 1, 3, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
			worksheet.Cells[3, 1, 3, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;


			var indexRow = 0;
			var headerConst = 4;
			
			//Add some items...
			for (int i = 0; i < StatisticMainCollection.Count; i++)
			{
				worksheet.Cells[headerConst + indexRow, 1].Value = i + 1;
				worksheet.Cells[headerConst + indexRow, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
				worksheet.Cells[headerConst + indexRow, 2].Value = StatisticMainCollection[i].NamePosition;
				worksheet.Cells[headerConst + indexRow, 3].Value = StatisticMainCollection[i].UnitMeasure;
				worksheet.Cells[headerConst + indexRow, 4].Value = StatisticMainCollection[i].MoveInPostSumm;

				using (var range = worksheet.Cells[headerConst + indexRow, 1, headerConst + indexRow, 6])
				{
					range.Style.Fill.PatternType = ExcelFillStyle.Solid;
					range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 224, 180));
					range.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
					range.Style.Border.Top.Style = ExcelBorderStyle.Double;
					range.Style.Font.Size = 12;
				}

				indexRow++;
				worksheet.Cells[headerConst + indexRow, 1].Value = "№ id";
				worksheet.Cells[headerConst + indexRow, 2].Value = "Наименование";
				worksheet.Cells[headerConst + indexRow, 3].Value = "Ед.изм.";
				worksheet.Cells[headerConst + indexRow, 4].Value = "Количество";
				worksheet.Cells[headerConst + indexRow, 5].Value = "Дата перемещения на пост";
				worksheet.Cells[headerConst + indexRow, 6].Value = "Пост";
				worksheet.Cells[headerConst + indexRow, 1, headerConst + indexRow, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


				using (var range = worksheet.Cells[headerConst + indexRow, 1, headerConst + indexRow, 6])
				{
					range.Style.Font.Italic = true;
				}

				var startIndexFill = headerConst + indexRow;
				for (int j = 0;  j < StatisticMainCollection[i].MoveListItems.Count; j++)
				{
					indexRow++;
					MoveListItem moveListItem = StatisticMainCollection[i].MoveListItems[j];
					worksheet.Cells[headerConst + indexRow, 1].Value = moveListItem.IdMove;
					worksheet.Cells[headerConst + indexRow, 2].Value = moveListItem.Name;
					worksheet.Cells[headerConst + indexRow, 3].Value = moveListItem.UnitMeasure;
					worksheet.Cells[headerConst + indexRow, 4].Value = moveListItem.UnitCount;
					worksheet.Cells[headerConst + indexRow, 5].Value = moveListItem.DateMove;
					worksheet.Cells[headerConst + indexRow, 5].Style.Numberformat.Format = "dd/mm/yyyy;@";
					worksheet.Cells[headerConst + indexRow, 6].Value = moveListItem.NamePost;
				}

				using (var range = worksheet.Cells[startIndexFill, 1, headerConst + indexRow, 6])
				{
					range.Style.Fill.PatternType = ExcelFillStyle.Solid;
					range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 255));
					range.Style.Font.Size = 10;
				}
				indexRow++;
			}

			// Общий стиль для всех ячеек.
			using (var range = worksheet.Cells[1, 1, headerConst + indexRow, 6])
			{
				range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				range.Style.Font.Name = "Arial";
			}


			worksheet.Cells[1, 1, 3, 6].Style.Font.Italic = true;
			worksheet.Cells[1, 1, 3, 6].Style.Font.Bold = true;
			worksheet.Cells[1, 1, 1, 6].Style.Border.Top.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 6, indexRow + 3, 6].Style.Border.Right.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[indexRow + 3, 1, indexRow + 3, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 1, indexRow + 3, 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
			worksheet.Cells.AutoFitColumns();  //Autofit columns for all cells

			// Change the sheet view to show it in page layout mode
			worksheet.View.PageLayoutView = false;

			// Set some custom property values
			package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Igar Radkevich");

			// Save our new workbook in the output directory and we are done!
			package.SaveAs(fileInfo);
		}
	}

	public void GenarationReport(ObservableCollection<DataStocksStatisticModel> dataStocksStatisticModels)
	{
		CheckExistsFile();

		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

		using (var package = new ExcelPackage())
		{

			//Добавление новой страницы в пустую книгу.
			var worksheet = package.Workbook.Worksheets.Add("Отчет по остаткам");

			//Добавление заголовка.
			worksheet.Cells[1, 1, 1, 7].Merge = true;
			worksheet.Cells[1, 1].Value = "Отчет по остаткам материалов";
			worksheet.Cells[2, 1, 2, 7].Merge = true;
			worksheet.Cells[2, 1].Value = $"на {DateOnly.FromDateTime(DateTime.Now)}";
			worksheet.Cells[2, 1].Style.Numberformat.Format = "dd/mm/yyyy;@";
			worksheet.Cells[3, 1].Value = "№ п/п";
			worksheet.Cells[3, 2].Value = "№ id";
			worksheet.Cells[3, 3].Value = "Наименование";
			worksheet.Cells[3, 4].Value = "Ед.измер";
			worksheet.Cells[3, 5].Value = "Остаток";
			worksheet.Cells[3, 6].Value = "Срок годности";
			worksheet.Cells[3, 7].Value = "Дата поступления";

			//Отрисовка линий-border.
			using (var range = worksheet.Cells[1, 1, 3 + dataStocksStatisticModels.Count, 7])
			{
				range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
				range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
				range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
				range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
			}

			//Add some items...
			for (int i = 0; i < dataStocksStatisticModels.Count; i++)
			{
				worksheet.Cells[4 + i, 1].Value = i + 1;
				worksheet.Cells[4 + i, 2].Value = dataStocksStatisticModels[i].IdDataStock;
				worksheet.Cells[4 + i, 3].Value = dataStocksStatisticModels[i].Name;
				worksheet.Cells[4 + i, 4].Value = dataStocksStatisticModels[i].UnitMeasure;
				worksheet.Cells[4 + i, 5].Value = dataStocksStatisticModels[i].UnitCount;
				worksheet.Cells[4 + i, 6].Style.Numberformat.Format = "dd/mm/yyyy;@";
				worksheet.Cells[4 + i, 7].Style.Numberformat.Format = "dd/mm/yyyy;@";
				worksheet.Cells[4 + i, 6].Value = dataStocksStatisticModels[i].ExpirationDate;
				worksheet.Cells[4 + i, 7].Value = dataStocksStatisticModels[i].DateReceipt;	
			}

			// Общий стиль для всех ячеек.
			using (var range = worksheet.Cells[1, 1, 3 + dataStocksStatisticModels.Count, 7])
			{
				range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				range.Style.Font.Name = "Arial";
			}

			worksheet.Cells[1, 1, 3, 7].Style.Border.Top.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 1, 3, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 1, 3, 7].Style.Font.Italic = true;
			worksheet.Cells[1, 1, 3, 7].Style.Font.Bold = true;
			worksheet.Cells[3, 1, 3, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 4, dataStocksStatisticModels.Count + 3, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[dataStocksStatisticModels.Count + 3, 1, dataStocksStatisticModels.Count + 3, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
			worksheet.Cells[1, 1, dataStocksStatisticModels.Count + 3, 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
			worksheet.Cells.AutoFitColumns();  //Autofit columns for all cells

			//worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
			//worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

			// Change the sheet view to show it in page layout mode
			worksheet.View.PageLayoutView = false;

			// Set some custom property values
			package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Igar Radkevich");

			// Save our new workbook in the output directory and we are done!
			package.SaveAs(fileInfo);
		}
	}
}
