using CallAladdin.Commands;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private IUserService userService;
        public ICommand GoToLoginCmd { get; set; }
        public ICommand GoToRegisterCmd { get; set; }

        public MainViewModel()
        {
            userService = new UserService();
            GoToLoginCmd = new GoToLoginCommand(this);
            GoToRegisterCmd = new GoToRegisterCommand(this);
        }
        public async void NavigateToRegister() => await Navigator.Instance.NavigateTo(PageType.USER_REGISTRATION);

        public async void NavigateToLogin()
        {
            var userProfile = await userService.GetUserProfile("dummy");  //DEBUG
            //await Navigator.Instance.NavigateTo(PageType.USER_LOGIN); //DEBUG
            await Navigator.Instance.NavigateTo(PageType.HOME, userProfile); //DEBUG
        }
    }
}
