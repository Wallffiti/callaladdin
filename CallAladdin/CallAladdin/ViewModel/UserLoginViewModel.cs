using CallAladdin.Commands;
using CallAladdin.Helper;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        private UserLogin userLogin;
        private string email;
        private string password;
        private bool emailIsNotValid;

        public UserLogin UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; OnPropertyChanged("UserLogin"); }
        }

        public string Email
        {
            get { return email; }
            set { email = value; UpdateUserLogin(); ValidateForm();  OnPropertyChanged("Email"); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; UpdateUserLogin(); ValidateForm();  OnPropertyChanged("Password"); }
        }

        public bool EmailIsNotValid
        {
            get { return emailIsNotValid; }
            set { emailIsNotValid = value; OnPropertyChanged("EmailIsNotValid"); }
        }

        public ICommand CancelLoginCmd { get; set; }
        public ICommand LoginToHomeCmd { get; set; }

        public UserLoginViewModel()
        {
            CancelLoginCmd = new CancelLoginCommand(this);
            LoginToHomeCmd = new LoginToHomeCommand(this);
        }

        public void UpdateUserLogin()
        {
            UserLogin = new UserLogin()
            {
                Email = email,
                Password = password
            };
        }

        public void ValidateForm()
        {
            EmailIsNotValid = string.IsNullOrEmpty(email) ? false : !Validators.ValidateEmail(email);
        }

        public void NavigateToHome()
        {
            //TODO
        }

        public async void ReturnMainPage()
        {
            await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
        }
    }
}
