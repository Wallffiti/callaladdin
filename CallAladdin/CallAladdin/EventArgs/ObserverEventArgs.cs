using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.EventArgs
{
    public class ObserverEventArgs : System.EventArgs
    {
        public string EventName { get; set; }
        public string EventType { get; set; }
        public object Parameters { get; set; }

        public ObserverEventArgs(string eventName, string eventType = "", object parameters = null)
        {
            this.EventName = eventName;
            this.EventType = eventType;
            this.Parameters = parameters;
        }
    }
}
