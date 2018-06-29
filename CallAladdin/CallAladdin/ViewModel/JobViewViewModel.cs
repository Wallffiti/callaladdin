using CallAladdin.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class JobViewViewModel : BaseViewModel
    {
        private BaseViewModel parentViewModel;
        private string contractorIcon;
        private string jobRequestType;
        private string jobRequestImage;

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


        public JobViewViewModel(object owner)
        {
            parentViewModel = (BaseViewModel)owner;

            if (parentViewModel is DashboardUserControlViewModel)
            {
                var dashboardViewModel = (DashboardUserControlViewModel)parentViewModel;
                var selectedJob = dashboardViewModel.GetSelectedJob();
                if (selectedJob != null)
                {
                    this.JobRequestImage = selectedJob.ImagePath;
                    this.JobRequestType = selectedJob.Category;
                    this.ContractorIcon = Utilities.GetIconByCategory(selectedJob.Category);
                }
            }
        }
    }
}
