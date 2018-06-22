using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model
{
    public class JobRequestParameters
    {
        //public string UserSystemUUID { get; set; }
        public UserProfile UserProfile { get; set; }
        public string JobCategoryType { get; set; }
    }
}
