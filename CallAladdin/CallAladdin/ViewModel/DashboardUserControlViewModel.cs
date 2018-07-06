using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using CallAladdin.Observers.Interfaces;
using CallAladdin.EventArgs;

namespace CallAladdin.ViewModel
{
    public class DashboardUserControlViewModel : BaseViewModel, ISubscriber
    {
        private IList<Job> jobRequestList;
        private IJobService jobService;
        private bool isBusy;
        private string descriptionLabel;
        private Job selectedJob;
        private const string DESCRIPTION_MESSAGE = "You have recently requested for {0} new jobs";
        public ICommand EditJobRequestCmd { get; set; }
        public ICommand DeleteJobRequestCmd { get; set; }
        public ICommand GoToJobView { get; set; }

        public Job GetSelectedJob()
        {
            return selectedJob;
        }

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
                SubscribeMeToThis(parentViewModel);
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
            EditJobRequestCmd = new Xamarin.Forms.Command(async e =>
            {
                this.selectedJob = (Job)e;
                await Navigator.Instance.NavigateTo(PageType.EDIT_JOB_VIEW, this);
            },
            param =>
            {
                return true;
            });
            DeleteJobRequestCmd = new Xamarin.Forms.Command(async e =>
            {
                var job = (Job)e;

                Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure you want to delete this job (cannot be undone)?", "Yes", "No", async () =>
                {
                    //For android
                    await DoDeleteJob(job);
                    await RefreshListAsync();
                }, async () =>
                {
                    //For ios
                    await DoDeleteJob(job);
                    await RefreshListAsync();
                });
            },
            param =>
            {
                return true;
            });
            GoToJobView = new Xamarin.Forms.Command(async e =>
            {
                this.selectedJob = (Job)e;
                await Navigator.Instance.NavigateTo(PageType.JOB_VIEW, this);
            },
            param =>
            {
                return true;
            });
        }

        private async Task DoDeleteJob(Job job)
        {
            if (job != null)
            {
                IsBusy = true;
                var result = await jobService.DeleteJob(job.SystemUUID);

                if (result)
                {
                    Navigator.Instance.OkAlert("Alert", "Your job has been deleted", "OK");
                }

                IsBusy = false;
            }
        }

        private void UpdateDescriptionLabel()
        {
            DescriptionLabel = string.Format(DESCRIPTION_MESSAGE, JobRequestList == null ? 0 : JobRequestList.Count);
        }

        public async System.Threading.Tasks.Task RefreshListAsync()
        {
            IsBusy = true;
            var data = await jobService.GetJobs(UserProfile?.SystemUUID, Constants.PENDING);
            if (data != null)
            {
                JobRequestList = data
                    .Where(p => (DateTime.Now.Subtract(p.CreatedDateTime).Days < Constants.JOB_REQUEST_EXPIRY_DURATION_IN_DAYS) /*&& p.Status.ToLower() == "pending"*/)
                    .ToList();
            }
            UpdateDescriptionLabel();
            IsBusy = false;
        }

        public void OnUpdatedHandler(object sender, ObserverEventArgs eventArgs)
        {
            if (sender is JobViewViewModel || sender is EditJobViewModel)
            {
                if (eventArgs?.EventName == Constants.JOB_REQUEST_LIST_UPDATE)
                {
                    RefreshJobList?.Execute(eventArgs?.Parameters);   
                }
            }
        }

        public void OnErrorHandler(object sender, ObserverErrorEventArgs eventArgs)
        {
            //if needed
        }
    }
}
