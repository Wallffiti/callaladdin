using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.EventArgs
{
    public class ObserverErrorEventArgs : System.EventArgs
    {
        public string ErrorName { get; set; }
        public string ErrorType { get; set; }
        public Exception ExceptionData { get; set; }

        public ObserverErrorEventArgs(string errorName, string errorType = "", Exception ex = null)
        {
            this.ErrorName = errorName;
            this.ErrorType = errorType;
            this.ExceptionData = ex;
        }
    }
}
