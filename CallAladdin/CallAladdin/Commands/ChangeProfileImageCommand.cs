using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class ChangeProfileImageCommand : ICommand
    {
        private UserRegistrationViewModel userRegistrationViewModel;
        public event EventHandler CanExecuteChanged;

        public ChangeProfileImageCommand(UserRegistrationViewModel viewModel)
        {
            userRegistrationViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            userRegistrationViewModel.ChangeProfileImageAsync();
        }
    }
}
