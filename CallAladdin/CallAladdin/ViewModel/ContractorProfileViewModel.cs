using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class ContractorProfileViewModel : BaseViewModel
    {
        private BaseViewModel parentViewModel;
        private string imagePath;
        private string companyName;
        private string name;
        private string mobile;
        private string email;
        private string city;
        private string country;
        private string companyAddress;
        private DateTime createdDate;

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

        public ContractorProfileViewModel(object owner)
        {
            if (owner is JobHistoryViewViewModel)
            {
                var jobHistoryViewViewModel = (JobHistoryViewViewModel)owner;
                parentViewModel = jobHistoryViewViewModel;
                var contractorProfile = jobHistoryViewViewModel.GetContractorProfile();

                if (contractorProfile != null)
                {
                    ImagePath = contractorProfile.PathToProfileImage;
                    CompanyName = contractorProfile.CompanyName;
                    Name = contractorProfile.Name;
                    Mobile = contractorProfile.Mobile;
                    Email = contractorProfile.Email;
                    City = contractorProfile.City;
                    Country = contractorProfile.Country;
                    CompanyAddress = contractorProfile.CompanyRegisteredAddress;
                    CreatedDate = contractorProfile.CreatedDate;
                }
            }
        }
    }
}
