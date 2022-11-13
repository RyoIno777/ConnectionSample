using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ConnectionSample.Core.Converters
{
    /// <summary>
    /// 列挙型の値をbool型に変換するコンバータクラスです。
    /// </summary>
    public class EnumToBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;

            if (parameterString == null || !Enum.IsDefined(value.GetType(), value))
            {
                return DependencyProperty.UnsetValue;
            }
            var parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;
            if (parameterString == null)
            {
                return DependencyProperty.UnsetValue;
            }

            if (true.Equals(value))
            {
                return Enum.Parse(targetType, parameterString);
            }
            return DependencyProperty.UnsetValue;
        }

    }
}
