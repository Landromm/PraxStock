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
    /// Логика взаимодействия для MoveAddView.xaml
    /// </summary>
    public partial class MoveAddView : Window
    {
        public MoveAddView()
        {
            InitializeComponent();
        }

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
            Thread.Sleep(500);
            this.Close();
		}
	}
}
