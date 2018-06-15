using CallAladdin.Commands;
using CallAladdin.Model;
using CallAladdin.Model.Entities;
using CallAladdin.Repositories;
using CallAladdin.Repositories.Interfaces;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Linq;

namespace CallAladdin.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private IUserIdentityRepository userIdentityRepository;
        private IUserProfileRepository userProfileRepository;
        private IUserService userService;
        public ICommand GoToLoginCmd { get; set; }
        public ICommand GoToRegisterCmd { get; set; }

        public MainViewModel()
        {
            userIdentityRepository = new UserIdentityRepository();
            userProfileRepository = new UserProfileRepository();
            userService = new UserService();
            GoToLoginCmd = new GoToLoginCommand(this);
            GoToRegisterCmd = new GoToRegisterCommand(this);
        }
        public async void NavigateToRegister() => await Navigator.Instance.NavigateTo(PageType.USER_REGISTRATION);

        public async void NavigateToLogin()
        {
            UserIdentityEntity userIdentityEntity = userIdentityRepository.GetAll().LastOrDefault();
            UserProfileEntity userProfileEntity = null;
            UserProfile userProfile = null;

            if (userIdentityEntity != null && !string.IsNullOrEmpty(userIdentityEntity.Email))
            {
                userProfileEntity = userProfileRepository.GetUserProfile(userIdentityEntity.Email);
                userProfile = GetUserProfile(userProfileEntity);
            }
            else
            {
                //TODO:
                //1. Login via firebase auth api to get signupUserResponse (if db doesn't have cached data)
                //2. save signupUserResponse into local storage (if db doesn't have cached data)
                //3. Call backend server api to get user profile using data from signupUserResponse
                //4. Navigate to home page

                userProfile = await userService.GetUserProfile("dummy");  //DEBUG
            }

            //await Navigator.Instance.NavigateTo(PageType.USER_LOGIN); //DEBUG
            await Navigator.Instance.NavigateTo(PageType.HOME, userProfile); //DEBUG
        }

        private UserProfile GetUserProfile(UserProfileEntity userProfileEntity)
        {
            UserProfile userProfile;
            userProfile = new UserProfile()
            {
                Category = userProfileEntity.Category,
                City = userProfileEntity.City,
                CompanyName = userProfileEntity.CompanyName,
                CompanyRegisteredAddress = userProfileEntity.CompanyRegisteredAddress,
                Country = userProfileEntity.Country,
                Email = userProfileEntity.Email,
                IsContractor = userProfileEntity.IsContractor,
                Mobile = userProfileEntity.Mobile,
                Name = userProfileEntity.Name,
                PathToProfileImage = userProfileEntity.PathToProfileImage,
                //TODO: fill in review data
            };
            return userProfile;
        }
    }
}
