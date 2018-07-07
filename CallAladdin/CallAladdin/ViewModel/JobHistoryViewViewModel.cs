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
    public class JobHistoryViewViewModel : BaseViewModel
    {
        private IUserService userService;
        private UserProfile profile;
        private JobViewCommonUserControlViewModel jobviewCommonUserControlViewModel;
        private BaseViewModel parentViewModel;
        private string profileAvatarImage;
        private string profileName;
        private DateTime createdDate;
        private bool hasProfile;
        private Job selectedJob;
        private bool isBusy;

        public UserProfile GetProfile()
        {
            return profile;
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

        public string ProfileAvatarImage
        {
            get
            {
                return profileAvatarImage;
            }
            set
            {
                profileAvatarImage = value;
                OnPropertyChanged(nameof(ProfileAvatarImage));
            }
        }

        public string ProfileName
        {
            get
            {
                return profileName;
            }
            set
            {
                profileName = value;
                OnPropertyChanged(nameof(ProfileName));
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
                OnPropertyChanged(nameof(CreatedDate));
            }
        }

        public bool HasProfile
        {
            get
            {
                return hasProfile;
            }
            set
            {
                hasProfile = value;
                OnPropertyChanged(nameof(HasProfile));
            }
        }

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

        public string ProfileType { get; set; }

        public ICommand GoToProfilePageCmd { get; set; }

        public JobHistoryViewViewModel(object owner)
        {
            parentViewModel = (BaseViewModel)owner;

            if (parentViewModel is HistoryUserControlViewModel)
            {
                var historyViewModel = (HistoryUserControlViewModel)parentViewModel;
                this.Job = historyViewModel.GetSelectedJob();

                GoToProfilePageCmd = new Xamarin.Forms.Command(async e =>
                {
                    await Navigator.Instance.NavigateTo(PageType.PROFILE_VIEW, this);
                },
                param =>
                {
                    if (param == null)
                        return false;

                    return !(bool)param;
                });

                string userSystemUUID = "";
                userService = new UserService();
                var user = historyViewModel?.UserProfile;

                if (user != null && this.Job != null)
                {
                    //if the current user is a contractor, and this job is the job that this contractor accepted, the profile displayed would be the job requestor's
                    if (user.IsContractor && this.Job.ContractorSystemUUID == user.SystemUUID)
                    {
                        userSystemUUID = this.Job.RequestorSystemUUID;
                        HasProfile = true;
                        ProfileType = Constants.REQUESTOR;
                    }
                    else if (this.Job.RequestorSystemUUID == user.SystemUUID && !string.IsNullOrEmpty(this.Job.ContractorSystemUUID))
                    {
                        //if the job has contractor user uuid assigned (that means the job is accepted by a contractor), then the profile displayed would be contractor's
                        userSystemUUID = this.Job.ContractorSystemUUID;
                        HasProfile = true;
                        ProfileType = Constants.CONTRACTOR;
                    }
                }

                if (!string.IsNullOrEmpty(userSystemUUID))
                    GetProfileData(userSystemUUID);
            }

            jobviewCommonUserControlViewModel = new JobViewCommonUserControlViewModel(this);
        }

        private void GetProfileData(string userSystemUUID)
        {
            Task.Run(async () =>
            {
                //jobviewCommonUserControlViewModel.IsBusy = true;
                IsBusy = true;
                profile = await userService.GetUserProfileByUUID(userSystemUUID);
                if (profile != null)
                {
                    this.ProfileAvatarImage = profile.PathToProfileImage;
                    this.ProfileName = profile.Name;
                    this.CreatedDate = profile.CreatedDate;
                }
                IsBusy = false;
                //jobviewCommonUserControlViewModel.IsBusy = false;
            });
        }
    }
}
