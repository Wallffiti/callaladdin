﻿using System;
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
        public static string GetIconByCategory(string category)
        {
            var path = "CallAladdin.Assets.Images.default_avatar_image.jpeg";

            switch (category)
            {
                case Constants.AIR_CONDITIONER:
                    path = "CallAladdin.Assets.Images.Contractors.AIR_CONDITIONER.png";
                    break;
                case Constants.ALARM_CCTV:
                    path = "CallAladdin.Assets.Images.Contractors.ALARM_N_CCTV.png";
                    break;
                case Constants.ALUMINIUM_GLASSWORKS:
                    path = "CallAladdin.Assets.Images.Contractors.ALUMINIUM_N_GLASS_WORKS.png";
                    break;
                case Constants.CLEANING_SERVICES:
                    path = "CallAladdin.Assets.Images.Contractors.CLEANING_SERVICES.png";
                    break;
                case Constants.CONSTRUCTION_TILING_PAINTING:
                    path = "CallAladdin.Assets.Images.Contractors.CONSTRUCTION_TILING_N_PAINTING.png";
                    break;
                case Constants.CURTAIN_CARPET_WALLPAPER:
                    path = "CallAladdin.Assets.Images.Contractors.CURTAIN_CARPET_N_WALLPAPER.png";
                    break;
                case Constants.FENGSHUI_CONSULTATION:
                    path = "CallAladdin.Assets.Images.Contractors.FENGSHUI_CONSULTATION.png";
                    break;
                case Constants.GATES_STEEL_WORKS:
                    path = "CallAladdin.Assets.Images.Contractors.GATES_N_STEEL_WORKS.png";
                    break;
                case Constants.GENERAL_WORKERS:
                    path = "CallAladdin.Assets.Images.Contractors.GENERAL_WORKERS.png";
                    break;
                case Constants.HOUSE_MOVERS:
                    path = "CallAladdin.Assets.Images.Contractors.HOUSE_MOVERS.png";
                    break;
                case Constants.INTERIOR_DESIGN_CARPENTERS:
                    path = "CallAladdin.Assets.Images.Contractors.INTERIOR_DESIGN_N_CARPENTERS.png";
                    break;
                case Constants.LAMINATED_TIMBER_FLOORING:
                    path = "CallAladdin.Assets.Images.Contractors.LAMINATED_TIMBER_FLOORING.png";
                    break;
                case Constants.LANDSCAPING_POND:
                    path = "CallAladdin.Assets.Images.Contractors.LANDSCAPING_N_POND.png";
                    break;
                case Constants.PEST_CONTROL:
                    path = "CallAladdin.Assets.Images.Contractors.PEST_CONTROL.png";
                    break;
                case Constants.PLASTERED_CEILING:
                    path = "CallAladdin.Assets.Images.Contractors.PLASTERED_CEILING.png";
                    break;
                case Constants.PLUMBER:
                    path = "CallAladdin.Assets.Images.Contractors.PLUMBER.png";
                    break;
                case Constants.SIGNBOARD:
                    path = "CallAladdin.Assets.Images.Contractors.SIGNBOARD.png";
                    break;
                case Constants.WIRING_LIGHING:
                    path = "CallAladdin.Assets.Images.Contractors.WIRING_N_LIGHTING.png";
                    break;
            }

            return path;
        }

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
