using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.Converter
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (TimeSpan)value;

            return $"{time.Hours.ToString().PadLeft(2, '0')}:" +
                $"{time.Minutes.ToString().PadLeft(2, '0')}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.Zero;
        }
    }
}
