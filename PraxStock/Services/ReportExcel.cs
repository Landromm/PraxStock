using Microsoft.Win32;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
		FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
		if (directchoosedlg.ShowDialog() == DialogResult.OK)
		{
			return pathFolder = directchoosedlg.SelectedPath + fileInfo.Name;
		}
		return "";
	}


	public void CheckExistsFile()
	{
		if (fileInfo.Exists) //Если файл существует -> удалить его.
			fileInfo.Delete();
		fileInfo = new FileInfo(pathFolder);
	}
}
