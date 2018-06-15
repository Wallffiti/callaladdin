using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class EditProfileCommand : ICommand
    {
        private UserProfileUserControlViewModel userProfileUserControlViewModel;
        public event EventHandler CanExecuteChanged;

        public EditProfileCommand(UserProfileUserControlViewModel viewModel)
        {
            userProfileUserControlViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            userProfileUserControlViewModel.NavigateToEditUserProfile();
        }
    }
}
