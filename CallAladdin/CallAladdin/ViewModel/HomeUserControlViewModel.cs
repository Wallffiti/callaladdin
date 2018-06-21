using CallAladdin.Commands;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class HomeUserControlViewModel : BaseViewModel
    {
        //private UserProfile userProfile;
        private string userSystemUUID;
        public ICommand SelectOptionCmd { get; set; }

        public HomeUserControlViewModel(UserProfile userProfile)
        {
            //this.userProfile = userProfile;
            this.userSystemUUID = userProfile?.SystemUUID;
            SelectOptionCmd = new SelectContractorOptionCommand(this);
        }

        public async System.Threading.Tasks.Task NavigateToJobRequestAsync()
        {
            await Navigator.Instance.NavigateTo(PageType.JOB_REQUEST);
        }
    }
}
