using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Responses
{
    public class UserLoginResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string IdToken { get; set; }
        public string LocalId { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
