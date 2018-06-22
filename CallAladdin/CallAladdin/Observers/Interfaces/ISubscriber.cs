using CallAladdin.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Observers.Interfaces
{
    public interface ISubscriber
    {
        void OnUpdatedHandler(object sender, ObserverEventArgs eventArgs);
        void OnErrorHandler(object sender, ObserverErrorEventArgs eventArgs);
    }
}
