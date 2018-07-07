using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private BaseViewModel parentViewModel;
        private string pageTitle;
        private string imagePath;
        private string companyName;
        private string name;
        private string mobile;
        private string email;
        private string city;
        private string country;
        private string companyAddress;
        private DateTime createdDate;

        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                OnPropertyChanged(nameof(PageTitle));
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public string CompanyName
        {
            get { return companyName; }
            set
            {
                companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Mobile
        {
            get { return mobile; }
            set
            {
                mobile = value;
                OnPropertyChanged(nameof(Mobile));
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string CompanyAddress
        {
            get { return companyAddress; }
            set
            {
                companyAddress = value;
                OnPropertyChanged(nameof(CompanyAddress));
            }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set
            {
                createdDate = value;
                OnPropertyChanged(nameof(CreatedDate));
            }
        }

        public ProfileViewModel(object owner)
        {
            if (owner is JobHistoryViewViewModel)
            {
                var jobHistoryViewViewModel = (JobHistoryViewViewModel)owner;
                parentViewModel = jobHistoryViewViewModel;
                var profile = jobHistoryViewViewModel.GetProfile();

                if (profile != null)
                {
                    ImagePath = profile.PathToProfileImage;
                    CompanyName = profile.CompanyName;
                    Name = profile.Name;
                    Mobile = profile.Mobile;
                    Email = profile.Email;
                    City = profile.City;
                    Country = profile.Country;
                    CompanyAddress = profile.CompanyRegisteredAddress;
                    CreatedDate = profile.CreatedDate;
                }

                var profileType = jobHistoryViewViewModel.ProfileType;
                if (string.IsNullOrEmpty(profileType))
                {
                    PageTitle = "USER PROFILE";
                }
                else if (profileType == Constants.REQUESTOR)
                {
                    PageTitle = "REQUESTOR PROFILE";
                }
                else if (profileType == Constants.CONTRACTOR)
                {
                    PageTitle = "CONTRACTOR PROFILE";
                }
            }
        }
    }
}
