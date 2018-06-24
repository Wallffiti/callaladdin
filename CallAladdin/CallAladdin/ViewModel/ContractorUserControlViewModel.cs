using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System.Linq;

namespace CallAladdin.ViewModel
{
    public class ContractorUserControlViewModel : BaseViewModel
    {
        private IJobService jobService;
        private string descriptionLabel;
        private bool isBusy;
        public UserProfile UserProfile { get; set; }
        public ICommand RefreshJobList { get; set; }
        private IList<Job> availableJobsList;
        private const string DESCRIPTION_MESSAGE = "There are {0} available jobs near your location";

        public string DescriptionLabel
        {
            get { return descriptionLabel; }
            set { descriptionLabel = value; OnPropertyChanged("DescriptionLabel"); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged("IsBusy"); }
        }

        public IList<Job> AvailableJobsList
        {
            get { return availableJobsList; }
            set { availableJobsList = value; OnPropertyChanged("AvailableJobsList"); }
        }

        public ContractorUserControlViewModel(object owner)
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
        }

        public async Task RefreshListAsync()
        {
            IsBusy = true;
            //TODO: call API for available jobs for a particular location
            UpdateDescriptionLabel();
            IsBusy = false;
        }

        private void UpdateDescriptionLabel()
        {
            if (UserProfile != null && UserProfile.IsContractor)
            {
                //TODO: load available jobs here
                AvailableJobsList = null;

                DescriptionLabel = string.Format(DESCRIPTION_MESSAGE, AvailableJobsList == null ? 0 : AvailableJobsList.Count);
            }
            else
            {
                DescriptionLabel = "You can only view this page if you are registered as CONTRACTOR";
            }
        }
    }
}
