using CallAladdin.Commands;
using CallAladdin.Helper;
using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class UserRegistrationViewModel : BaseViewModel
    {
        public ICommand GoToAgreementCmd { get; set; }
        private string name;
        private string mobile;
        private string email;
        private IList<string> countries;
        private IList<string> cities;
        private string selectedCountry;
        private string selectedCity;
        private string company;
        private string selectedCategory;
        private string companyAddress;
        private bool emailIsNotValid;
        //private bool showContractorOptions;

        //public bool ShowContractorOptions
        //{
        //    get { return showContractorOptions; }
        //    set
        //    {
        //        showContractorOptions = value;
        //        OnPropertyChanged("ShowContractorOptions");
        //    }
        //}

        public bool EmailIsNotValid
        {
            get { return emailIsNotValid; }
            set
            {
                emailIsNotValid = value;
                OnPropertyChanged("EmailIsNotValid");
            }
        }

        public string CompanyAddress
        {
            get { return companyAddress; }
            set
            {
                companyAddress = value;
                UpdateUserRegistration();
                OnPropertyChanged("CompanyAddress");
            }
        }

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                UpdateUserRegistration();
                OnPropertyChanged("SelectedCategory");
            }
        }

        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
                UpdateUserRegistration();
                OnPropertyChanged("Company");
            }
        }

        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                UpdateUserRegistration();
                OnPropertyChanged("SelectedCountry");
            }
        }

        public string SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                UpdateUserRegistration();
                OnPropertyChanged("SelectedCity");
            }
        }

        public IList<string> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                OnPropertyChanged("Countries");
            }
        }

        public IList<string> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                OnPropertyChanged("Cities");
            }
        }

        private bool isRegisteredAsContractor;
        private UserRegistration userRegistration;
        private ILocationService locationService;

        public UserRegistration UserRegistration
        {
            get { return userRegistration; }
            set
            {
                userRegistration = value;
                OnPropertyChanged("UserRegistration");
            }
        }


        public bool IsRegisteredAsContractor
        {
            get { return isRegisteredAsContractor; }
            set
            {
                isRegisteredAsContractor = value;
                UpdateUserRegistration();
                //this.ShowContractorOptions = isRegisteredAsContractor;
                OnPropertyChanged("IsRegisteredAsContractor");
            }
        }


        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                UpdateUserRegistration();
                ValidateForm();
                OnPropertyChanged("Email");
            }
        }


        public string Mobile
        {
            get { return mobile; }
            set
            {
                mobile = value;
                UpdateUserRegistration();
                OnPropertyChanged("Mobile");
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                UpdateUserRegistration();
                OnPropertyChanged("Name");
            }
        }

        public UserRegistrationViewModel()
        {
            GoToAgreementCmd = new GoToAgreementCommand(this);
            locationService = new MockLocationService();    //TODO switch this
            Cities = locationService.GetCities("Malaysia");
            Countries = locationService.GetCountries();
        }

        public void UpdateUserRegistration()
        {
            UserRegistration = new UserRegistration()
            {
                Name = this.name,
                Mobile = this.mobile,
                Email = this.email,
                City = this.selectedCity,
                Country = this.selectedCountry,
                IsRegisteredAsContractor = this.isRegisteredAsContractor,
                Category = this.selectedCategory,
                CompanyName = this.company,
                CompanyAddress = this.companyAddress
            };
        }

        public void ValidateForm()
        {
            this.EmailIsNotValid = string.IsNullOrEmpty(this.email) ? false : !Validators.ValidateEmail(this.email);
        }

        public async void NavigateToAgreement(UserRegistration userRegistration)
        {
            await Navigator.Instance.NavigateTo(PageType.DISCLAIMER, userRegistration);
        }

    }
}
