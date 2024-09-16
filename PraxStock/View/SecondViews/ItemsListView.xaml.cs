using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PraxStock.View.SecondViews
{
    /// <summary>
    /// Логика взаимодействия для ItemsListView.xaml
    /// </summary>
    public partial class ItemsListView : Window
    {
        public ItemsListView()
        {
            InitializeComponent();
        }

		private void ButtonReset_Click(object sender, RoutedEventArgs e)
		{
            MainListItem.SelectedItem = null;
            btnSavaAndAdd.Content = "Добавить";
        }

		private void MainListItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			btnSavaAndAdd.Content = "Сохранить";
		}
	}
}
