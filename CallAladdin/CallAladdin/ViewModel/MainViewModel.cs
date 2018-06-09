using CallAladdin.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand GoToLoginCmd { get; set; }
        public ICommand GoToRegisterCmd { get; set; }

        public MainViewModel()
        {
            GoToLoginCmd = new GoToLoginCommand(this);
            GoToRegisterCmd = new GoToRegisterCommand(this);
        }
        public async void NavigateToRegister() => await Navigator.Instance.NavigateTo(PageType.USER_REGISTRATION);

        public async void NavigateToLogin()
        {
            await Navigator.Instance.NavigateTo(PageType.USER_LOGIN);
        }
    }
}
