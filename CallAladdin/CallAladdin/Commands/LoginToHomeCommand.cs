using CallAladdin.Model;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class LoginToHomeCommand : ICommand
    {
        private UserLoginViewModel userLoginViewModel;
        public event EventHandler CanExecuteChanged;

        public LoginToHomeCommand(UserLoginViewModel viewModel)
        {
            userLoginViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                var userLogin = (UserLogin)parameter;
                if (userLogin != null)
                {
                    return !string.IsNullOrEmpty(userLogin.Email) && !string.IsNullOrEmpty(userLogin.Password);
                }
            }

            return false;
        }

        public void Execute(object parameter)
        {
            userLoginViewModel.NavigateToHome();
        }
    }
}
