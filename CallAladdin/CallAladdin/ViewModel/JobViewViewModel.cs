using CallAladdin.Helper;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class JobViewViewModel : BaseViewModel
    {
        private JobViewCommonUserControlViewModel jobviewCommonUserControlViewModel;
        private BaseViewModel parentViewModel;
        private Job selectedJob;

        public JobViewCommonUserControlViewModel JobViewCommonUserControlViewModel
        {
            get
            {
                return jobviewCommonUserControlViewModel;
            }
            set
            {
                jobviewCommonUserControlViewModel = value;
                OnPropertyChanged(nameof(JobViewCommonUserControlViewModel));
            }
        }

        public Job GetSelectedJob()
        {
            return selectedJob;
        }

        public JobViewViewModel(object owner)
        {
            parentViewModel = (BaseViewModel)owner;

            if (parentViewModel is DashboardUserControlViewModel)
            {
                var dashboardViewModel = (DashboardUserControlViewModel)parentViewModel;
                this.selectedJob = dashboardViewModel.GetSelectedJob();
            }

            jobviewCommonUserControlViewModel = new JobViewCommonUserControlViewModel(this);
        }
    }
}
