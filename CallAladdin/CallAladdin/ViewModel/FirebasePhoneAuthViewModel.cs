using CallAladdin.Model;
using CallAladdin.Repositories;
using CallAladdin.Repositories.Interfaces;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class FirebasePhoneAuthViewModel : BaseViewModel
    {
        private BaseViewModel parentViewModel;
        private UserRegistration userRegistration;
        private bool isBusy;
        private string vendorUUID;
        private string refreshToken;
        private string accessToken;
        //private DateTime expiryDateTime;
        private IUserService userService;
        private IUserProfileRepository userProfileRepository;
        private IUserIdentityRepository userIdentityRepository;

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public FirebasePhoneAuthViewModel(object owner)
        {
            if (owner != null)
            {
                if (owner is PersonalDataProtectionViewModel)
                {
                    var personalDataProtectionViewModel = (PersonalDataProtectionViewModel)owner;
                    userRegistration = personalDataProtectionViewModel.GetUserRegistration();
                    parentViewModel = personalDataProtectionViewModel;
                }
            }

            userService = new UserService();
            userProfileRepository = new UserProfileRepository();
            userIdentityRepository = new UserIdentityRepository();
        }

        public void UpdatePhone(string phone)
        {
            if (userRegistration != null)
            {
                userRegistration.Mobile = phone;
            }
        }

        public void UpdateVendorUUID(string uuid)
        {
            vendorUUID = uuid;
        }

        //public void UpdateExpiryTime(DateTime date)
        //{
        //    expiryDateTime = date;
        //}

        public void UpdateAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public void UpdateRefreshToken(string refreshToken)
        {
            this.refreshToken = refreshToken;
        }

        //public void NavigateToHomeHandler(object sender, System.EventArgs e)
        //{
        //    NavigateToHome();
        //}

        public async void NavigateToHome()
        {
            if (IsBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            IsBusy = true;

            //Check if user exists in backend
            var userProfile = await userService.GetUserProfileByAuthLocalId(vendorUUID);

            if (userProfile != null)
            {
                //If user exists in backend, cache fresh user profile data from backend to device
                int rowAffectedUserIdentity = CreateOrUpdateUserIdentity();
                var rowAffectedUserProfile = userProfileRepository.CreateOrUpdate(GetUserProfile(userProfile));

                if (rowAffectedUserIdentity < 0 || rowAffectedUserProfile < 0)
                {
                    Navigator.Instance.OkAlert("Error", "There is an issue with local storage.", "OK", null, null);
                    IsBusy = false;
                    return;
                }
            }
            else
            {
                //If user not exists, then register user profile first
                var createUserResponse = await userService.CreateUser(this.userRegistration, vendorUUID);

                if (createUserResponse != null && createUserResponse.IsSuccess)
                {
                    //Upon successful user profile registration, then cache user profile data on device
                    var userIdentityRowsAffected = CreateOrUpdateUserIdentity();
                    var userProfileEntity = GetUserProfileEntity();
                    userProfileEntity.SystemGeneratedId = createUserResponse.SystemGeneratedId;
                    var userProfileRowsAffected = userProfileRepository.CreateOrUpdate(userProfileEntity);

                    if (userIdentityRowsAffected < 0 || userProfileRowsAffected < 0)
                    {
                        Navigator.Instance.OkAlert("Error", "There is an issue with local storage.", "OK", () =>
                        {
                            //For android
                        }, () =>
                        {
                            //For ios
                        });

                        IsBusy = false;
                        return;
                    }

                    userProfile = GetUserProfile();
                    userProfile.SystemUUID = createUserResponse.SystemGeneratedId;
                }
                else
                {
                    Navigator.Instance.OkAlert("Error", "There is a problem with the registration process. Please try again later.", "OK", () =>
                    {
                        //For android
                    }, () =>
                    {
                        //For ios
                    });

                    IsBusy = false;
                    return;
                }
            }

            //3. Navigate to home
            Navigator.Instance.OkAlert("Successful", "Login successful! You can now use Call Aladdin.", "OK", null, null);
            await Navigator.Instance.NavigateTo(PageType.HOME, userProfile);
            IsBusy = false;
        }

        private UserProfile GetUserProfile()
        {
            UserProfile userProfile = null;

            if (userRegistration != null)
            {
                userProfile = new UserProfile()
                {
                    Category = userRegistration.Category,
                    City = userRegistration.City,
                    CompanyName = userRegistration.CompanyName,
                    CompanyRegisteredAddress = userRegistration.CompanyAddress,
                    Country = userRegistration.Country,
                    Email = userRegistration.Email,
                    IsContractor = userRegistration.IsRegisteredAsContractor,
                    Mobile = userRegistration.Mobile,
                    Name = userRegistration.Name,
                    PathToProfileImage = userRegistration.ProfileImagePath
                    //TODO: update reviews
                };
            }
            return userProfile;
        }

        private int CreateOrUpdateUserIdentity()
        {
            //2.a If user exists, then store user profile to device storage
            return userIdentityRepository.CreateOrUpdate(new Model.Entities.UserIdentityEntity
            {
                Email = userRegistration?.Email,
                LocalId = vendorUUID,
                IdToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        private Model.Entities.UserProfileEntity GetUserProfileEntity()
        {
            if (userRegistration != null)
            {
                return new Model.Entities.UserProfileEntity()
                {
                    Category = userRegistration.Category,
                    City = userRegistration.City,
                    CompanyName = userRegistration.CompanyName,
                    CompanyRegisteredAddress = userRegistration.CompanyAddress,
                    Country = userRegistration.Country,
                    Email = userRegistration.Email,
                    IsContractor = userRegistration.IsRegisteredAsContractor,
                    Mobile = userRegistration.Mobile,
                    Name = userRegistration.Name,
                    PathToProfileImage = userRegistration.ProfileImagePath
                };
            }

            return null;
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
                    PathToProfileImage = userProfile.PathToProfileImage,
                    SystemGeneratedId = userProfile.SystemUUID
                };
            }

            return null;
        }
    }
}
