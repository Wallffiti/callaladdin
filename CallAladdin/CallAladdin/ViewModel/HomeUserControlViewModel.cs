using CallAladdin.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class HomeUserControlViewModel : BaseViewModel
    {
        public ICommand SelectOptionCmd { get; set; }

        public HomeUserControlViewModel()
        {
            SelectOptionCmd = new SelectContractorOptionCommand(this);
        }
    }
}
