using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Requests
{
    public class ForgottenPasswordRequest
    {
        public string requestType { get; set; }
        public string email { get; set; }
    }
}
