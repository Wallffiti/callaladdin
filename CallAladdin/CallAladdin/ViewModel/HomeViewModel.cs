using CallAladdin.Commands;
using CallAladdin.EventArgs;
using CallAladdin.Model;
using CallAladdin.Observers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class HomeViewModel : BaseViewModel, ISubscriber
    {
        public DummyCommand dummyCmd { get; set; }
        public Dummy2Command dummy2Cmd { get; set; }

        private HomeUserControlViewModel homeUserControlViewModel;
        private UserProfileUserControlViewModel userProfileUserControlViewModel;

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


        public HomeViewModel(UserProfile userProfile)
        {
            dummyCmd = new DummyCommand(this);
            dummy2Cmd = new Dummy2Command(this);
            HomeUserControlViewModel = new HomeUserControlViewModel(userProfile);
            UserProfileUserControlViewModel = new UserProfileUserControlViewModel(userProfile);

            homeUserControlViewModel.SubscribeMeToThis(this);
            userProfileUserControlViewModel.SubscribeMeToThis(this);
        }

        public async void NavigateToDummyPage()
        {
            await Navigator.Instance.NavigateTo(PageType.DUMMY, titleAlignment: TitleAlignment.LEFT);
        }

        public async void NavigateToDummyModal()
        {
            await Navigator.Instance.NavigateTo(PageType.DUMMY, uIPageType: UIPageType.MODAL);
        }

        public void OnUpdatedHandler(object sender, ObserverEventArgs eventArgs)
        {
            if (sender is UserProfileUserControlViewModel)
            {
                if (eventArgs != null && eventArgs.EventName == Constants.USER_PROFILE_UPDATE)
                {
                    //Notify related view models on profile updates
                    homeUserControlViewModel.UpdateUserProfile(eventArgs.Parameters as UserProfile);
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
