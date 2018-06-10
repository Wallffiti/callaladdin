using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class Dummy2Command : ICommand
    {
        private HomeViewModel homeViewModel;
        public event EventHandler CanExecuteChanged;

        public Dummy2Command(HomeViewModel viewModal)
        {
            homeViewModel = viewModal;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            homeViewModel.NavigateToDummyModal();
        }
    }
}
