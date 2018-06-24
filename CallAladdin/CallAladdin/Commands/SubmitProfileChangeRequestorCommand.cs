using CallAladdin.Model;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class SubmitProfileChangeRequestorCommand : ICommand
    {
        public EditRequestorProfileViewModel editRequestorProfileViewModel { get; private set; }

        public event EventHandler CanExecuteChanged;

        public SubmitProfileChangeRequestorCommand(EditRequestorProfileViewModel viewModel)
        {
            editRequestorProfileViewModel = viewModel;
        }

        public bool CanExecute(object param)
        {
            if (param != null)
            {
                var userProfile = (UserProfile)param;

                if (userProfile != null)
                {
                    bool hasMandatoryInfo = !string.IsNullOrEmpty(userProfile.Name)
                    && !string.IsNullOrEmpty(userProfile.City)
                    && !string.IsNullOrEmpty(userProfile.Country);

                    bool hasContractorInfo = !string.IsNullOrEmpty(userProfile.Category)
                    && !string.IsNullOrEmpty(userProfile.CompanyName)
                    && !string.IsNullOrEmpty(userProfile.CompanyRegisteredAddress)
                    && !string.IsNullOrEmpty(userProfile.PathToProfileImage);

                    if (userProfile.IsContractor)
                    {
                        return hasMandatoryInfo && hasContractorInfo;
                    }
                    else
                    {
                        return hasMandatoryInfo;
                    }
                }
            }

            return false;
        }

        public void Execute(object parameter)
        {
            editRequestorProfileViewModel.SubmitProfileChanges();
        }
    }
}
