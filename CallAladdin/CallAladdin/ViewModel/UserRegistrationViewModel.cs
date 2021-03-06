﻿using CallAladdin.Commands;
using CallAladdin.EventArgs;
using CallAladdin.Helper;
using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class UserRegistrationViewModel : BaseViewModel
    {
        private bool isBusy;
        private const string CHOOSE_PHOTO_FROM_CAMERA = "Choose photo from camera";
        private const string BROWSE_PHOTO_FROM_FOLDER = "Browse photo from folder";
        public event EventHandler OnProfilePictureChanged;
        public ICommand GoToAgreementCmd { get; set; }
        public ICommand ChangeProfileImageCmd { get; set; }
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
        private bool passwordIsNotValid;
        private bool isRegisteredAsContractor;
        private bool showPasswordField;
        private bool passwordMismatch;
        private string password;
        private string reTypePassword;
        private string profileImagePath;
        private UserRegistration userRegistration;
        private IList<string> photoOptionSelections;
        private string selectedPhotoOption;
        private IList<string> categories;

        public IList<string> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public IList<string> PhotoOptionSelections
        {
            get { return photoOptionSelections; }
            set { photoOptionSelections = value; OnPropertyChanged("PhotoOptionSelections"); }
        }

        public string SelectedPhotoOption
        {
            get { return selectedPhotoOption; }
            set { selectedPhotoOption = value; OnPropertyChanged("SelectedPhotoOption"); }
        }

        public bool PasswordIsNotValid
        {
            get
            {
                return passwordIsNotValid;
            }
            set
            {
                passwordIsNotValid = value;
                OnPropertyChanged("PasswordIsNotValid");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                UpdateUserRegistration();
                ValidateForm();
                OnPropertyChanged("Password");
            }
        }

        public string ReTypePassword
        {
            get
            {
                return reTypePassword;
            }
            set
            {
                reTypePassword = value;
                UpdateUserRegistration();
                ValidateForm();
                OnPropertyChanged("ReTypePassword");
            }
        }

        public bool ShowPasswordField
        {
            get
            {
                return showPasswordField;
            }
            set
            {
                showPasswordField = value;
                UpdateUserRegistration();
                ValidateForm();
                OnPropertyChanged("ShowPasswordField");
            }
        }

        public bool PasswordMismatch
        {
            get { return passwordMismatch; }
            set
            {
                passwordMismatch = value;
                OnPropertyChanged("PasswordMismatch");
            }
        }

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
            ChangeProfileImageCmd = new ChangeProfileImageCommand(this);
            ShowPasswordField = !Auth.UsePasswordless();
            Mobile = Helper.Utilities.GetPhoneNumber();
            LoadImageUploaderOptions();
            LoadContractorOptions();
        }

        private void LoadImageUploaderOptions()
        {
            PhotoOptionSelections = new List<string>
            {
                CHOOSE_PHOTO_FROM_CAMERA,
                BROWSE_PHOTO_FROM_FOLDER
            };
            SelectedPhotoOption = BROWSE_PHOTO_FROM_FOLDER;
        }

        private void LoadContractorOptions()
        {
            Categories = new List<string>
            {
                Constants.CONSTRUCTION_TILING_PAINTING,
                Constants.WIRING_LIGHING,
                Constants.INTERIOR_DESIGN_CARPENTERS,

                Constants.LAMINATED_TIMBER_FLOORING,
                Constants.CURTAIN_CARPET_WALLPAPER,
                Constants.SIGNBOARD,

                Constants.AIR_CONDITIONER,
                Constants.PLASTERED_CEILING,
                Constants.ALUMINIUM_GLASSWORKS,

                Constants.ALARM_CCTV,
                Constants.CLEANING_SERVICES,
                Constants.LANDSCAPING_POND,

                Constants.GATES_STEEL_WORKS,
                Constants.PLUMBER,
                Constants.PEST_CONTROL,

                Constants.FENGSHUI_CONSULTATION,
                Constants.GENERAL_WORKERS,
                Constants.HOUSE_MOVERS
            };
        }

        public void UpdateUserRegistration()
        {
            var correctedPhone = "";

            if (!string.IsNullOrEmpty(this.mobile))
            {
                var tempPhone = this.mobile.Replace(" ", "").Trim();
                correctedPhone = tempPhone.StartsWith("+") ? tempPhone : "+" + tempPhone;
            }

            UserRegistration = new UserRegistration()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = this.name,
                Mobile = correctedPhone, //this.mobile,
                Email = this.email,
                City = this.selectedCity,
                Country = this.selectedCountry,
                IsRegisteredAsContractor = this.isRegisteredAsContractor,
                Category = this.selectedCategory,
                CompanyName = this.company,
                CompanyAddress = this.companyAddress,
                Password = this.password,
                ReTypePassword = this.reTypePassword,
                ProfileImagePath = this.profileImagePath
            };
        }

        public void ValidateForm()
        {
            this.EmailIsNotValid = string.IsNullOrEmpty(this.email) ? false : !Validators.ValidateEmail(this.email);
            if (Auth.UsePasswordless())
            {
                this.PasswordIsNotValid = false;
            }
            else
            {
                this.PasswordIsNotValid = string.IsNullOrEmpty(this.password) ? false : !Validators.ValidatePassword(this.password);

                if (!string.IsNullOrEmpty(userRegistration.Password) && !string.IsNullOrEmpty(userRegistration.ReTypePassword))
                {
                    this.PasswordMismatch = string.Compare(userRegistration.Password, userRegistration.ReTypePassword) != 0;
                }
                else
                {
                    this.PasswordMismatch = false;
                }
            }
        }

        public async void NavigateToAgreement(UserRegistration userRegistration)
        {
            if (isBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            isBusy = true;
            await Navigator.Instance.NavigateTo(PageType.DISCLAIMER, userRegistration);
            await Task.Delay(1500);
            isBusy = false;
        }

        public async void ChangeProfileImageAsync()
        {
            string filePath = "";

            try
            {
                if (this.selectedPhotoOption == CHOOSE_PHOTO_FROM_CAMERA)
                {
                    filePath = await Utilities.TakePhoto(userRegistration.Guid);
                }
                else if (this.selectedPhotoOption == BROWSE_PHOTO_FROM_FOLDER)
                {
                    filePath = await Utilities.PickPhoto();
                }
            }
            catch (Exception ex)
            {

            }

            this.profileImagePath = filePath;
            OnProfilePictureChanged?.Invoke(this, new ProfilePhotoChangedEventArgs(filePath));
        }
    }
}
