using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class DummyCommand : ICommand
    {
        private HomeViewModel homeViewModel;
        public event EventHandler CanExecuteChanged;

        public DummyCommand(HomeViewModel viewModel)
        {
            homeViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            homeViewModel.NavigateToDummyPage();
        }
    }
}
