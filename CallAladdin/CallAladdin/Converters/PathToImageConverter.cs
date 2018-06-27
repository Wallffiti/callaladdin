using CallAladdin.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                    var param = (string)parameter;
                    if (!string.IsNullOrEmpty(param) && param == "isEmbedded")
                    {
                        return ImageSource.FromResource(path);
                    }

                    if (Validators.ValidateUrl(path))
                    {
                        return ImageSource.FromUri(new Uri(path));
                    }

                    if (File.Exists(path))
                    {
                        return ImageSource.FromFile(path);
                    }
                }
                catch (Exception ex)
                {
                    //if exception caught, then bind to the default image
                }
            }

            //return ImageSource.FromResource("CallAladdin.Assets.Images.default_avatar_image.jpeg");
            return ImageSource.FromResource("CallAladdin.Assets.Images.ic_image.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
