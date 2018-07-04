﻿using System.Collections.Generic;
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
        Task<IList<Job>> GetJobs(string requestorUUID, string status = "");
        Task<bool> DeleteJob(string jobUUID);
    }
}