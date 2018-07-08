using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Requests
{
    public class AcceptJobRequestRequest
    {
        public string JobSystemUUID { get; set; }
        public string ContractorUUID { get; set; }
    }
}
