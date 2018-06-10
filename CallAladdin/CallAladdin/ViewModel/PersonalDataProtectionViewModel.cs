using CallAladdin.Commands;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class PersonalDataProtectionViewModel : BaseViewModel
    {
        public ICommand GoToVerificationCmd { get; set; }
        public event EventHandler AcceptAgreementEvent;
        private string personalDataProtectionText;

        public string PersonalDataProtectionText
        {
            get { return personalDataProtectionText; }
            set { personalDataProtectionText = value; OnPropertyChanged("PersonalDataProtectionText"); }
        }


        public PersonalDataProtectionViewModel(UserRegistration userRegistration)
        {
            GoToVerificationCmd = new GoToVerificationCommand(this);
        }

        public async void NavigateToSmsVerification(UserRegistration userRegistration)
        {
            await Navigator.Instance.NavigateTo(PageType.SMS_VERIFICATION, userRegistration);
        }

        public async void NavigateToHome(UserRegistration userRegistration)
        {
            await Navigator.Instance.NavigateTo(PageType.HOME, userRegistration);
        }

        public void NotifyViewOnConfirmation()
        {
            if (this.AcceptAgreementEvent != null)
            {
                this.AcceptAgreementEvent(this, null);
            }
        }
    }
}
