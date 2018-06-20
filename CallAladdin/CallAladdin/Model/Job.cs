using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model
{
    public class Job
    {
        public string SystemUUID { get; set; }
        public string RequestorSystemUUID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string ScopeOfWork { get; set; }
        public string VoiceNotePath { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public bool ContractorSystemUUID { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public float Rating { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
