using CallAladdin.Helper;
using CallAladdin.Helper.Interfaces;
using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static CallAladdin.Constants;

namespace CallAladdin.ViewModel
{
    public class EditJobViewModel : BaseViewModel
    {
        private const string CHOOSE_PHOTO_FROM_CAMERA = "Choose photo from camera";
        private const string BROWSE_PHOTO_FROM_FOLDER = "Browse photo from folder";

        private IJobService jobService;
        private BaseViewModel parentViewModel;
        //private Job selectedJob;
        private string contractorIcon;
        private string jobRequestType;
        private string jobRequestImage;
        private string selectedPhotoOption;
        private IList<string> photoOptionSelections;
        private Job jobRequest;
        private string title;
        private string scopeOfWork;
        private bool isBusy;

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

        private bool ValidateDateTime()
        {
            bool result = true;

            if (selectedStartDate != default(DateTime) && selectedEndDate != default(DateTime) && selectedStartTime != default(TimeSpan) && selectedEndTime != default(TimeSpan))
            {
                if (GetStartDateTime() > GetEndDateTime())
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

        public Job JobRequest
        {
            get { return jobRequest; }
            set { jobRequest = value; OnPropertyChanged("JobRequest"); }
        }

        public ICommand ChangeProfileImageCmd { get; set; }

        public ICommand RecordVoiceCmd { get; set; }
        public ICommand PlayRecordedCmd { get; set; }
        public ICommand StopCmd { get; set; }
        public ICommand DeleteCmd { get; set; }

        private string mediaFilePath;   //unique per session
        private MediaState mediaState;
        private IMediaPlayer mediaPlayer;
        private DateTime minEndDate;
        private DateTime minStartDate;
        private DateTime selectedStartDate;
        private DateTime selectedEndDate;
        private TimeSpan selectedStartTime;
        private TimeSpan selectedEndTime;

        public EditJobViewModel(object owner)
        {
            Job selectedJob = null;
            jobService = new JobService();

            if (owner is DashboardUserControlViewModel)
            {
                parentViewModel = (DashboardUserControlViewModel)owner;
                selectedJob = ((DashboardUserControlViewModel)parentViewModel).GetSelectedJob();
            }
            else if (owner is JobViewViewModel)
            {
                parentViewModel = (JobViewViewModel)owner;
                selectedJob = ((JobViewViewModel)parentViewModel).GetSelectedJob();
            }

            if (selectedJob != null)
            {
                ContractorIcon = Helper.Utilities.GetIconByCategory(selectedJob.Category);
                JobRequestType = selectedJob.Category;
                JobRequestImage = selectedJob.ImagePath;
                Title = selectedJob.Title;
                ScopeOfWork = selectedJob.ScopeOfWork;
                SelectedStartDate = selectedJob.StartDateTime;
                SelectedEndDate = selectedJob.EndDateTime;
                SelectedStartTime = new TimeSpan(selectedJob.StartDateTime.Hour, selectedJob.StartDateTime.Minute, 0);
                SelectedEndTime = new TimeSpan(selectedJob.EndDateTime.Hour, selectedJob.EndDateTime.Minute, 0);
                //TODO min start end date time
            }

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

            if (selectedJob != null && !string.IsNullOrEmpty(selectedJob.VoiceNotePath))
            {
                mediaFilePath = selectedJob.VoiceNotePath;
            }

            InitializeVoiceButtons();
            SetupMediaPlayer();
            InitializeMinStartAndEndDates();
        }

        private void InitializeMinStartAndEndDates()
        {
            MinStartDate = DateTime.Now;
            MinEndDate = DateTime.Now;
        }

        private void SetupMediaPlayer()
        {
            mediaState = MediaState.NEUTRAL;
            mediaPlayer = DependencyService.Get<IMediaPlayer>(DependencyFetchTarget.NewInstance);
        }

        private void InitializeVoiceButtons()
        {
            if (string.IsNullOrEmpty(mediaFilePath))
            {
                AllowRecording = true;
                AllowStopping = false;
                AllowPlaying = false;
                AllowDeleting = false;
                GenerateMediaFilePath();
            }
            else
            {
                AllowRecording = false;
                AllowStopping = false;
                AllowPlaying = true;
                AllowDeleting = true;
            }
        }

        private void GenerateMediaFilePath()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            mediaFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), guid + ".3gpp");
        }

        private void StopMedia()
        {
            AllowRecording = false;
            AllowStopping = false;
            AllowPlaying = File.Exists(mediaFilePath) || Validators.ValidateUrl(mediaFilePath);   //if there is a recorded file
            AllowDeleting = File.Exists(mediaFilePath) || Validators.ValidateUrl(mediaFilePath);   //if there is a recorded file

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

        private void RemoveRecordedVoice()
        {
            AllowRecording = true;
            AllowStopping = false;
            AllowPlaying = false;

            if (Validators.ValidateUrl(mediaFilePath))
            {
                //if media exists and is stored in backend, then reset it to a new path so that it can overwrite the copy in backend
                GenerateMediaFilePath();
            }
            else
            {
                if (File.Exists(mediaFilePath))
                {
                    File.Delete(mediaFilePath);
                }
            }

            AllowDeleting = false; //if file is successfully deleted
            mediaState = MediaState.NEUTRAL;
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

        private async void ChangeProfileImageAsync()
        {
            string filePath = "";

            try
            {
                if (selectedPhotoOption == CHOOSE_PHOTO_FROM_CAMERA)
                {
                    filePath = await Utilities.TakePhoto(Guid.NewGuid().ToString().Replace("-", ""));
                }
                else if (selectedPhotoOption == BROWSE_PHOTO_FROM_FOLDER)
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

        private void UpdateJobRequest()
        {
            JobRequest = new Job
            {
                //TODO
            };
        }
    }
}
