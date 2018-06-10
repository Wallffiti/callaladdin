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
            var strBuilder = new StringBuilder();
            strBuilder.Append("<!DOCTYPE html>")
            .Append("<html>")
            .Append("<head>")
            .Append("<meta charset=utf-8 />")
            .Append("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\">")
            .Append("<title>©2017 Call Aladdin Disclaimer</title>")
            .Append("</head>")
            .Append("<body style=\"background-color: #EAECF5\">")
            .Append("<div style=\"background-color: lightgray; padding: 10px\">")

            //TODO for body

            .Append("</div>")
            .Append("</body>")
            .Append("</html>");

            this.PersonalDataProtectionText = strBuilder.ToString();
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
