using CallAladdin.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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

        public ICommand PlayRecordedCmd { get; set; }
        public ICommand StopCmd { get; set; }

        public JobViewCommonUserControlViewModel(object owner)
        {
            parentViewModel = (BaseViewModel)owner;

            if (parentViewModel is JobViewViewModel)
            {
                var jobViewViewModel = (JobViewViewModel)parentViewModel;
                var selectedJob = jobViewViewModel.GetSelectedJob();

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

                    if (!string.IsNullOrEmpty(selectedJob.VoiceNotePath))
                    {
                        AllowPlaying = true;
                    }

                    AllowStopping = false;
                }
            }
        }
    }
}
