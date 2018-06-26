using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CallAladdin.Helper.Interfaces;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CallAladdin.iOS.IosPlatformBasicService))]
namespace CallAladdin.iOS
{
    public class IosPlatformBasicService : IPlatformBasicService
    {
        public IosPlatformBasicService()
        {
            Debug.WriteLine("Entering IosPlatformBasicService constructor");
        }

        public void ExitApplication()
        {
            throw new NotImplementedException();
        }

        public string GetPhoneNumber()
        {
            throw new NotImplementedException();
        }
    }
}