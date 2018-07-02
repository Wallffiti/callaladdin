using CallAladdin.Helper;
using CallAladdin.Helper.Interfaces;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CallAladdin.ViewModel
{
    public class JobViewCommonUserControlViewModel : BaseViewModel
    {
        private BaseViewModel parentViewModel;
        private string contractorIcon;
        private string jobRequestType;
        private string jobRequestImage;
        private string title;
        private string scopeOfWork;
        private bool isBusy;
        private bool allowPlaying;
        private bool allowStopping;
        private DateTime selectedStartDate;
        private DateTime selectedEndDate;
        private TimeSpan selectedStartTime;
        private TimeSpan selectedEndTime;
        private string city;
        private string country;
        private bool hasAudio;
        private IMediaPlayer mediaPlayer;

        public string ContractorIcon
        {
            get
            {
                return contractorIcon;
            }
            set
            {
                contractorIcon = value;
                OnPropertyChanged(nameof(ContractorIcon));
            }
        }

        public string JobRequestType
        {
            get
            {
                return jobRequestType;
            }
            set
            {
                jobRequestType = value;
                OnPropertyChanged(nameof(JobRequestType));
            }
        }

        public string JobRequestImage
        {
            get
            {
                return jobRequestImage;
            }
            set
            {
                jobRequestImage = value;
                OnPropertyChanged(nameof(JobRequestImage));
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string ScopeOfWork
        {
            get
            {
                return scopeOfWork;
            }
            set
            {
                scopeOfWork = value;
                OnPropertyChanged(nameof(ScopeOfWork));
            }
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public bool AllowPlaying
        {
            get
            {
                return allowPlaying;
            }
            set
            {
                allowPlaying = value;
                OnPropertyChanged(nameof(AllowPlaying));
            }
        }

        public bool AllowStopping
        {
            get
            {
                return allowStopping;
            }
            set
            {
                allowStopping = value;
                OnPropertyChanged(nameof(AllowStopping));
            }
        }

        public DateTime SelectedStartDate
        {
            get
            {
                return selectedStartDate;
            }
            set
            {
                selectedStartDate = value;
                OnPropertyChanged(nameof(SelectedStartDate));
            }
        }
        public DateTime SelectedEndDate
        {
            get
            {
                return selectedEndDate;
            }
            set
            {
                selectedEndDate = value;
                OnPropertyChanged(nameof(SelectedEndDate));
            }
        }
        public TimeSpan SelectedStartTime
        {
            get
            {
                return selectedStartTime;
            }
            set
            {
                selectedStartTime = value;
                OnPropertyChanged(nameof(SelectedStartTime));
            }
        }
        public TimeSpan SelectedEndTime
        {
            get
            {
                return selectedEndTime;
            }
            set
            {
                selectedEndTime = value;
                OnPropertyChanged(nameof(SelectedEndTime));
            }
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public bool HasAudio
        {
            get
            {
                return hasAudio;
            }
            set
            {
                hasAudio = value;
                OnPropertyChanged(nameof(HasAudio));
            }
        }

        private string voiceNotePath;

        public ICommand PlayRecordedCmd { get; set; }
        public ICommand StopCmd { get; set; }

        public JobViewCommonUserControlViewModel(object owner)
        {
            parentViewModel = (BaseViewModel)owner;
            Job selectedJob = null;

            if (parentViewModel is JobViewViewModel)
            {
                var jobViewViewModel = (JobViewViewModel)parentViewModel;
                selectedJob = jobViewViewModel.GetSelectedJob();
            }
            else if (parentViewModel is JobHistoryViewViewModel)
            {
                var jobHistoryViewViewModel = (JobHistoryViewViewModel)parentViewModel;
                selectedJob = jobHistoryViewViewModel.GetSelectedJob();
            }

            if (selectedJob != null)
            {
                this.JobRequestImage = selectedJob.ImagePath;
                this.JobRequestType = selectedJob.Category;
                this.ContractorIcon = Utilities.GetIconByCategory(selectedJob.Category);
                this.Title = selectedJob.Title;
                this.ScopeOfWork = selectedJob.ScopeOfWork;
                this.SelectedStartDate = selectedJob.StartDateTime;
                this.SelectedEndDate = selectedJob.EndDateTime;
                this.SelectedStartTime = new TimeSpan(selectedJob.StartDateTime.Hour, selectedJob.StartDateTime.Minute, 0);
                this.SelectedEndTime = new TimeSpan(selectedJob.EndDateTime.Hour, selectedJob.EndDateTime.Minute, 0);
                this.City = selectedJob.City;
                this.Country = selectedJob.Country;

                if (!string.IsNullOrEmpty(selectedJob.VoiceNotePath))
                {
                    HasAudio = true;
                    voiceNotePath = selectedJob.VoiceNotePath;
                    AllowPlaying = true;
                    mediaPlayer = DependencyService.Get<IMediaPlayer>(DependencyFetchTarget.NewInstance);
                }
            }

            PlayRecordedCmd = new Xamarin.Forms.Command(e =>
            {
                PlayRecordedAudio();
            },
            param =>
            {
                if (param == null)
                    return false;

                var isBusy = (bool)param;
                return !isBusy;
            });
            StopCmd = new Xamarin.Forms.Command(e =>
            {
                StopPlayingRecordedAudio();
            },
            param =>
            {
                if (param == null)
                    return false;

                var isBusy = (bool)param;
                return !isBusy;
            });
        }

        private void PlayRecordedAudio()
        {
            if (string.IsNullOrEmpty(voiceNotePath))
                return;

            AllowPlaying = false;
            AllowStopping = true;
            mediaPlayer.Play(voiceNotePath, (s, e) =>
            {
                AllowStopping = false;
                AllowPlaying = true;
            });
        }

        private void StopPlayingRecordedAudio()
        {
            if (string.IsNullOrEmpty(voiceNotePath))
                return;

            AllowStopping = false;
            AllowPlaying = true;

            mediaPlayer.StopPlaying();
        }
    }
}
