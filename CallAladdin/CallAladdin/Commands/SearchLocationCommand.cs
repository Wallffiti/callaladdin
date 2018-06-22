using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class SearchLocationCommand : ICommand
    {
        public JobRequestViewModel JobRequestViewModel { get; private set; }

        public event EventHandler CanExecuteChanged;

        public SearchLocationCommand(JobRequestViewModel viewModel)
        {
            JobRequestViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO
        }
    }
}
