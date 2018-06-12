using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class CancelPhoneNumberSubmissionCommand : ICommand
    {
        private ChangePhoneNumberViewModel changePhoneNumberViewModel;
        public event EventHandler CanExecuteChanged;

        public CancelPhoneNumberSubmissionCommand(ChangePhoneNumberViewModel viewModel)
        {
            changePhoneNumberViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            changePhoneNumberViewModel.CancelPhoneNumberChange();
        }
    }
}
