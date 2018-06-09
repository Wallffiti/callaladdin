using CallAladdin.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public DummyCommand dummyCmd { get; set; }

        public HomeViewModel()
        {
            dummyCmd = new DummyCommand(this);
        }

        public async void NavigateToDummyPage()
        {
            await Navigator.Instance.NavigateTo(PageType.DUMMY);
        }
    }
}
