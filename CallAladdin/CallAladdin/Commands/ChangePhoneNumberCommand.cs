using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class ChangePhoneNumberCommand : ICommand
    {
        private SmsVerificationViewModel smsVerificationViewModel;
        public event EventHandler CanExecuteChanged;

        public ChangePhoneNumberCommand(SmsVerificationViewModel viewModel)
        {
            smsVerificationViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            smsVerificationViewModel.PromptPhoneNumberChange();
        }
    }
}
