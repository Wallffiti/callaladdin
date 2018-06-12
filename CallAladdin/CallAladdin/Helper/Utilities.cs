using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CallAladdin.Helper
{
    public class Utilities
    {
        public static string GetPhoneNumber()
        {
            string phoneNumber = "";

            if (Device.RuntimePlatform == Device.Android)
            {
                //For android

                Android.Telephony.TelephonyManager mgr = null;

                try
                {
                    mgr = Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService) as Android.Telephony.TelephonyManager;
                }
                catch (Exception ex)
                {

                }
                if (mgr != null)
                {
                    phoneNumber = mgr.Line1Number;
                }
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                //For IOS
            }

            return phoneNumber;
        }
    }
}
