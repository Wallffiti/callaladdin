using System;
using System.Collections.Generic;
using System.Text;
using CallAladdin.Model;
using CallAladdin.Services.Interfaces;
using System.Linq;
using System.Windows.Input;
using CallAladdin.Services;

namespace CallAladdin.ViewModel
{
    public class HistoryUserControlViewModel : BaseViewModel
    {
        private IJobService jobService;
        private bool isBusy;
        private const string DESCRIPTION_SUMMARY_MESSAGE = "You have a total of {0} jobs in the past";
        private const string DESCRIPTION_CONTRACTOR_FOUND_MESSAGE = "{0} jobs with contractors found";
        private const string DESCRIPTION_EXPIRED_MESSAGGE = "{0} expired jobs";
        private const string DESCRIPTION_JOB_ACCEPTED = "{0} accepted jobs";
        private IList<Job> jobRequestHistoryList;
        private string summaryLabel;
        private string contractorLabel;
        private string expiredLabel;
        private string acceptedLabel;

        private UserProfile userProfile;
        public UserProfile UserProfile
        {
            get
            {
                return userProfile;
            }
            set
            {
                userProfile = value;
                OnPropertyChanged("UserProfile");
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged("IsBusy"); }
        }

        public IList<Job> JobRequestHistoryList
        {
            get { return jobRequestHistoryList; }
            set { jobRequestHistoryList = value; OnPropertyChanged("JobRequestHistoryList"); }
        }

        public string SummaryLabel
        {
            get { return summaryLabel; }
            set { summaryLabel = value; OnPropertyChanged("SummaryLabel"); }
        }

        public string ContractorFoundLabel
        {
            get { return contractorLabel; }
            set { contractorLabel = value; OnPropertyChanged("ContractorFoundLabel"); }
        }

        public string ExpiredLabel
        {
            get { return expiredLabel; }
            set { expiredLabel = value; OnPropertyChanged("ExpiredLabel"); }
        }

        public string AcceptedLabel
        {
            get { return acceptedLabel; }
            set { acceptedLabel = value; OnPropertyChanged("AcceptedLabel"); }
        }

        public ICommand RefreshJobList { get; set; }
        public ICommand EditJobRequestHistoryCmd { get; set; }
        public ICommand DeleteJobRequestHistoryCmd { get; set; }
        public ICommand SortJobByDate { get; set; }
        public ICommand GoToJobView { get; set; }

        private Job selectedJob;

        public Job GetSelectedJob()
        {
            return selectedJob;
        }

        public HistoryUserControlViewModel(object owner)
        {
            var parentViewModel = (HomeViewModel)owner;
            if (parentViewModel != null)
            {
                this.UserProfile = parentViewModel.UserProfile;
            }
            UpdateDescriptionLabel();
            jobService = new JobService();
            RefreshJobList = new Xamarin.Forms.Command(async (e) =>
            {
                await RefreshListAsync();
            }, (param) =>
            {
                if (IsBusy)
                    return false;

                return true;
            });
            EditJobRequestHistoryCmd = new Xamarin.Forms.Command(e =>
            {
                //TODO
            },
           param =>
           {
               return true;
           });
            DeleteJobRequestHistoryCmd = new Xamarin.Forms.Command(e =>
            {
                //TODO
            },
            param =>
            {
                return true;
            });
            SortJobByDate = new Xamarin.Forms.Command(e =>
            {

            },
            param =>
            {
                return true;
            });
            GoToJobView = new Xamarin.Forms.Command(async e =>
            {
                this.selectedJob = (Job)e;
                await Navigator.Instance.NavigateTo(PageType.HISTORY_JOB_VIEW, this);
            },
            param =>
            {
                return true;
            });
        }

        private void UpdateDescriptionLabel(int contractorFound = 0, int expired = 0, int jobAccepted = 0)
        {
            var summaryTxt = string.Format(DESCRIPTION_SUMMARY_MESSAGE, contractorFound + expired + jobAccepted);
            var contractorFoundTxt = string.Format(DESCRIPTION_CONTRACTOR_FOUND_MESSAGE, contractorFound);
            var pendingTxt = string.Format(DESCRIPTION_EXPIRED_MESSAGGE, expired);
            var jobAcceptedTxt = string.Format(DESCRIPTION_JOB_ACCEPTED, jobAccepted);

            //SummaryLabel = string.Concat(summaryTxt, " with ", contractorFoundTxt, ", ", pendingTxt, ", and ", jobAcceptedTxt);
            SummaryLabel = summaryTxt;
            ContractorFoundLabel = contractorFoundTxt;
            ExpiredLabel = pendingTxt;
            AcceptedLabel = jobAcceptedTxt;
        }

        public async System.Threading.Tasks.Task RefreshListAsync()
        {
            IsBusy = true;
            var requestedJobs = await jobService.GetJobs(UserProfile?.SystemUUID);   //TODO: need backend to provide better filtering
            var fullList = new List<Job>();
            int contractorFoundCount = 0;
            int expiredJobsCount = 0;
            int acceptedJobsCount = 0;

            if (requestedJobs != null)
            {
                var filteredJobs = requestedJobs
                    .Where(p => p.Status != Constants.DELETED)
                    .ToList();

                var contractorFoundJobs = filteredJobs
                    .Where(p => !string.IsNullOrEmpty(p.ContractorSystemUUID))
                    .ToList();
                var expiredJobs = filteredJobs
                    .Where(p => (DateTime.Now.Subtract(p.CreatedDateTime).Days >= Constants.JOB_REQUEST_EXPIRY_DURATION_IN_DAYS) && p.Status.ToLower() == Constants.PENDING)
                    .ToList();

                if (contractorFoundJobs != null)
                {
                    contractorFoundCount = contractorFoundJobs.Count;
                    fullList.AddRange(contractorFoundJobs);
                }

                if (expiredJobs != null)
                {
                    expiredJobsCount = expiredJobs.Count;
                    fullList.AddRange(expiredJobs);
                }
            }

            var acceptedJobs = await jobService.GetAcceptedJobs(UserProfile?.SystemUUID);

            if (acceptedJobs != null)
            {
                acceptedJobsCount = acceptedJobs.Count;
                fullList.AddRange(acceptedJobs);
            }

            JobRequestHistoryList = fullList;
            UpdateDescriptionLabel(contractorFoundCount, expiredJobsCount, acceptedJobsCount);

            IsBusy = false;
        }
    }
}
