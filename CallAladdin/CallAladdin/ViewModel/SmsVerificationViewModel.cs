using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using CallAladdin.Commands;
using Xamarin.Forms;
using System.Threading.Tasks;

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
        private bool isBusy;

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
            this.mobileNumber = userRegistration?.Mobile;
        }

        public async void NavigateToHome()
        {
            //TODO: verify sms code before navigate to home
            if (isBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            isBusy = true;
            var userProfile = GetUserProfile();
            await Navigator.Instance.NavigateTo(PageType.HOME, userProfile, appendFromRoot: true);
            await Task.Delay(1500);
            isBusy = false;
        }

        public async void PromptPhoneNumberChange()
        {
            await Navigator.Instance.NavigateTo(PageType.CHANGE_PHONE_NUMBER, this, uIPageType: UIPageType.MODAL);
        }

        private UserProfile GetUserProfile()
        {
            UserProfile userProfile = null;

            //3. Generate user profile
            if (userRegistration != null)
            {
                userProfile = new UserProfile()
                {
                    Category = userRegistration.Category,
                    City = userRegistration.City,
                    CompanyName = userRegistration.CompanyName,
                    CompanyRegisteredAddress = userRegistration.CompanyAddress,
                    Country = userRegistration.Country,
                    Email = userRegistration.Email,
                    IsContractor = userRegistration.IsRegisteredAsContractor,
                    Mobile = userRegistration.Mobile,
                    Name = userRegistration.Name,
                    PathToProfileImage = userRegistration.ProfileImagePath
                    //TODO: update reviews
                };
            }
            return userProfile;
        }
    }
}
