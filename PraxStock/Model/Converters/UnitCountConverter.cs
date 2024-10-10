using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PraxStock.Model.Converters
{
	public class UnitCountConverter : IMultiValueConverter
	{

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (System.Convert.ToDouble(values[0]) <= System.Convert.ToDouble(values[1]))
					return true;
				return false;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
