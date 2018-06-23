using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CallAladdin.ViewModel
{
    public class DashboardUserControlViewModel : BaseViewModel
    {
        private IList<Job> jobRequestList;
        private IJobService jobService;
        private bool isBusy;

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

        public DashboardUserControlViewModel(object owner)
        {
            var parentViewModel = (HomeViewModel)owner;
            if (parentViewModel != null)
            {
                this.UserProfile = parentViewModel.UserProfile;
            }
            jobService = new JobService();

            //TODO: run this only when dashboard tab is selected
            //Task.Run(async () =>
            //{
            //    await RefreshListAsync();
            //});
        }

        public async System.Threading.Tasks.Task RefreshListAsync()
        {
            IsBusy = true;
            JobRequestList = await jobService.GetJobs(UserProfile?.SystemUUID);
            IsBusy = false;
        }
    }
}
