using CallAladdin.Helper;
using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class JobViewViewModel : BaseViewModel
    {
        private IJobService jobService;
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

        public ICommand EditCmd { get; set; }
        public ICommand DeleteCmd { get; set; }

        public Job GetSelectedJob()
        {
            return selectedJob;
        }

        private bool isBusy;
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

        public JobViewViewModel(object owner)
        {
            parentViewModel = (BaseViewModel)owner;

            if (parentViewModel is DashboardUserControlViewModel)
            {
                var dashboardViewModel = (DashboardUserControlViewModel)parentViewModel;
                this.selectedJob = dashboardViewModel.GetSelectedJob();
            }

            jobviewCommonUserControlViewModel = new JobViewCommonUserControlViewModel(this);
            jobService = new JobService();

            EditCmd = new Xamarin.Forms.Command(e =>
            {
                //TODO
            },
            param =>
            {
                return true;
            });
            DeleteCmd = new Xamarin.Forms.Command(e =>
            {
                Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure you want to delete this job (cannot be undone)?", "Yes", "No", async () =>
                {
                    //For android
                    await DoDeleteJob(selectedJob);
                }, async () =>
                {
                    //For ios
                    await DoDeleteJob(selectedJob);
                });
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
                if (parentViewModel is DashboardUserControlViewModel)
                {
                    var dashboardViewModel = (DashboardUserControlViewModel)parentViewModel;
                    //dashboardViewModel.IsBusy = true;
                    IsBusy = true;

                    var result = await jobService.DeleteJob(job.SystemUUID);
                    //dashboardViewModel.IsBusy = false;
                    IsBusy = false;

                    if (result)
                    {
                        await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                        Navigator.Instance.OkAlert("Alert", "Your job has been deleted", "OK");
                        await dashboardViewModel.RefreshListAsync();
                    }
                    else
                    {
                        Navigator.Instance.OkAlert("Error", "There is a problem with the server. Please try again later.", "OK");
                    }
                }
            }
        }
    }
}
