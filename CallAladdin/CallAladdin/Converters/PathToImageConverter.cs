using CallAladdin.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CallAladdin.Converters
{
    public class PathToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    if (Validators.ValidateUrl(path))
                    {
                        return ImageSource.FromUri(new Uri(path));
                    }

                    return ImageSource.FromFile(path);
                }
                catch (Exception ex)
                {
                    //if exception caught, then bind to the default image
                }
            }

            return ImageSource.FromResource("CallAladdin.Assets.Images.default_avatar_image.jpeg");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
