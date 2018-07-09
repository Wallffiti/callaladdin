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
using System.Threading.Tasks;

namespace CallAladdin.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private IUserIdentityRepository userIdentityRepository;
        private IUserProfileRepository userProfileRepository;
        private IUserService userService;
        private UserProfile userProfile;
        private bool isBusy;
        private bool usePasswordless;
        public ICommand GoToLoginCmd { get; set; }
        public ICommand GoToRegisterCmd { get; set; }

        public bool UsePasswordless
        {
            get { return usePasswordless; }
            set
            {
                usePasswordless = value;
                GlobalConfig.Instance.SetValueByKey(Constants.USE_PASSWORDLESS, usePasswordless);
                OnPropertyChanged("UsePasswordless");
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged("IsBusy"); }
        }

        public MainViewModel()
        {
            UsePasswordless = false;
            userIdentityRepository = new UserIdentityRepository();
            userProfileRepository = new UserProfileRepository();
            userService = new UserService();
            GoToLoginCmd = new GoToLoginCommand(this);
            GoToRegisterCmd = new GoToRegisterCommand(this);
        }
        public async void NavigateToRegister() => await Navigator.Instance.NavigateTo(PageType.USER_REGISTRATION);

        public async void NavigateToLogin() => await Navigator.Instance.NavigateTo(PageType.USER_LOGIN);    //TODO: if using passwordless, need to go to another page

        public async Task<UserProfile> UpdateUserProfile()
        {
            userProfile = await GetUserProfile();
            return userProfile;
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

        public async Task<UserProfile> GetUserProfile()
        {
            UserIdentityEntity userIdentityEntity = userIdentityRepository.GetAll().LastOrDefault();
            UserProfileEntity userProfileEntity = null;
            userProfile = null;

            if (userIdentityEntity != null && !string.IsNullOrEmpty(userIdentityEntity.Email))
            {
                userProfile = await userService.GetUserProfileByAuthLocalId(userIdentityEntity.LocalId);

                //Below code: tries to get latest data from server (and update cached data), otherwise get from cached data
                if (userProfile == null)
                {
                    Navigator.Instance.OkAlert("Alert", "Currently in offline mode due to issue on server. Your profile is temporarily not synced.", "OK");
                    userProfileEntity = userProfileRepository.GetUserProfile(userIdentityEntity.Email);
                    userProfile = ConvertProfileEntityToUserProfile(userProfileEntity);
                }
                else
                {
                    var row = userProfileRepository.CreateOrUpdate(GetUserProfileEntity());
                    if (row < 0)
                    {
                        Navigator.Instance.OkAlert("Alert", "There is an issue trying to update your profile onto your device from server.", "OK");
                    }
                }
            }

            return userProfile;
        }

        private UserProfileEntity GetUserProfileEntity()
        {
            return new UserProfileEntity
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
                PathToProfileImage = userProfile.PathToProfileImage,
                SystemGeneratedId = userProfile.SystemUUID
            };
        }

        private UserProfile ConvertProfileEntityToUserProfile(UserProfileEntity userProfileEntity)
        {
            UserProfile userProfile = null;

            if (userProfileEntity != null)
            {
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
                    SystemUUID = userProfileEntity.SystemGeneratedId
                    //TODO: fill in review data
                };
            }
            return userProfile;
        }
    }
}
