using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegisteredAddress { get; set; }
        public float OverallRating { get; set; }
        public int TotalReviewers { get; set; }
        public DateTime LastReviewedDate { get; set; }
        public bool IsContractor { get; set; }
    }
}
