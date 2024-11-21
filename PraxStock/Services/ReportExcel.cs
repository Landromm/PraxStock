using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PraxStock.Services;
internal class ReportExcel : IReportExcel
{
	private string? pathFolder = "";
	public string? PathFolderSaveFile()
	{
		FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
		if (directchoosedlg.ShowDialog() == DialogResult.OK)
		{
			return pathFolder = directchoosedlg.SelectedPath;
		}
		return "";
	}
}
