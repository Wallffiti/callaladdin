using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CallAladdin.Converters
{
    public class JobStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Transparent;

            var job = (Job)value;
            if (job != null)
            {
                var status = job.Status;

                if (status == Constants.PENDING)
                {
                    if (DateTime.Now.Subtract(job.CreatedDateTime).Days >= Constants.JOB_REQUEST_EXPIRY_DURATION_IN_DAYS)
                    {
                        return Color.Red;
                    }
                }
                else if (status == Constants.SUSPENDED)
                {
                    return Color.Black;
                }
                else if (status == Constants.FOUND_CONTRACTOR)
                {
                    return Color.Green;
                }
                else if (status == Constants.COMPLETED)
                {
                    return Color.Green;
                }
            }

            return Color.Blue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
