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

        public UserProfile UserProfile { get; set; }

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
            var data = await jobService.GetJobs(UserProfile?.SystemUUID);   //TODO: need backend to provide better filtering
            if (data != null)
            {
                var contractorFoundJobs = data
                    .Where(p => !string.IsNullOrEmpty(p.ContractorSystemUUID))
                    .ToList();
                var expiredJobs = data
                    .Where(p => (DateTime.Now.Subtract(p.CreatedDateTime).Days >= Constants.JOB_REQUEST_EXPIRY_DURATION_IN_DAYS) && p.Status.ToLower() == "pending")
                    .ToList();
                var jobsAccepted = new List<Job>();

                JobRequestHistoryList = contractorFoundJobs.Concat(expiredJobs).Concat(jobsAccepted).ToList();
                UpdateDescriptionLabel(contractorFoundJobs == null ? 0 : contractorFoundJobs.Count(),
                    expiredJobs == null ? 0 : expiredJobs.Count(),
                    jobsAccepted == null ? 0 : jobsAccepted.Count());
            }
            IsBusy = false;
        }
    }
}
