using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CallAladdin.ViewModel
{
    public class EditRequestorProfileViewModel : BaseViewModel
    {
        private ILocationService locationService;
        private bool isBusy;
        private bool isRegisteredAsContractor;
        private const string CHOOSE_PHOTO_FROM_CAMERA = "Choose photo from camera";
        private const string BROWSE_PHOTO_FROM_FOLDER = "Browse photo from folder";
        private string name;
        private string mobile;
        private string email;
        private IList<string> countries;
        private IList<string> cities;
        private string selectedCountry;
        private string selectedCity;
        private IList<string> photoOptionSelections;
        private string selectedPhotoOption;
        private UserProfile userProfile;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public bool IsRegisteredAsContractor
        {
            get { return isRegisteredAsContractor; }
            set
            {
                isRegisteredAsContractor = value;
                UpdateUserProfile();
                ValidateForm();
                OnPropertyChanged("IsRegisteredAsContractor");
            }
        }


        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        public string Mobile
        {
            get { return mobile; }
            set
            {
                mobile = value;
                OnPropertyChanged("Mobile");
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                UpdateUserProfile();
                ValidateForm();
                OnPropertyChanged("Name");
            }
        }

        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                UpdateUserProfile();
                ValidateForm();
                OnPropertyChanged("SelectedCountry");
            }
        }

        public string SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                UpdateUserProfile();
                ValidateForm();
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

        public UserProfile UserProfile
        {
            get { return userProfile; }
            set { userProfile = value; OnPropertyChanged("UserProfile"); }
        }

        public EditRequestorProfileViewModel(/*UserProfile userProfile*/)
        {
            locationService = new LocationService();
        }

        private void PopulateLocations()
        {
            Task.Run(async () =>
            {
                //Long processes below
                this.IsBusy = true;
                this.Countries = await locationService.GetCountries();
                this.Cities = await locationService.GetCities("all");  //right now no parameter needed to filter cities

                this.SelectedCountry = userProfile?.Country;
                this.SelectedCity = userProfile?.City;

                this.IsBusy = false;
            });
        }

        public void PopulateData(UserProfile userProfile)
        {
            PopulateLocations();
            UserProfile = userProfile;

            if (userProfile != null)
            {
                Name = userProfile.Name;
                Mobile = userProfile.Mobile;
                Email = userProfile.Email;
                //SelectedCity = userProfile.City;
                //SelectedCountry = userProfile.Country;
                IsRegisteredAsContractor = userProfile.IsContractor;
            }

            LoadImageUploaderOptions();
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

        private void ValidateForm()
        {
            //TODO
        }

        private void UpdateUserProfile()
        {
            //TODO
        }
    }
}
