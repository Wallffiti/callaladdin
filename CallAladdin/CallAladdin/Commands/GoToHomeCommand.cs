using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class GoToHomeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private SmsVerificationViewModel smsVerificationViewModel;

        public GoToHomeCommand(SmsVerificationViewModel viewModel)
        {
            smsVerificationViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            //TODO
        }
    }
}
