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
		enum MyEnum
		{
			Green = 1,
			Red = 2,
			Gray = 3
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if(System.Convert.ToBoolean(values[2])) 
				{
					if (System.Convert.ToDouble(values[0]) <= System.Convert.ToDouble(values[1])
						&& System.Convert.ToDouble(values[0]) >= 0)
						return MyEnum.Green;
					return MyEnum.Red;
				}
				return MyEnum.Gray;
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
