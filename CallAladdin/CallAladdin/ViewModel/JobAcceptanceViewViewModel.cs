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
            }

            jobviewCommonUserControlViewModel = new JobViewCommonUserControlViewModel(this);
            jobService = new JobService();

            AcceptJobCmd = new Xamarin.Forms.Command(async e =>
            {
                if (selectedJob != null)
                {
                    var result = await jobService.AcceptJob(new Model.Requests.AcceptJobRequestRequest
                    {
                        ContractorUUID = UserProfile?.SystemUUID,
                        JobSystemUUID = selectedJob.SystemUUID
                    });

                    if (result == true)
                    {
                        Navigator.Instance.OkAlert("Job Accepted", "You have accepted the job, please call the requestor for more details.", "OK");
                        await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                        if (parentViewModel is ContractorUserControlViewModel)
                        {
                            var contractorUserControlViewModel = (ContractorUserControlViewModel)parentViewModel;
                            await contractorUserControlViewModel.RefreshListAsync();
                        }
                    }
                    else
                    {
                        Navigator.Instance.OkAlert("Error", "There is a problem with the server. Please try again later.", "OK");
                    }
                }
            },
            param =>
            {
                return true;
            });
        }
    }
}
