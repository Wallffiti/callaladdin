using CallAladdin.Model;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class SubmitProfileChangeContractorCommand : ICommand
    {
        private EditContractorProfileViewModel editContractorProfileViewModel;

        public event EventHandler CanExecuteChanged;

        public SubmitProfileChangeContractorCommand(EditContractorProfileViewModel viewModel)
        {
            editContractorProfileViewModel = viewModel;
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

                    return hasMandatoryInfo && hasContractorInfo;    //this is contractor page, therefore all fields are mandatory
                }
            }

            return false;
        }

        public void Execute(object parameter)
        {
            editContractorProfileViewModel.SubmitProfileChanges();
        }
    }
}
