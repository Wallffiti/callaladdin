using CallAladdin.Commands;
using CallAladdin.Helper;
using CallAladdin.Model;
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
    public class EditRequestorProfileViewModel : BaseViewModel
    {
        private ILocationService locationService;
        private IUserService userService;
        private IUserProfileRepository userProfileRepository;
        private IUserIdentityRepository userIdentityRepository;
        private ICommand submitProfileChangeCmd;
        private ICommand changeProfileImageCmd;
        private bool isBusy;
        private bool isRegisteredAsContractor;
        private const string CHOOSE_PHOTO_FROM_CAMERA = "Choose photo from camera";
        private const string BROWSE_PHOTO_FROM_FOLDER = "Browse photo from folder";
        private string name;
        private string mobile;
        private string email;
        private string company;
        private string companyAddress;
        private string selectedCategory;
        private IList<string> countries;
        private IList<string> cities;
        private IList<string> categories;
        private string selectedCountry;
        private string selectedCity;
        private IList<string> photoOptionSelections;
        private string selectedPhotoOption;
        private UserProfile userProfile;
        private string imagePath;

        public ICommand SubmitProfileChangeCmd
        {
            get { return submitProfileChangeCmd; }
            set
            {
                submitProfileChangeCmd = value;
                OnPropertyChanged("SubmitProfileChangeCmd");
            }
        }

        public ICommand ChangeProfileImageCmd
        {
            get { return changeProfileImageCmd; }
            set
            {
                changeProfileImageCmd = value;
                OnPropertyChanged("ChangeProfileImageCmd");
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

                if (isRegisteredAsContractor)
                {
                    Navigator.Instance.OkAlert("Warning", "Once you have switched to CONTRACTOR, you can no longer switch back to REQUESTOR", "OK");
                }
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

        public string Company
        {
            get { return company; }
            set
            {
                company = value;
                UpdateUserProfile();
                OnPropertyChanged("Company");
            }
        }

        public string CompanyAddress
        {
            get { return companyAddress; }
            set
            {
                companyAddress = value;
                UpdateUserProfile();
                OnPropertyChanged("CompanyAddress");
            }
        }

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                UpdateUserProfile();
                OnPropertyChanged("SelectedCategory");
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

        public IList<string> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                //UpdateUserProfile();
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

        public UserProfile UserProfile
        {
            get { return userProfile; }
            set { userProfile = value; OnPropertyChanged("UserProfile"); }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value; UpdateUserProfile(); OnPropertyChanged("ImagePath");
            }
        }

        private UserProfileUserControlViewModel parentViewModel;
        private string userSystemUUID;

        public EditRequestorProfileViewModel(/*UserProfileUserControlViewModel parentViewModel*/ object owner)
        {
            var parentViewModel = (UserProfileUserControlViewModel)owner;
            this.parentViewModel = parentViewModel;
            locationService = new LocationService();
            userService = new UserService();
            userProfileRepository = new UserProfileRepository();
            userIdentityRepository = new UserIdentityRepository();
            SubmitProfileChangeCmd = new SubmitProfileChangeRequestorCommand(this);

            ChangeProfileImageCmd = new Xamarin.Forms.Command((e) =>
            {
                ChangeProfileImageAsync();
            }, (param) =>
            {
                if (param == null)
                    return false;

                var isBusy = (bool)param;

                return !isBusy;
            });

            LoadContractorOptions();

            this.SubscribeMeToThis(parentViewModel);
        }

        public async void ChangeProfileImageAsync()
        {
            string filePath = "";

            try
            {
                if (this.selectedPhotoOption == CHOOSE_PHOTO_FROM_CAMERA)
                {
                    filePath = await Utilities.TakePhoto(new Guid().ToString());
                }
                else if (this.selectedPhotoOption == BROWSE_PHOTO_FROM_FOLDER)
                {
                    filePath = await Utilities.PickPhoto();
                }
            }
            catch (Exception ex)
            {

            }

            this.ImagePath = filePath;
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
                ImagePath = userProfile.PathToProfileImage;
                userSystemUUID = userProfile.SystemUUID;
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

        public void RefreshRootPage()
        {
            parentViewModel.NotifyCompletion(parentViewModel, new EventArgs.ObserverEventArgs(Constants.TAB_SWITCH, Constants.USER_PROFILE));
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
                IsContractor = isRegisteredAsContractor,
                PathToProfileImage = imagePath,
                Category = selectedCategory,
                CompanyName = company,
                CompanyRegisteredAddress = companyAddress,
                SystemUUID = userSystemUUID
            };
        }

        public void SubmitProfileChanges()
        {
            if (IsBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            Navigator.Instance.ConfirmationAlert("Confirmation", "Submit your profile changes now?", "Yes", "No", async () =>
            {
                await DoSubmitProfileChanges();
            }, async () =>
            {
                await DoSubmitProfileChanges();
            });
        }

        private async Task DoSubmitProfileChanges()
        {
            if (IsBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            IsBusy = true;

            if (FormIsValid())
            {
                //if (IsRegisteredAsContractor)
                //{
                //    Navigator.Instance.ConfirmationAlert("Warning", "Once you have switched to CONTRACTOR, you can no longer switch back to REQUESTOR. Continue?", "Yes", "No", () =>
                //    {
                //        //For android
                //        SendToServer();
                //    },
                //    () =>
                //    {
                //        //For ios
                //        SendToServer();
                //    });
                //    IsBusy = false;
                //    return;
                //}

                await SendToServer();
            }

            IsBusy = false;
        }

        public async Task SendToServer()
        {
            if (this.userProfile == null)
                return;

            IsBusy = true;
            var userIdentity = userIdentityRepository.GetUserIdentity();
            var response = await userService.UpdateUserProfile(this.userProfile, userIdentity?.LocalId);

            if (response)
            {
                var profileData = userProfileRepository.GetUserProfile(this.userProfile.Email);
                if (profileData != null)
                {
                    profileData.Category = this.userProfile.Category;
                    profileData.City = this.userProfile.City;
                    profileData.CompanyName = this.userProfile.CompanyName;
                    profileData.CompanyRegisteredAddress = this.userProfile.CompanyRegisteredAddress;
                    profileData.Country = this.userProfile.Country;
                    profileData.Email = this.userProfile.Email;
                    profileData.IsContractor = this.userProfile.IsContractor;
                    profileData.Mobile = this.userProfile.Mobile;
                    profileData.Name = this.userProfile.Name;
                    profileData.PathToProfileImage = this.userProfile.PathToProfileImage;
                }

                var rowUpdated = userProfileRepository.CreateOrUpdate(profileData);

                if (rowUpdated < 0)
                {
                    Navigator.Instance.OkAlert("Error", "There is a problem storing user profile locally. Please try again later.", "OK", null, null);
                    IsBusy = false;
                    return;
                }

                
                //this.parentViewModel.UpdateUserProfile(this.userProfile);

                Navigator.Instance.OkAlert("Alert", "User profile is successfully updated.", "OK", async ()=> {
                    //For android
                    await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                    //this.parentViewModel.UpdateUserProfile(this.userProfile);
                    base.NotifyCompletion(this, new EventArgs.ObserverEventArgs(Constants.USER_PROFILE_UPDATE, Constants.REQUESTOR, this.UserProfile));
                    IsBusy = false;
                }, async () => 
                {
                    //For ios
                    await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                    //this.parentViewModel.UpdateUserProfile(this.userProfile);
                    base.NotifyCompletion(this, new EventArgs.ObserverEventArgs(Constants.USER_PROFILE_UPDATE, Constants.REQUESTOR, this.UserProfile));
                    IsBusy = false;
                });
                return;
            }

            Navigator.Instance.OkAlert("Error", "There is a problem updating user profile on the server. Please try again later.", "OK", null, null);
            IsBusy = false;
        }
    }
}
