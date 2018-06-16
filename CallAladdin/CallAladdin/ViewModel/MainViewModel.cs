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
        private UserProfile userProfile;
        private bool isBusy;
        public ICommand GoToLoginCmd { get; set; }
        public ICommand GoToRegisterCmd { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged("IsBusy"); }
        }

        public MainViewModel()
        {
            userIdentityRepository = new UserIdentityRepository();
            userProfileRepository = new UserProfileRepository();
            userService = new UserService();
            GoToLoginCmd = new GoToLoginCommand(this);
            GoToRegisterCmd = new GoToRegisterCommand(this);
        }
        public async void NavigateToRegister() => await Navigator.Instance.NavigateTo(PageType.USER_REGISTRATION);

        public async void NavigateToLogin() => await Navigator.Instance.NavigateTo(PageType.USER_LOGIN);

        public void UpdateUserProfile()
        {
            userProfile = GetUserProfile();
        }

        public void SetPageBusy(bool isBusy)
        {
            IsBusy = isBusy;
        }

        public async void NavigateToHome()
        {
            if (userProfile != null)
            {
                try
                {
                    await Navigator.Instance.NavigateTo(PageType.HOME, userProfile);
                }
                catch (Exception ex)
                {
                    //sometimes page may not load fast enough
                }
            }
        }

        public UserProfile GetUserProfile()
        {
            UserIdentityEntity userIdentityEntity = userIdentityRepository.GetAll().LastOrDefault();
            UserProfileEntity userProfileEntity = null;
            userProfile = null;

            if (userIdentityEntity != null && !string.IsNullOrEmpty(userIdentityEntity.Email))
            {
                userProfileEntity = userProfileRepository.GetUserProfile(userIdentityEntity.Email);
                userProfile = ConvertProfileEntityToUserProfile(userProfileEntity);
            }

            return userProfile;
        }

        private UserProfile ConvertProfileEntityToUserProfile(UserProfileEntity userProfileEntity)
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
