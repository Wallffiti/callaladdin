using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class ForgottenPasswordCommand : ICommand
    {
        private UserLoginViewModel userLoginViewModel;
        public event EventHandler CanExecuteChanged;

        public ForgottenPasswordCommand(UserLoginViewModel viewModel)
        {
            userLoginViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;

            var isBusy = (bool)parameter;
            return !isBusy;
        }

        public void Execute(object parameter)
        {
            userLoginViewModel.SendForgottenPasswordLink();
        }
    }
}
