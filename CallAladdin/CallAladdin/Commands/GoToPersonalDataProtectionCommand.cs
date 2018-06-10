using CallAladdin.Model;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class GoToPersonalDataProtectionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private DisclaimerViewModel disclaimerViewModel;

        public GoToPersonalDataProtectionCommand(DisclaimerViewModel viewModel)
        {
            disclaimerViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var userRegistration = (UserRegistration)parameter;
            disclaimerViewModel.NavigateToPersonalDataProtection(userRegistration);
        }
    }
}
