using CallAladdin.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CallAladdin.Model;

namespace CallAladdin.Services
{
    public class JobService
    {
        public async Task<bool> CreateRequest(JobRequest jobRequest)
        {
            //TODO
            return true;
        }

        public async Task<IList<Job>> GetJobs(string requestorUUID)
        {
            //TODO
            var results = new List<Job>();

            return results;
        }
    }
}
