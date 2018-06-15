using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class DummyUserControlViewModel : BaseViewModel
    {
        private string dummyText;

        public string DummyText
        {
            get { return dummyText; ; }
            set { dummyText = value; OnPropertyChanged("DummyText"); }
        }

    }
}
