using CallAladdin.Helper;
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
        private PersonalDataProtectionViewModel personalDataProtectionViewModel;
        public event EventHandler CanExecuteChanged;

        public GoToVerificationCommand(PersonalDataProtectionViewModel viewModel)
        {
            this.personalDataProtectionViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null) return false;

            bool isBusy = (bool)parameter;
            return isBusy == false;
        }

        public void Execute(object parameter)
        {
            //var userRegistration = (UserRegistration)parameter;
            personalDataProtectionViewModel.NotifyViewOnConfirmation();

            if (Auth.UsePasswordless())
            {
                personalDataProtectionViewModel.NavigateToSmsVerification(/*userRegistration*/);
            }
            else
            {
                personalDataProtectionViewModel.NavigateToHome(/*userRegistration*/);
            }
        }
    }
}
