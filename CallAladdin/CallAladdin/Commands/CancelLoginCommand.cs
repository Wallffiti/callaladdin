using CallAladdin.Model;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class CancelLoginCommand : ICommand
    {
        private UserLoginViewModel userLoginViewModel;
        public event EventHandler CanExecuteChanged;

        public CancelLoginCommand(UserLoginViewModel viewModel)
        {
            userLoginViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;

            var boolVal = (bool)parameter;
            return !boolVal;
        }

        public void Execute(object parameter)
        {
            userLoginViewModel.ReturnMainPage();
        }
    }
}
