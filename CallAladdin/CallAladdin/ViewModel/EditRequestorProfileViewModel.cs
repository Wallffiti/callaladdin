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
    public class EditRequestorProfileViewModel : BaseViewModel
    {
        private ILocationService locationService;
        private ICommand submitProfileChangeCmd;
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

        public ICommand SubmitProfileChangeCmd
        {
            get { return submitProfileChangeCmd; }
            set
            {
                submitProfileChangeCmd = value;
                OnPropertyChanged("SubmitProfileChangeCmd");
            }
        }

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
                //ValidateForm();
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
                //ValidateForm();
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
                //ValidateForm();
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
                //ValidateForm();
                OnPropertyChanged("SelectedCity");
            }
        }

        public IList<string> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                //UpdateUserProfile();
                OnPropertyChanged("Countries");
            }
        }

        public IList<string> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                //UpdateUserProfile();
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

        public EditRequestorProfileViewModel()
        {
            locationService = new LocationService();
            SubmitProfileChangeCmd = new Xamarin.Forms.Command((e) =>
            {
                SubmitProfileChanges();
            },
            (param) =>
            {
                if (param != null)
                {
                    var userProfile = (UserProfile)param;

                    if (userProfile != null)
                    {
                        return !string.IsNullOrEmpty(userProfile.Name) && !string.IsNullOrEmpty(userProfile.City) && !string.IsNullOrEmpty(userProfile.Country);
                    }

                    //var isBusy = (bool)param;
                    //return !isBusy;
                }

                return false;
            });
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
            LoadImageUploaderOptions();

            if (userProfile != null)
            {
                Name = userProfile.Name;
                Mobile = userProfile.Mobile;
                Email = userProfile.Email;
                IsRegisteredAsContractor = userProfile.IsContractor;
                SelectedCity = userProfile.City;
                SelectedCountry = userProfile.Country;
            }

            UpdateUserProfile();

            //UserProfile = userProfile;

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

        private bool FormIsValid()
        {
            if (string.IsNullOrEmpty(userProfile?.Name) || string.IsNullOrEmpty(userProfile?.City) || string.IsNullOrEmpty(userProfile?.Country))
            {
                return false;
            }

            return true;
        }

        private void UpdateUserProfile()
        {
            UserProfile = new UserProfile
            {
                Name = name,
                Mobile = mobile,
                Email = email,
                City = selectedCity,
                Country = selectedCountry,
                IsContractor = isRegisteredAsContractor
            };
        }

        public void SubmitProfileChanges()
        {
            if (IsBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            IsBusy = true;

            if (FormIsValid())
            {
                if (IsRegisteredAsContractor)
                {
                    Navigator.Instance.ConfirmationAlert("Warning", "Once you have switched to CONTRACTOR, you can no longer switch back to REQUESTOR. Continue?", "Yes", "No", () =>
                    {
                        //For android
                        SendToServer();
                    },
                    () =>
                    {
                        //For ios
                        SendToServer();
                    });
                    IsBusy = false;
                    return;
                }

                SendToServer();
            }

            IsBusy = false;
        }

        public void SendToServer()
        {
            //TODO
        }
    }
}
