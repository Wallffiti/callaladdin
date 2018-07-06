using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Requests
{
    public class EditJobRequestRequest
    {
        public string SystemUUID { get; set; }
        //public string RequestorSystemUUID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string ScopeOfWork { get; set; }
        public string VoiceNotePath { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float Longitude { get; set; } = 101.97576600000002f;
        public float Latitude { get; set; } = 4.210484f;
        public bool ContractorSystemUUID { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public string Comment { get; set; }
    }
}
