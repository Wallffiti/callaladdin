using CallAladdin.Commands;
using CallAladdin.EventArgs;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        //private UserProfile userProfile;
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
            //this.userProfile = userProfile;
            dummyCmd = new DummyCommand(this);
            dummy2Cmd = new Dummy2Command(this);
            HomeUserControlViewModel = new HomeUserControlViewModel(userProfile);
            UserProfileUserControlViewModel = new UserProfileUserControlViewModel(userProfile);
        }

        public async void NavigateToDummyPage()
        {
            await Navigator.Instance.NavigateTo(PageType.DUMMY, titleAlignment: TitleAlignment.LEFT);
        }

        public async void NavigateToDummyModal()
        {
            await Navigator.Instance.NavigateTo(PageType.DUMMY, uIPageType: UIPageType.MODAL);
        }
    }
}
