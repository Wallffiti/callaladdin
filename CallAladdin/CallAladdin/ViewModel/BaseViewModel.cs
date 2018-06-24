using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using CallAladdin.Observers;

namespace CallAladdin.ViewModel
{
    public class BaseViewModel : BasePublisher, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
