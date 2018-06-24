using CallAladdin.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Observers.Interfaces
{
    public interface IPublisher
    {
        void SubscribeMeToThis(ISubscriber subscriber);
        void UnSubscribeMeFromThis(ISubscriber subscriber);
        void NotifyCompletion(object sender, ObserverEventArgs eventArgs);
        void NotifyError(object sender, ObserverErrorEventArgs eventArgs);
    }
}
