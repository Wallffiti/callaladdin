//using Android.Content;
//using Android.Telephony;
//using Android.App;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using CallAladdin.Commands;
using Xamarin.Forms; 

namespace CallAladdin.ViewModel
{
    public class SmsVerificationViewModel : BaseViewModel
    {
        public ICommand GoToHomeCmd { get; set; }
        private string smsCode;
        private UserRegistration userRegistration;

        public string SmsCode
        {
            get { return smsCode; }
            set { smsCode = value; OnPropertyChanged("SmsCode"); }
        }

        private string mobileNumber;

        public string MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; OnPropertyChanged("MobileNumber"); }
        }


        public SmsVerificationViewModel(UserRegistration userRegistration)
        {
            this.GoToHomeCmd = new GoToHomeCommand(this);
            if (Device.RuntimePlatform == Device.Android)
            {
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
                    this.MobileNumber = string.IsNullOrEmpty(mgr.Line1Number) ? "Unknown" : mgr.Line1Number;
                }
            }
        }

        public async void NavigateToHome(/*UserRegistration userRegistration*/)
        {
            await Navigator.Instance.NavigateTo(PageType.HOME, userRegistration);
        }
    }
}
