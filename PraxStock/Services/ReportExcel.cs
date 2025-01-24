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

	public ReportExcel(DateTime startdate, DateTime endTime)
	{
		StartDate = startdate;
		EndDate = endTime;
		fileInfo = new FileInfo($"Расходный отчет материалов за {DateOnly.FromDateTime(StartDate)}-{DateOnly.FromDateTime(EndDate)}.xlsx");
	}

	public string? PathFolderSaveFile()
	{
		//FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
		//if (directchoosedlg.ShowDialog() == DialogResult.OK)
		//{
		//	return pathFolder = directchoosedlg.SelectedPath + fileInfo.Name;
		//}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = $"Отчет по остаткам за {DateOnly.FromDateTime(StartDate)}-{DateOnly.FromDateTime(EndDate)}";
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
	/// Метод генерации расходного отчета по материалам.
	/// </summary>
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
			worksheet.Cells[2, 1].Style.Numberformat.Format = "d/mm/yyyy;@";
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
