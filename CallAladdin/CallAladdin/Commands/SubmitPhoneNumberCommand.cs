using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class SubmitPhoneNumberCommand : ICommand
    {
        private ChangePhoneNumberViewModel changePhoneNumberViewModel;
        public event EventHandler CanExecuteChanged;

        public SubmitPhoneNumberCommand(ChangePhoneNumberViewModel viewModel)
        {
            changePhoneNumberViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var phoneNumber = parameter?.ToString();
            return !string.IsNullOrEmpty(phoneNumber);
        }

        public void Execute(object parameter)
        {
            changePhoneNumberViewModel.SubmitPhoneNumber();
        }
    }
}
