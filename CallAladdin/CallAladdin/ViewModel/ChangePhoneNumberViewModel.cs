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

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }

        public ChangePhoneNumberViewModel(SmsVerificationViewModel viewModel)
        {
            this.parentViewModel = viewModel;
            this.PhoneNumber = viewModel.MobileNumber;
            this.SubmitPhoneNumberCmd = new SubmitPhoneNumberCommand(this);
        }

        public void SubmitPhoneNumber()
        {
            parentViewModel.MobileNumber = phoneNumber;
            Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure you want to change it to " + phoneNumber + " ?", "Yes", "No", async () =>
            {
                //For android
                await Navigator.Instance.ReturnPrevious(UIPageType.MODAL);
            },
            async ()=>
            {
                //For IOS
                await Navigator.Instance.ReturnPrevious(UIPageType.MODAL);
            });
        }
    }
}
