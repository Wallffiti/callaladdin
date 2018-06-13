using CallAladdin.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class ChangePhoneNumberViewModel : BaseViewModel
    {
        private SmsVerificationViewModel parentViewModel;
        private string phoneNumber;
        public ICommand SubmitPhoneNumberCmd { get; set; }
        public ICommand CancelPhoneNumberSubmissionCmd { get; set; }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }

        public ChangePhoneNumberViewModel(SmsVerificationViewModel viewModel)
        {
            this.parentViewModel = viewModel;
            this.PhoneNumber = viewModel?.MobileNumber;
            this.SubmitPhoneNumberCmd = new SubmitPhoneNumberCommand(this);
            this.CancelPhoneNumberSubmissionCmd = new CancelPhoneNumberSubmissionCommand(this);
        }

        public void SubmitPhoneNumber()
        {
            Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure you want to change it to " + phoneNumber + "?", "Yes", "No", async () =>
            {
                //For android
                parentViewModel.MobileNumber = phoneNumber;
                await Navigator.Instance.ReturnPrevious(UIPageType.MODAL);
            },
            async ()=>
            {
                //For IOS
                parentViewModel.MobileNumber = phoneNumber;
                await Navigator.Instance.ReturnPrevious(UIPageType.MODAL);
            });
        }

        public void CancelPhoneNumberChange()
        {
            Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure you want to return to previous screen? All changes will be lost.", "Yes", "No", async () =>
            {
                //For android
                await Navigator.Instance.ReturnPrevious(UIPageType.MODAL);
            }, async () =>
            {
                //For IOS
                await Navigator.Instance.ReturnPrevious(UIPageType.MODAL);
            });
        }
    }
}
