using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Responses
{
    /// <summary>
    /// Response from backend upon job request success
    /// </summary>
    public class JobRequestResponse
    {
        public bool IsSuccess { get; set; }
        public string SystemGeneratedId { get; set; }
    }
}
