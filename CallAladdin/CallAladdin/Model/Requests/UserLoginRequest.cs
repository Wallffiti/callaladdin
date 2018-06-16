using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Requests
{
    public class UserLoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }
}
