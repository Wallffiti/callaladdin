using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CallAladdin.Converters
{
    public class JobStatusDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var job = (Job)value;
            if (job != null)
            {
                var status = job.Status;

                if (status == Constants.PENDING)
                {
                    if (DateTime.Now.Subtract(job.CreatedDateTime).Days >= Constants.JOB_REQUEST_EXPIRY_DURATION_IN_DAYS) {
                        return Constants.EXPIRED_NO_CONTRACTOR_FOUND;
                    }
                }
                else if (status == Constants.SUSPENDED)
                {
                    return Constants.JOB_SUSPENDED;
                }
                else if (status == Constants.FOUND_CONTRACTOR)
                {
                    return Constants.CONTRACTOR_FOUND;
                }
                else if (status == Constants.COMPLETED)
                {
                    return Constants.JOB_COMPLETED;
                }
                else
                {
                    return status;
                }
            }

            return Constants.UNKNOWN_STATUS;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
