using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;

namespace CallAladdin.ViewModel
{
    public class DashboardUserControlViewModel : BaseViewModel
    {
        private IList<Job> jobRequestList;
        private IJobService jobService;
        private bool isBusy;
        private string descriptionLabel;
        private const string DESCRIPTION_MESSAGE = "You have recently requested for {0} new jobs";
        public ICommand EditJobRequestCmd { get; set; }
        public ICommand DeleteJobRequestCmd { get; set; }

        public IList<Job> JobRequestList
        {
            get { return jobRequestList; }
            set { jobRequestList = value; OnPropertyChanged("JobRequestList"); }
        }

        public UserProfile UserProfile { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged("IsBusy"); }
        }

        public string DescriptionLabel
        {
            get { return descriptionLabel; }
            set { descriptionLabel = value; OnPropertyChanged("DescriptionLabel"); }
        }

        public ICommand RefreshJobList { get; set; }

        public DashboardUserControlViewModel(object owner)
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
            EditJobRequestCmd = new Xamarin.Forms.Command(e =>
            {
                //TODO
            },
            param =>
            {
                return true;
            });
            DeleteJobRequestCmd = new Xamarin.Forms.Command(e =>
            {
                //TODO
            },
            param =>
            {
                return true;
            });
        }

        private void UpdateDescriptionLabel()
        {
            DescriptionLabel = string.Format(DESCRIPTION_MESSAGE, JobRequestList == null ? 0 : JobRequestList.Count);
        }

        public async System.Threading.Tasks.Task RefreshListAsync()
        {
            IsBusy = true;
            var data = await jobService.GetJobs(UserProfile?.SystemUUID, "pending");
            if (data != null)
            {
                JobRequestList = data
                    .Where(p => (DateTime.Now.Subtract(p.CreatedDateTime).Days < Constants.JOB_REQUEST_EXPIRY_DURATION_IN_DAYS) /*&& p.Status.ToLower() == "pending"*/)
                    .ToList();
            }
            UpdateDescriptionLabel();
            IsBusy = false;
        }
    }
}
