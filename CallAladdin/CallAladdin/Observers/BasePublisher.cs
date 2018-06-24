using CallAladdin.EventArgs;
using CallAladdin.Observers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Observers
{
    public class BasePublisher : IPublisher
    {
        private List<ISubscriber> subscribers = new List<ISubscriber>();

        public void NotifyCompletion(object sender, ObserverEventArgs eventArgs)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.OnUpdatedHandler(sender, eventArgs);
            }
        }

        public void NotifyError(object sender, ObserverErrorEventArgs eventArgs)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.OnErrorHandler(sender, eventArgs);
            }
        }

        public void SubscribeMeToThis(ISubscriber subscriber)
        {
            if (!subscribers.Contains(subscriber))
            {
                subscribers.Add(subscriber);
            }
        }

        public void UnSubscribeMeFromThis(ISubscriber subscriber)
        {
            if (subscribers.Contains(subscriber))
            {
                subscribers.Remove(subscriber);
            }
        }
    }
}
