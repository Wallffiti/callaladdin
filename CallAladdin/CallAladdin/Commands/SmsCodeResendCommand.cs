using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class SmsCodeResendCommand : ICommand
    {
        private SmsVerificationViewModel smsVerificationViewModel;
        public event EventHandler CanExecuteChanged;

        public SmsCodeResendCommand(SmsVerificationViewModel viewModel)
        {
            smsVerificationViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO
        }
    }
}
