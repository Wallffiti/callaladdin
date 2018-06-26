using System;
using System.Diagnostics;
using Xamarin.Forms;
using CallAladdin.Helper.Interfaces;

[assembly: Dependency(typeof(CallAladdin.Droid.AndroidPlatformBasicService))]
namespace CallAladdin.Droid
{
    public class AndroidPlatformBasicService : IPlatformBasicService
    {
        public AndroidPlatformBasicService()
        {
            Debug.WriteLine("Entering AndroidPlatformBasicService constructor");
        }

        public void ExitApplication()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        public string GetPhoneNumber()
        {
            string phoneNumber = "";

            try
            {
                var mgr = Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService) as Android.Telephony.TelephonyManager;

                if (mgr != null)
                {
                    phoneNumber = mgr.Line1Number;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }

            return phoneNumber;
        }
    }
}