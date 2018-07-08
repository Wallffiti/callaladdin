using CallAladdin.Commands;
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
    public class HomeUserControlViewModel : BaseViewModel, ISubscriber
    {
        public string CurrentSelectedCategory { get; set; }
        private HomeViewModel parentViewModel;

        public UserProfile UserProfile { get; set; }
        private IJobService jobService;
        private bool isBusy;
        public ICommand SelectOptionCmd { get; set; }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public HomeUserControlViewModel(HomeViewModel homeViewModel)
        {
            this.parentViewModel = homeViewModel;
            this.UserProfile = homeViewModel.UserProfile; //userProfile;
            jobService = new JobService();
            SelectOptionCmd = new SelectContractorOptionCommand(this);
        }

        public void NavigateToJobRequest(string category)
        {
            Navigator.Instance.ConfirmationAlert("Confirmation", "Request a job under the category of " + category + "?", "Yes", "No", async () =>
            {
                //For android
                await GoToJobRequestAsync(category);
            }, async () =>
            {
                //For ios
                await GoToJobRequestAsync(category);
            });
        }

        private async Task GoToJobRequestAsync(string category)
        {
            if (IsBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            IsBusy = true;

            var isEligible = await IsEligibleForRequest();

            if (isEligible == null)
            {
                Navigator.Instance.OkAlert("Error", "There is a problem contacting the server to request for jobs. Please try again later.", "OK");
                IsBusy = false;
                return;
            }

            if (isEligible == true)
            {
                CurrentSelectedCategory = category;
                await Navigator.Instance.NavigateTo(PageType.JOB_REQUEST, this);
            }
            else
            {
                Navigator.Instance.OkAlert("LIMIT REACHED", "Sorry you can't request anymore job in this category as you have reached this monthly limit (" + Constants.ALLOWABLE_JOB_REQUESTS_PER_MONTH + " per month). If you still want to raise, please call " + Constants.ADMIN_NUMBER, "OK");
            }

            IsBusy = false;
        }

        public async Task<bool?> IsEligibleForRequest()
        {
            bool? result = null;

            var jobs = await jobService.GetRequestedJobs(/*userSystemUUID*/ UserProfile?.SystemUUID);

            if (jobs != null)
            {
                var now = DateTime.Now;
                var monthStart = new DateTime(now.Year, now.Month, 1);
                int daysPerMonth = 30;

                if (now.Month == 2)
                {
                    if (now.Year % 4 == 0)
                    {
                        daysPerMonth = 29;
                    }
                    else
                    {
                        daysPerMonth = 28;
                    }
                }
                else if (now.Month == 1 || now.Month == 3 || now.Month == 5 || now.Month == 7 || now.Month == 8 || now.Month == 10 || now.Month == 12)
                {
                    daysPerMonth = 31;
                }

                var monthEnd = new DateTime(now.Year, now.Month, daysPerMonth);
                var requestsThisMonth = jobs.Where(p => p.CreatedDateTime > monthStart && p.CreatedDateTime < monthEnd).Count();
                result = requestsThisMonth <= Constants.ALLOWABLE_JOB_REQUESTS_PER_MONTH;
            }

            return result;
        }

        public void OnUpdatedHandler(object sender, ObserverEventArgs eventArgs)
        {
            base.NotifyCompletion(this, eventArgs);
        }

        public void OnErrorHandler(object sender, ObserverErrorEventArgs eventArgs)
        {
           //if needed
        }
    }
}
