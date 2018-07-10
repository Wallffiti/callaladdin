using CallAladdin.Model;
using CallAladdin.Repositories;
using CallAladdin.Repositories.Interfaces;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using CallAladdin.Helper;
using CallAladdin.Commands;
using CallAladdin.Helper.Interfaces;
using Xamarin.Forms;
using System.IO;
using static CallAladdin.Constants;

namespace CallAladdin.ViewModel
{
    public class JobRequestViewModel : BaseViewModel
    {
        private const string CHOOSE_PHOTO_FROM_CAMERA = "Choose photo from camera";
        private const string BROWSE_PHOTO_FROM_FOLDER = "Browse photo from folder";

        private string contractorIcon;
        private string jobRequestType;
        private string jobRequestImage;
        private string title;
        private string scopeOfWork;
        private DateTime selectedStartDate;
        private DateTime selectedEndDate;
        private DateTime minStartDate;
        private DateTime minEndDate;
        private TimeSpan selectedStartTime;
        private TimeSpan selectedEndTime;
        private string selectedCountry;
        private string selectedCity;
        private string location;
        private int contractorsAvailable;
        private IList<string> countries;
        private IList<string> cities;
        private IList<string> photoOptionSelections;
        private string selectedPhotoOption;
        private bool isBusy;
        private ILocationService locationService;
        private IUserProfileRepository userProfileRepository;
        private Job jobRequest;
        private IMediaPlayer mediaPlayer;

        public string ContractorIcon
        {
            get { return contractorIcon; }
            set { contractorIcon = value; OnPropertyChanged("ContractorIcon"); }
        }

        public string JobRequestType
        {
            get { return jobRequestType; }
            set { jobRequestType = value; OnPropertyChanged("JobRequestType"); }
        }

        public UserProfile UserProfile { get; set; }

        public string JobRequestImage
        {
            get { return jobRequestImage; }
            set { jobRequestImage = value; UpdateJobRequest(); OnPropertyChanged("JobRequestImage"); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; UpdateJobRequest(); OnPropertyChanged("Title"); }
        }

        public string ScopeOfWork
        {
            get { return scopeOfWork; }
            set { scopeOfWork = value; UpdateJobRequest(); OnPropertyChanged("ScopeOfWork"); }
        }

        public DateTime MinStartDate
        {
            get { return minStartDate; }
            set { minStartDate = value; OnPropertyChanged("MinStartDate"); }
        }

        public DateTime MinEndDate
        {
            get { return minEndDate; }
            set { minEndDate = value; OnPropertyChanged("MinEndDate"); }
        }

        public DateTime SelectedStartDate
        {
            get { return selectedStartDate; }
            set
            {
                var oldValue = selectedStartDate;
                selectedStartDate = value;

                if (ValidateDateTime())
                {
                    UpdateJobRequest();
                }
                else
                {
                    selectedStartDate = oldValue;
                }
                OnPropertyChanged("SelectedStartDate");
            }
        }

        public DateTime SelectedEndDate
        {
            get { return selectedEndDate; }
            set
            {
                var oldValue = selectedEndDate;
                selectedEndDate = value;

                if (ValidateDateTime())
                {
                    UpdateJobRequest();
                }
                else
                {
                    selectedEndDate = oldValue;
                }

                OnPropertyChanged("SelectedEndDate");
            }
        }

        public TimeSpan SelectedStartTime
        {
            get { return selectedStartTime; }
            set
            {
                var oldValue = selectedStartTime;
                selectedStartTime = value;

                if (ValidateDateTime())
                {
                    UpdateJobRequest();
                }
                else
                {
                    selectedStartTime = oldValue;
                }

                OnPropertyChanged("SelectedStartTime");
            }
        }

        public TimeSpan SelectedEndTime
        {
            get { return selectedEndTime; }
            set
            {
                var oldValue = selectedEndTime;
                selectedEndTime = value;

                if (ValidateDateTime())
                {
                    UpdateJobRequest();
                }
                else
                {
                    selectedEndTime = oldValue;
                }

                OnPropertyChanged("SelectedEndTime");
            }
        }

        public string SelectedCountry
        {
            get { return selectedCountry; }
            set { selectedCountry = value; UpdateJobRequest(); OnPropertyChanged("SelectedCountry"); }
        }

        public string SelectedCity
        {
            get { return selectedCity; }
            set { selectedCity = value; UpdateJobRequest(); OnPropertyChanged("SelectedCity"); }
        }

        public string Location
        {
            get { return location; }
            set { location = value; UpdateJobRequest(); OnPropertyChanged("Location"); }
        }

        public int ContractorsAvailable
        {
            get { return contractorsAvailable; }
            set { contractorsAvailable = value; OnPropertyChanged("ContractorsAvailable"); }
        }

        public IList<string> Countries
        {
            get { return countries; }
            set { countries = value; OnPropertyChanged("Countries"); }
        }

        public IList<string> Cities
        {
            get { return cities; }
            set { cities = value; OnPropertyChanged("Cities"); }
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

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged("IsBusy"); }
        }

        public Job JobRequest
        {
            get { return jobRequest; }
            set { jobRequest = value; OnPropertyChanged("JobRequest"); }
        }

        public ICommand SearchLocationCmd { get; set; }
        public ICommand ChangeProfileImageCmd { get; set; }
        public ICommand SubmitJobRequestCmd { get; set; }

        public ICommand RecordVoiceCmd { get; set; }
        public ICommand PlayRecordedCmd { get; set; }
        public ICommand StopCmd { get; set; }
        public ICommand DeleteCmd { get; set; }

        private bool allowRecording;
        public bool AllowRecording
        {
            get { return allowRecording; }
            set { allowRecording = value; OnPropertyChanged("AllowRecording"); }
        }
        private bool allowPlaying;
        public bool AllowPlaying
        {
            get { return allowPlaying; }
            set { allowPlaying = value; OnPropertyChanged("AllowPlaying"); }
        }
        private bool allowStopping;
        public bool AllowStopping
        {
            get { return allowStopping; }
            set { allowStopping = value; OnPropertyChanged("AllowStopping"); }
        }

        private bool allowDeleting;
        public bool AllowDeleting
        {
            get { return allowDeleting; }
            set { allowDeleting = value; OnPropertyChanged("AllowDeleting"); }
        }

        //private string userSystemUUID;
        private HomeUserControlViewModel parentViewModel;
        private IJobService jobService;
        private string mediaFilePath;   //unique per session
        private MediaState mediaState;

        public JobRequestViewModel(object owner)
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            mediaFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), guid + ".3gpp");
            var parentViewModel = (HomeUserControlViewModel)owner;
            this.parentViewModel = parentViewModel;
            if (parentViewModel != null)
            {
                ContractorIcon = Helper.Utilities.GetIconByCategory(parentViewModel.CurrentSelectedCategory);
                JobRequestType = parentViewModel.CurrentSelectedCategory;
                UserProfile = parentViewModel.UserProfile;
                this.SubscribeMeToThis(parentViewModel);
            }
            jobService = new JobService();
            locationService = new LocationService();
            userProfileRepository = new UserProfileRepository();
            PopulateLocations();
            SetInitialTimes();
            ContractorsAvailable = 0;   //TODO: call api
            SearchLocationCmd = new SearchLocationCommand(this);
            SubmitJobRequestCmd = new SubmitJobCommand(this);
            ChangeProfileImageCmd = new Xamarin.Forms.Command(e =>
            {
                ChangeProfileImageAsync();
            }, (param) =>
            {
                return true;
            });
            RecordVoiceCmd = new Xamarin.Forms.Command(e =>
            {
                Navigator.Instance.OkAlert("Alert", "Begin recording your voice once you tapped OK, and tap on the solid rectangular icon on your right to stop", "OK", () =>
                {
                    //for android
                    BeginRecording();
                },
                () =>
                {
                    //for ios
                    BeginRecording();
                });

            }, param =>
            {
                return true;
            });
            PlayRecordedCmd = new Xamarin.Forms.Command(e =>
            {
                PlayRecordedVoice();
            }, param =>
            {
                return true;
            });
            StopCmd = new Xamarin.Forms.Command(e =>
            {
                StopMedia();
            }, param =>
            {
                return true;
            });
            DeleteCmd = new Xamarin.Forms.Command(e =>
            {
                Navigator.Instance.ConfirmationAlert("Alert", "Are you sure you want to delete your voice message?", "Yes", "No", () =>
                {
                    //for android
                    RemoveRecordedVoice();
                },
                () =>
                {
                    //for ios
                    RemoveRecordedVoice();
                });

            }, param =>
            {
                return true;
            });
            LoadImageUploaderOptions();
            InitializeVoiceButtons();
            SetupMediaPlayer();
            InitializeMinStartAndEndDates();
        }

        private void InitializeMinStartAndEndDates()
        {
            MinStartDate = DateTime.Now;
            MinEndDate = DateTime.Now;
        }

        private bool ValidateDateTime()
        {
            bool result = true;

            if (selectedStartDate != default(DateTime) && selectedEndDate != default(DateTime) && selectedStartTime != default(TimeSpan) && selectedEndTime != default(TimeSpan))
            {
                if (/*!isBusy && */ GetStartDateTime() > GetEndDateTime())
                {
                    Navigator.Instance.OkAlert("Error", "Preferred end date time should be later than preferred start date time", "OK");
                    result = false;
                }
            }

            return result;
        }

        private DateTime GetStartDateTime()
        {
            return new DateTime(selectedStartDate.Year, selectedStartDate.Month, selectedStartDate.Day, selectedStartTime.Hours, selectedStartTime.Minutes, selectedStartTime.Seconds);
        }

        private DateTime GetEndDateTime()
        {
            return new DateTime(selectedEndDate.Year, selectedEndDate.Month, selectedEndDate.Day, selectedEndTime.Hours, selectedEndTime.Minutes, selectedEndTime.Seconds);
        }

        private void RemoveRecordedVoice()
        {
            AllowRecording = true;
            AllowStopping = false;
            AllowPlaying = false;

            if (File.Exists(mediaFilePath))
            {
                File.Delete(mediaFilePath);
            }

            AllowDeleting = false; //if file is successfully deleted
            mediaState = MediaState.NEUTRAL;
        }

        private void StopMedia()
        {
            AllowRecording = false;
            AllowStopping = false;
            AllowPlaying = File.Exists(mediaFilePath);   //if there is a recorded file
            AllowDeleting = File.Exists(mediaFilePath);   //if there is a recorded file

            if (mediaState == MediaState.RECORD)
            {
                mediaPlayer.StopRecording();
            }
            else if (mediaState == MediaState.PLAY)
            {
                mediaPlayer.StopPlaying();
            }

            mediaState = MediaState.STOP;
        }

        private void PlayRecordedVoice()
        {
            AllowRecording = false;
            AllowStopping = true;
            AllowPlaying = false;
            AllowDeleting = false;
            mediaState = MediaState.PLAY;
            mediaPlayer.Play(mediaFilePath, (s, e) =>
            {
                AllowStopping = false;
                AllowPlaying = true;
                AllowDeleting = true;
            });
        }

        private void BeginRecording()
        {
            AllowRecording = false;
            AllowStopping = true;
            AllowPlaying = false;
            AllowDeleting = false;
            mediaState = MediaState.RECORD;
            mediaPlayer.Record(mediaFilePath);
        }

        private void SetupMediaPlayer()
        {
            mediaState = MediaState.NEUTRAL;

            //if (Device.OS == TargetPlatform.Android)
            //{
            //    //for android
            //    mediaPlayer = new AndroidMediaPlayer();
            //}
            //else if (Device.OS == TargetPlatform.iOS)
            //{
            //    //for ios
            //}

            mediaPlayer = DependencyService.Get<IMediaPlayer>(DependencyFetchTarget.NewInstance);
        }

        private void InitializeVoiceButtons()
        {
            AllowRecording = true;
            AllowStopping = false;
            AllowPlaying = false;
            AllowDeleting = false;
        }

        public void RefreshRootPage()
        {
            parentViewModel.NotifyCompletion(parentViewModel, new EventArgs.ObserverEventArgs(Constants.TAB_SWITCH, Constants.HOME));
        }

        private void SetInitialTimes()
        {
            var now = DateTime.Now;
            SelectedStartDate = now;
            SelectedEndDate = now;
            SelectedStartTime = new TimeSpan(now.Hour, now.Minute, now.Second);

            if (SelectedStartTime.Add(new TimeSpan(1, 0, 0)) > new TimeSpan(23, 59, 59))
            {
                SelectedEndDate = now.AddDays(1);
                SelectedEndTime = new TimeSpan(0, now.Minute, now.Second);
            }
            else
            {
                SelectedEndTime = new TimeSpan(now.Hour + 1, now.Minute, now.Second);
            }
        }

        public /*async*/ void CreateJobRequest()
        {
            if (IsBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            Navigator.Instance.ConfirmationAlert("Confirmation", "Create a job now?", "Yes", "No", async () =>
            {
                await DoCreateJob();
            }, async () =>
            {
                await DoCreateJob();
            });
        }

        private async Task DoCreateJob()
        {
            if (IsBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            IsBusy = true;
            var response = await jobService.CreateRequest(new Model.Requests.JobRequestRequest
            {
                Address = jobRequest.Address,
                Category = jobRequest.Category,
                City = jobRequest.City,
                Country = jobRequest.Country,
                StartDateTime = jobRequest.StartDateTime,
                EndDateTime = jobRequest.EndDateTime,
                ImagePath = jobRequest.ImagePath,
                RequestorSystemUUID = jobRequest.RequestorSystemUUID,
                ScopeOfWork = jobRequest.ScopeOfWork,
                Title = JobRequest.Title,
                VoiceNotePath = mediaFilePath,  //not sure it works?
                //TODO: to complete remaining fields
            });

            if (response.IsSuccess)
            {
                try
                {
                    base.NotifyCompletion(this, new EventArgs.ObserverEventArgs(Constants.JOB_REQUEST_LIST_UPDATE));
                    Navigator.Instance.OkAlert("Job Submited", "Your job has been posted to available contractors in town. Please verify contractors quality of work before finalising.", "OK");
                    await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                    //parentViewModel?.SetDashboardTab();
                    base.NotifyCompletion(this, new EventArgs.ObserverEventArgs(Constants.TAB_SWITCH, Constants.DASHBOARD));
                    Navigator.Instance.OkAlert("Disclaimer", "Call Aladdin bears no responsibility on the outcome of the work by the contractor.", "OK");
                    return;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    IsBusy = false;
                }
                return;
            }

            Navigator.Instance.OkAlert("Error", "There is a problem attempting to create a job. Please try again later.", "OK");
            IsBusy = false;
        }

        private void UpdateJobRequest()
        {
            JobRequest = new Job
            {
                Address = location,
                Category = jobRequestType,
                City = selectedCity,
                Country = selectedCountry,
                StartDateTime = GetStartDateTime(),
                EndDateTime = GetEndDateTime(),
                CreatedDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now,
                ImagePath = jobRequestImage,
                ScopeOfWork = scopeOfWork,
                Title = title,
                RequestorSystemUUID = UserProfile?.SystemUUID  //userSystemUUID
                //TODO: to complete remaining fields
            };
        }

        private async void ChangeProfileImageAsync()
        {
            string filePath = "";

            try
            {
                if (this.selectedPhotoOption == CHOOSE_PHOTO_FROM_CAMERA)
                {
                    filePath = await Utilities.TakePhoto(Guid.NewGuid().ToString().Replace("-", ""));
                }
                else if (this.selectedPhotoOption == BROWSE_PHOTO_FROM_FOLDER)
                {
                    filePath = await Utilities.PickPhoto();
                }
            }
            catch (Exception ex)
            {

            }

            JobRequestImage = filePath;
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

        private void PopulateLocations()
        {
            Task.Run(async () =>
            {
                //Long processes below
                IsBusy = true;
                Countries = await locationService.GetCountries();
                Cities = await locationService.GetCities("all");  //right now no parameter needed to filter cities
                //var userProfile = userProfileRepository.GetAll()?.LastOrDefault();
                await Task.Delay(1000);
                SelectedCountry = UserProfile?.Country;// userProfile?.Country;
                SelectedCity = UserProfile?.City; //userProfile?.City;
                IsBusy = false;
            });
        }
    }
}
