using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Plugin.Media;
using System.Threading.Tasks;
using System.IO;
using CallAladdin.Helper.Interfaces;

namespace CallAladdin.Helper
{
    public class Utilities
    {
        public static byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string GetPhoneNumber()
        {
            string phoneNumber = "";

            //if (Device.RuntimePlatform == Device.Android)
            //{
            //    //For android

            //    Android.Telephony.TelephonyManager mgr = null;

            //    try
            //    {
            //        mgr = Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService) as Android.Telephony.TelephonyManager;
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //    if (mgr != null)
            //    {
            //        phoneNumber = mgr.Line1Number;
            //    }
            //}
            //else if (Device.RuntimePlatform == Device.iOS)
            //{
            //    //For IOS
            //}

            var platformBasicService = DependencyService.Get<IPlatformBasicService>(DependencyFetchTarget.NewInstance);

            if (platformBasicService != null)
            {
                phoneNumber = platformBasicService.GetPhoneNumber();
            }

            return phoneNumber;
        }

        public static async Task<string> PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No PickPhoto available.", "OK", "Cancel");
                return null;
            }

            var mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (mediaFile == null)
                return null;

            var filePath = mediaFile.Path;

            //var imgStream = ImageSource.FromStream(() =>
            //{
            //    return mediaFile.GetStream();
            //});

            return filePath;
        }

        public static async Task<string> TakePhoto(string fileIdentifier)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera available.", "OK", "Cancel");
                return null;
            }

            var mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                //Directory = "UserProfile",
                Name = fileIdentifier + ".jpg"
            });

            if (mediaFile == null)
                return null;

            var filePath = mediaFile.Path;

            return filePath;
        }
    }
}
