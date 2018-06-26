using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.Converter
{
    public class SecondToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double.TryParse(value.ToString(), out double secs);
            var time = TimeSpan.FromSeconds(secs);

            string duration;
            if (secs < 3600)
            {
                duration = $"{time.Minutes.ToString().PadLeft(2, '0')}:" +
                           $"{time.Seconds.ToString().PadLeft(2, '0')}";
            }
            else
            {
                duration = $"{time.Hours.ToString().PadLeft(2, '0')}:" +
                           $"{time.Minutes.ToString().PadLeft(2, '0')}:" +
                           $"{time.Seconds.ToString().PadLeft(2, '0')}";
            }
            
            return duration;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = value.ToString().Split(':');

            double secs;
            if (time.Length == 2)
            {
                secs = new TimeSpan(0, int.Parse(time[0]), int.Parse(time[1])).TotalSeconds;
            }
            else if (time.Length == 3)
            {
                secs = new TimeSpan(int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2])).TotalSeconds;
            }
            else
            {
                secs = 0;
            }

            return secs;
        }
    }
}
