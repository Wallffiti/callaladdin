using CallAladdin.Commands;
using CallAladdin.EventArgs;
using CallAladdin.Model;
using CallAladdin.Observers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CallAladdin.ViewModel
{
    public class HomeViewModel : BaseViewModel, ISubscriber
    {
        public DummyCommand dummyCmd { get; set; }
        public Dummy2Command dummy2Cmd { get; set; }

        public UserProfile UserProfile { get; set; }
        private HomeUserControlViewModel homeUserControlViewModel;
        private UserProfileUserControlViewModel userProfileUserControlViewModel;
        private DashboardUserControlViewModel dashboardUserControlViewModel;

        public HomeUserControlViewModel HomeUserControlViewModel
        {
            get { return homeUserControlViewModel; }
            set { homeUserControlViewModel = value; OnPropertyChanged("HomeUserControlViewModel"); }
        }

        public UserProfileUserControlViewModel UserProfileUserControlViewModel
        {
            get { return userProfileUserControlViewModel; }
            set { userProfileUserControlViewModel = value; OnPropertyChanged("UserProfileUserControlViewModel"); }
        }

        public DashboardUserControlViewModel DashboardUserControlViewModel
        {
            get { return dashboardUserControlViewModel; }
            set { dashboardUserControlViewModel = value; OnPropertyChanged("DashboardUserControlViewModel"); }
        }


        public HomeViewModel(/*UserProfile userProfile*/ object owner)
        {
            dummyCmd = new DummyCommand(this);
            dummy2Cmd = new Dummy2Command(this);
            this.UserProfile = (UserProfile)owner;
            HomeUserControlViewModel = new HomeUserControlViewModel(this);
            UserProfileUserControlViewModel = new UserProfileUserControlViewModel(this);
            DashboardUserControlViewModel = new DashboardUserControlViewModel(this);

            homeUserControlViewModel.SubscribeMeToThis(this);
            userProfileUserControlViewModel.SubscribeMeToThis(this);
            dashboardUserControlViewModel.SubscribeMeToThis(this);
        }

        public async void NavigateToDummyPage()
        {
            await Navigator.Instance.NavigateTo(PageType.DUMMY, titleAlignment: TitleAlignment.LEFT);
        }

        public async void NavigateToDummyModal()
        {
            await Navigator.Instance.NavigateTo(PageType.DUMMY, uIPageType: UIPageType.MODAL);
        }

        public async System.Threading.Tasks.Task RefreshDashboardViewAsync()
        {
            await dashboardUserControlViewModel.RefreshListAsync();
        }

        public void OnUpdatedHandler(object sender, ObserverEventArgs eventArgs)
        {
            if (eventArgs != null)
            {
                if (sender is UserProfileUserControlViewModel)
                {
                    if (eventArgs.EventName == Constants.USER_PROFILE_UPDATE)
                    {
                        //Notify related view models on profile updates
                        //Update user profile for related user controls here
                        this.UserProfile = eventArgs.Parameters as UserProfile;
                        HomeUserControlViewModel.UserProfile = this.UserProfile;
                        dashboardUserControlViewModel.UserProfile = this.UserProfile;
                    }
                }
            }

            base.NotifyCompletion(this, eventArgs);
        }

        public void OnErrorHandler(object sender, ObserverErrorEventArgs eventArgs)
        {
            //if needed
        }
    }
}
