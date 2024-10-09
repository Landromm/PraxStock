using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
	public MainWindow()
	{
		InitializeComponent();
	}
}

public class StringUpperConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value.ToString().ToUpper();
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}


public class ColorLightConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		SolidColorBrush br = value as SolidColorBrush;

		float coef = 0;

		if (parameter != null)
		{
			switch (parameter.ToString().ToUpper())
			{
				case "DARK":
					coef = 0.8F;
					break;
				case "LIGHT":
					coef = 1.4F;
					break;
				case "SELECT":
					coef = 1.6F;
					break;
				default:
					coef = 1.1F;
					break;
			}
		}
		return ChangeLightness(br.Color, coef);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}

	public static Color ChangeLightness(Color color, float coef)
	{
		return Color.FromArgb(255, (byte)(color.R * coef), (byte)(color.G * coef),
		(byte)(color.B * coef));
	}
}