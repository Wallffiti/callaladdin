using CallAladdin.Commands;
using CallAladdin.Helper;
using CallAladdin.Model;
using CallAladdin.Repositories;
using CallAladdin.Repositories.Interfaces;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        private IUserService userService;
        private IUserProfileRepository userProfileRepository;
        private IUserIdentityRepository userIdentityRepository;
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
            set { email = value; UpdateUserLogin(); ValidateForm(); OnPropertyChanged("Email"); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; UpdateUserLogin(); ValidateForm(); OnPropertyChanged("Password"); }
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
            userService = new UserService();
            userProfileRepository = new UserProfileRepository();
            userIdentityRepository = new UserIdentityRepository();
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

        public async void NavigateToHome()
        {
            //1. Login via firebase auth api to get signupUserResponse (if db doesn't have cached data)
            var authResponse = await userService.LoginUserToAuthServer(userLogin);

            if (authResponse != null)
            {
                if (authResponse.IsError)
                {
                    Navigator.Instance.OkAlert("Error", "There is a problem with user log in. Error: Credentials are invalid (check your email and password).", "OK", null, null);
                    return;
                }

                //2. Call backend server api to get user profile using data from signupUserResponse
                var userProfile = await userService.GetUserProfile(authResponse.LocalId);

                if (userProfile != null)
                {
                    //3. Store auth and user profile to local storage
                    var rowAffectedUserIdentity = userIdentityRepository.CreateOrUpdate(GetUserIdentity(authResponse));
                    var rowAffectedUserProfile = userProfileRepository.CreateOrUpdate(GetUserProfile(userProfile));

                    if (rowAffectedUserIdentity < 0 || rowAffectedUserProfile < 0)
                    {
                        Navigator.Instance.OkAlert("Error", "There is an issue with local storage.", "OK", null, null);
                        return;
                    }

                    Navigator.Instance.OkAlert("Successful", "Login successful! You can now use Call Aladdin.", "OK", null, null);
                    await Navigator.Instance.NavigateTo(PageType.HOME, userProfile);
                    return;
                }
            }

            Navigator.Instance.OkAlert("Error", "There is a problem with user log in. Please try again later.", "OK", null, null);
        }

        private Model.Entities.UserProfileEntity GetUserProfile(UserProfile userProfile)
        {
            if (userProfile != null)
            {
                return new Model.Entities.UserProfileEntity
                {
                    Category = userProfile.Category,
                    City = userProfile.City,
                    CompanyName = userProfile.CompanyName,
                    CompanyRegisteredAddress = userProfile.CompanyRegisteredAddress,
                    Country = userProfile.Country,
                    Email = userProfile.Email,
                    IsContractor = userProfile.IsContractor,
                    Mobile = userProfile.Mobile,
                    Name = userProfile.Name,
                    PathToProfileImage = userProfile.PathToProfileImage
                };
            }

            return null;
        }

        private Model.Entities.UserIdentityEntity GetUserIdentity(Model.Responses.UserLoginResponse authResponse)
        {
            if (authResponse != null)
            {
                return new Model.Entities.UserIdentityEntity
                {
                    Email = authResponse.Email,
                    ExpiresIn = authResponse.ExpiresIn,
                    IdToken = authResponse.IdToken,
                    LocalId = authResponse.LocalId,
                    RefreshToken = authResponse.RefreshToken
                };
            }

            return null;
        }

        public void ReturnMainPage()
        {
            Navigator.Instance.ConfirmationAlert("Alert", "Are you sure you want to return to main page? All changes will be lost.", "OK", "Cancel", async() =>
            {
                //For android
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
            }, async () =>
            {
                //For ios
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
            });
        }
    }
}
