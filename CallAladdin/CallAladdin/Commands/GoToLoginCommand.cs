using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class GoToLoginCommand : ICommand
    {
        private MainViewModel mainViewModel;
        public event EventHandler CanExecuteChanged;

        public GoToLoginCommand(MainViewModel viewModel)
        {
            mainViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                bool isBusy = (bool)parameter;
                return !isBusy;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            mainViewModel.NavigateToLogin();
        }
    }
}
