using PraxStock.Model.OtherModel;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PraxStock.View.MainViews;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private MainListItems _mainListItems;

	public MainWindow()
	{
		InitializeComponent();
		_mainListItems = new();
	}

	private void CopyInBuffer_Click(object sender, RoutedEventArgs e)
	{
		CopyToCsv();
	}

	private void dataStockListMain_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
	{
		if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.C)
			CopyToCsv();
	}

	private void CopyToCsv()
	{
		if (dataStockListMain.SelectedItems != null)
		{
			System.Windows.Clipboard.Clear();
			string? str = "";
			for (int i = 0; i < dataStockListMain.SelectedItems.Count; i++)
			{
				var convertObject = (MainListItems)dataStockListMain.SelectedItems[i]!;
				str += convertObject!.Name;
				str += "\t";
				str += convertObject.UnitCount;
				str += "\t";
				str += convertObject.UnitCount;
				if (i != dataStockListMain.SelectedItems.Count - 1)
					str += "\n";
			}
			System.Windows.Clipboard.SetText(str);
		}
	}
}