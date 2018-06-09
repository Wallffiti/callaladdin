﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model
{
    public class UserRegistration
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsRegisteredAsContractor { get; set; }
        public string Category { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
    }
}
