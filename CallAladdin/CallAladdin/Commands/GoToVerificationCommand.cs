using CallAladdin.Model;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class GoToVerificationCommand : ICommand
    {
        private DisclaimerViewModel disclaimerViewModel;
        public event EventHandler CanExecuteChanged;

        public GoToVerificationCommand(DisclaimerViewModel viewModel)
        {
            this.disclaimerViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var userRegistration = (UserRegistration)parameter;
            disclaimerViewModel.NotifyViewOnConfirmation();
            disclaimerViewModel.NavigateToSmsVerification(userRegistration);
        }
    }
}
