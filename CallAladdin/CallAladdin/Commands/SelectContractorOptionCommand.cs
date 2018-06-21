using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class SelectContractorOptionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HomeUserControlViewModel homeUserControlViewModel;

        public SelectContractorOptionCommand(HomeUserControlViewModel viewModel)
        {
            homeUserControlViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                return;

            var category = (string)parameter;
            homeUserControlViewModel.NavigateToJobRequestAsync(category);
        }
    }
}
