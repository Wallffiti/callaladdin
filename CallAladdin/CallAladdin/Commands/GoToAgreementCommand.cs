using CallAladdin.Model;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using CallAladdin.Helper;

namespace CallAladdin.Commands
{
    public class GoToAgreementCommand : ICommand
    {
        private UserRegistrationViewModel userRegistrationViewModel;
        public event EventHandler CanExecuteChanged;

        public GoToAgreementCommand(UserRegistrationViewModel viewModel)
        {
            userRegistrationViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var userRegistration = (UserRegistration)parameter;
            
            if (userRegistration != null)
            {
                //return !string.IsNullOrEmpty(userRegistration.Name) 
                //    && !string.IsNullOrEmpty(userRegistration.Email) 
                //    && !string.IsNullOrEmpty(userRegistration.Mobile) 
                //    && !string.IsNullOrEmpty(userRegistration.City) 
                //    && !string.IsNullOrEmpty(userRegistration.Country);

                //return (!string.IsNullOrEmpty(userRegistration.Name)
                //    && !string.IsNullOrEmpty(userRegistration.Email)
                //    && !string.IsNullOrEmpty(userRegistration.Mobile)
                //    && !string.IsNullOrEmpty(userRegistration.City)
                //    && !string.IsNullOrEmpty(userRegistration.Country))
                //    && (!userRegistration.IsRegisteredAsContractor || (userRegistration.IsRegisteredAsContractor && !string.IsNullOrEmpty(userRegistration.Category) && !string.IsNullOrEmpty(userRegistration.CompanyName) && !string.IsNullOrEmpty(userRegistration.CompanyAddress));

                bool hasMandatoryInfo = !string.IsNullOrEmpty(userRegistration.Name)
                    && !string.IsNullOrEmpty(userRegistration.Email)
                    && Validators.ValidateEmail(userRegistration.Email)
                    && !string.IsNullOrEmpty(userRegistration.Mobile)
                    && !string.IsNullOrEmpty(userRegistration.City)
                    && !string.IsNullOrEmpty(userRegistration.Country);

                bool hasContractorInfo = !string.IsNullOrEmpty(userRegistration.Category)
                    && !string.IsNullOrEmpty(userRegistration.CompanyName)
                    && !string.IsNullOrEmpty(userRegistration.CompanyAddress);

                if (userRegistration.IsRegisteredAsContractor)
                {
                    return hasMandatoryInfo && hasContractorInfo;
                }
                else
                {
                    return hasMandatoryInfo;
                }
            }

            return false;
        }

        public void Execute(object parameter)
        {
            var userRegistration = (UserRegistration)parameter;
            userRegistrationViewModel.NavigateToAgreement(userRegistration);
        }
    }
}
