using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;

namespace CallAladdin.ViewModel
{
    public class JobAcceptanceViewViewModel : BaseViewModel
    {
        private BaseViewModel parentViewModel;
        private JobViewCommonUserControlViewModel jobviewCommonUserControlViewModel;
        private IJobService jobService;
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

        public Job Job
        {
            get
            {
                return selectedJob;
            }
            set
            {
                selectedJob = value;
                OnPropertyChanged(nameof(Job));
            }
        }

        public ICommand AcceptJobCmd { get; set; }
        public UserProfile UserProfile { get; set; }

        public JobAcceptanceViewViewModel(object owner)
        {
            parentViewModel = (BaseViewModel)owner;
            
            if (parentViewModel is ContractorUserControlViewModel)
            {
                var contractorUserControlViewModel = (ContractorUserControlViewModel)parentViewModel;
                this.Job = contractorUserControlViewModel.GetSelectedJob();
                UserProfile = contractorUserControlViewModel.UserProfile;
                SubscribeMeToThis(contractorUserControlViewModel);
            }

            jobviewCommonUserControlViewModel = new JobViewCommonUserControlViewModel(this);
            jobService = new JobService();

            AcceptJobCmd = new Xamarin.Forms.Command(e =>
            {
                //TODO
            },
            param =>
            {
                return true;
            });
        }
    }
}
