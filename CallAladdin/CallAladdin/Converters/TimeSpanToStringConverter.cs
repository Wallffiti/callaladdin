using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CallAladdin.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var timeSpan = (TimeSpan)value;

            try
            {
                string hours = timeSpan.Hours < 10? "0" + timeSpan.Hours.ToString() : timeSpan.Hours.ToString();
                string minutes = timeSpan.Minutes < 10? "0" + timeSpan.Minutes.ToString() : timeSpan.Minutes.ToString() ;
                return String.Format("{0}:{1}", hours, minutes); //timeSpan.ToString("HH:mm");
            }
            catch (Exception ex)
            {
                
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return default(TimeSpan);

            try
            {
                var tokens = ((string)value).Split(':');
                return new TimeSpan(int.Parse(tokens[0]), int.Parse(tokens[1]), 0);
            }
            catch (Exception ex)
            {

            }

            return default(TimeSpan);
        }
    }
}
