using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class UserProfileUserControlViewModel : BaseViewModel
    {
        private UserProfile userProfile;
        private bool isContractor;
        private string name;
        private string mobile;
        private string email;
        private string city;
        private string country;
        private string category;
        private string companyName;
        private string companyRegisteredAddress;

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
            }
        }
    }
}
