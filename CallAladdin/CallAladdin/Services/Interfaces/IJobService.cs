using System.Collections.Generic;
using System.Threading.Tasks;
using CallAladdin.Model;
using CallAladdin.Model.Requests;
using CallAladdin.Model.Responses;

namespace CallAladdin.Services.Interfaces
{
    public interface IJobService
    {
        Task<JobRequestResponse> CreateRequest(JobRequestRequest jobRequest);
        Task<EditJobRequestResponse> UpdateRequest(EditJobRequestRequest updatedJobRequest);
        Task<IList<Job>> GetAcceptedJobs(string contractorUUID);
        Task<IList<Job>> GetRequestedJobs(string requestorUUID, string status = "");
        Task<IList<Job>> GetAvailableJobs(string requestorUUID, string city, string workCategory = "");
        Task<bool> DeleteJob(string jobUUID);
        Task<bool> AcceptJob(AcceptJobRequestRequest acceptJobRequestRequest);
    }
}