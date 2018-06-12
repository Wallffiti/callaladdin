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
        public ICommand SmsCodeResendCmd { get; set; }
        public ICommand ChangePhoneNumberCmd { get; set; }
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
            this.SmsCodeResendCmd = new SmsCodeResendCommand(this);
            this.ChangePhoneNumberCmd = new ChangePhoneNumberCommand(this);
            this.userRegistration = userRegistration;

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
                    this.MobileNumber = string.IsNullOrEmpty(mgr.Line1Number) ? "Unknown" : mgr.Line1Number;
                }
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                //For IOS
            }
        }

        public async void NavigateToHome(/*UserRegistration userRegistration*/)
        {
            //TODO: verify sms code before navigate to home
            await Navigator.Instance.NavigateTo(PageType.HOME, userRegistration, appendFromRoot: true);
        }

        public async void PromptPhoneNumberChange()
        {
            await Navigator.Instance.NavigateTo(PageType.CHANGE_PHONE_NUMBER, this, uIPageType: UIPageType.MODAL);
        }
    }
}
