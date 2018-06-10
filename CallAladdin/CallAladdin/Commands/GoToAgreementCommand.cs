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
                bool requiredVerificationFulfilled = true;

                if (!GlobalConfig.Instance.UsePasswordless)
                {
                    bool passwordMatched = string.Compare(userRegistration.Password, userRegistration.ReTypePassword) == 0;
                    requiredVerificationFulfilled = passwordMatched && string.IsNullOrEmpty(userRegistration.Password) ? false : Validators.ValidatePassword(userRegistration.Password);
                }

                bool hasMandatoryInfo = !string.IsNullOrEmpty(userRegistration.Name)
                    && !string.IsNullOrEmpty(userRegistration.Email)
                    && Validators.ValidateEmail(userRegistration.Email)
                    && !string.IsNullOrEmpty(userRegistration.Mobile)
                    && !string.IsNullOrEmpty(userRegistration.City)
                    && !string.IsNullOrEmpty(userRegistration.Country)
                    && requiredVerificationFulfilled;

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
