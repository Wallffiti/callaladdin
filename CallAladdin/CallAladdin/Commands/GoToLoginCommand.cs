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
            return true;
        }

        public void Execute(object parameter)
        {
            mainViewModel.NavigateToLogin();
        }
    }
}
