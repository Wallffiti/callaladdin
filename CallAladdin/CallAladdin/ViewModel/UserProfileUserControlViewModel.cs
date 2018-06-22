using CallAladdin.Commands;
using CallAladdin.EventArgs;
using CallAladdin.Helper;
using CallAladdin.Model;
using CallAladdin.Observers.Interfaces;
using CallAladdin.Repositories;
using CallAladdin.Repositories.Interfaces;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class UserProfileUserControlViewModel : BaseViewModel
    {
        private bool isBusy;
        private UserProfile userProfile;
        private IUserIdentityRepository userIdentityRepository;
        private IUserProfileRepository userProfileRepository;
        private bool isContractor;
        private string name;
        private string mobile;
        private string email;
        private string city;
        private string country;
        private string category;
        private string companyName;
        private string companyRegisteredAddress;
        private string imagePath;

        public UserProfile UserProfile { get { return userProfile; } }

        public bool IsContractor
        {
            get { return isContractor; }
            set { isContractor = value; OnPropertyChanged("IsContractor"); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; OnPropertyChanged("Mobile"); }
        }
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }
        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged("City"); }
        }
        public string Country
        {
            get { return country; }
            set { country = value; OnPropertyChanged("Country"); }
        }

        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged("Category"); }
        }

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; OnPropertyChanged("CompanyName"); }
        }

        public string CompanyRegisteredAddress
        {
            get { return companyRegisteredAddress; }
            set { companyRegisteredAddress = value; OnPropertyChanged("CompanyRegisteredAddress"); }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; OnPropertyChanged("ImagePath"); }
        }

        //private string userSystemUUID;

        public ICommand LogoutCmd { get; set; }
        public ICommand EditProfileCmd { get; set; }

        public UserProfileUserControlViewModel(UserProfile userProfile)
        {
            UpdateUserProfile(userProfile);

            userIdentityRepository = new UserIdentityRepository();
            userProfileRepository = new UserProfileRepository();
            LogoutCmd = new LogoutCommand(this);
            EditProfileCmd = new EditProfileCommand(this);
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            this.userProfile = userProfile;
            if (userProfile != null)
            {
                Name = userProfile.Name;
                Mobile = userProfile.Mobile;
                Email = userProfile.Email;
                City = userProfile.City;
                Country = userProfile.Country;
                IsContractor = userProfile.IsContractor;

                if (IsContractor)
                {
                    Category = userProfile.Category;
                    CompanyName = userProfile.CompanyName;
                    CompanyRegisteredAddress = userProfile.CompanyRegisteredAddress;
                }

                ImagePath = userProfile.PathToProfileImage;
                //userSystemUUID = userProfile.SystemUUID;
            }

            base.NotifyCompletion(this, new ObserverEventArgs(Constants.USER_PROFILE_UPDATE, string.Empty, userProfile));
        }

        public async void NavigateToEditUserProfile()
        {
            if (userProfile == null)
                return;

            if (isBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            isBusy = true;

            if (userProfile.IsContractor)
            {
                await Navigator.Instance.NavigateTo(PageType.EDIT_CONTRACTOR_PROFILE, this);
            }
            else
            {
                await Navigator.Instance.NavigateTo(PageType.EDIT_REQUESTOR_PROFILE, this);
            }

            await Task.Delay(1500);
            isBusy = false;
        }

        public async void Logout()
        {
            if (isBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            isBusy = true;

            Navigator.Instance.ConfirmationAlert("Alert", "Are you sure you want to log out?", "OK", "Cancel", async () =>
            {
                //For android
                var userIdentityDeletedRows = userIdentityRepository.DeleteUserIdentity();
                var userProfileDeletedRows = userProfileRepository.DeleteUserProfile();
                await Navigator.Instance.NavigateToRoot();
            }, async () =>
            {
                //For ios
                var userIdentityDeletedRows = userIdentityRepository.DeleteUserIdentity();
                var userProfileDeletedRows = userProfileRepository.DeleteUserProfile();
                await Navigator.Instance.NavigateToRoot();
            });

            await Task.Delay(1500);
            isBusy = false;
        }
    }
}
