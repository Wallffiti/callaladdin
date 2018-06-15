using CallAladdin.Commands;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private UserProfile userProfile;
        public DummyCommand dummyCmd { get; set; }
        public Dummy2Command dummy2Cmd { get; set; }

        public HomeViewModel(UserProfile userProfile)
        {
            this.userProfile = userProfile;
            dummyCmd = new DummyCommand(this);
            dummy2Cmd = new Dummy2Command(this);
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
