using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class DummyViewModel : BaseViewModel
    {
        private DummyUserControlViewModel childViewModel;
        public DummyUserControlViewModel ChildViewModel
        {
            get { return childViewModel; }
            set { childViewModel = value; OnPropertyChanged("ChildViewModel"); }
        }

        public DummyViewModel()
        {
            ChildViewModel = new DummyUserControlViewModel();
            ChildViewModel.DummyText = "Hello there";
        }
    }
}
