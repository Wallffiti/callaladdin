using CallAladdin.Commands;
using CallAladdin.EventArgs;
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
    public class UserProfileUserControlViewModel : BaseViewModel
    {
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

        public ICommand LogoutCmd { get; set; }
        public ICommand EditProfileCmd { get; set; }

        public UserProfileUserControlViewModel(UserProfile userProfile)
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
            }

            userIdentityRepository = new UserIdentityRepository();
            userProfileRepository = new UserProfileRepository();
            LogoutCmd = new LogoutCommand(this);
            EditProfileCmd = new EditProfileCommand(this);
        }

        public void NavigateToEditUserProfile()
        {
            //TODO: navigate tp edit user profile page (different page for each requestor and contractor)
        }

        public void Logout()
        {
            var userIdentityDeletedRows = userIdentityRepository.DeleteUserIdentity();
            var userProfileDeletedRows = userProfileRepository.DeleteUserProfile();

            Navigator.Instance.OkAlert("Info", "User is now logged out.", "OK", async () =>
            {
                //For android
                await Navigator.Instance.NavigateToRoot();
            }, async () =>
            {
                //For ios
                await Navigator.Instance.NavigateToRoot();
            });

        }
    }
}
