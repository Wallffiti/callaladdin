using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Requests
{
    /// <summary>
    /// The body for signup with Firebase
    /// </summary>
    public class UserSignupRequest
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }
}
