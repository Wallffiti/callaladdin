﻿using CallAladdin.Model.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallAladdin.Model;
using System.Net.Http;
using Newtonsoft.Json;
using CallAladdin.Model.Responses;
using RestSharp;
using System.IO;
using CallAladdin.Helper;
using CallAladdin.Services.Interfaces;
using System.Linq;

namespace CallAladdin.Services
{
    public class JobService : IJobService
    {
        public async Task<bool> AcceptJob(AcceptJobRequestRequest acceptJobRequestRequest)
        {
            bool result = false;
            IRestResponse response = null;

            if (acceptJobRequestRequest != null)
            {
                var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
                var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();

                if (!string.IsNullOrEmpty(acceptJobRequestRequest.ContractorUUID) && !string.IsNullOrEmpty(acceptJobRequestRequest.JobSystemUUID))
                {
                    var fullUrl = baseUrl + "/requests/" + acceptJobRequestRequest.JobSystemUUID + "/";

                    var client = new RestClient(fullUrl);
                    var request = new RestRequest(Method.PATCH);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "Basic " + apiKey);
                    request.AddParameter("contractor", acceptJobRequestRequest.ContractorUUID);
                    request.AddParameter("status", Constants.FOUND_CONTRACTOR);

                    try
                    {
                        response = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            if (response != null && response.IsSuccessful)
            {
                var strResponse = response?.Content;
                dynamic responseData = string.IsNullOrEmpty(strResponse) ? "" : JsonConvert.DeserializeObject(strResponse);

                if (responseData != null)
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task<bool> CompleteJob(string jobSystemUUID)
        {
            bool result = false;
            IRestResponse response = null;

            response = await CallApiSetJobStatus(jobSystemUUID, response, Constants.COMPLETED);

            return result;
        }

        private static async Task<IRestResponse> CallApiSetJobStatus(string jobSystemUUID, IRestResponse response, string status)
        {
            if (!string.IsNullOrEmpty(jobSystemUUID))
            {
                var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
                var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();

                var fullUrl = baseUrl + "/requests/" + jobSystemUUID + "/";
                var client = new RestClient(fullUrl);
                var request = new RestRequest(Method.PATCH);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic " + apiKey);
                request.AddParameter("status", status);

                try
                {
                    response = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                }
                catch (Exception ex)
                {

                }
            }

            return response;
        }

        public async Task<JobRequestResponse> CreateRequest(JobRequestRequest jobRequest)
        {
            JobRequestResponse result = null;
            IRestResponse response = null;

            if (jobRequest != null)
            {
                result = new JobRequestResponse();
                var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
                var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();

                if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
                {
                    var fullUrl = baseUrl + "/requests/";
                    var requestorUUID = jobRequest.RequestorSystemUUID;
                    var title = jobRequest.Title;
                    var scopeOfWork = jobRequest.ScopeOfWork;
                    var address = jobRequest.Address;
                    var city = jobRequest.City;
                    var country = jobRequest.Country;
                    var longitude = jobRequest.Longitude;
                    var latitude = jobRequest.Latitude;
                    var workCategory = jobRequest.Category;
                    var preferredStartTime = jobRequest.StartDateTime;
                    var preferredEndTime = jobRequest.EndDateTime;

                    var client = new RestClient(fullUrl);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "Basic " + apiKey);
                    request.AddParameter("requestor_uuid", requestorUUID);
                    request.AddParameter("title", title);
                    request.AddParameter("scope_of_work", scopeOfWork);
                    request.AddParameter("address", address);
                    request.AddParameter("city", city);
                    request.AddParameter("country", country);
                    request.AddParameter("longitude", longitude);
                    request.AddParameter("latitude", latitude);
                    request.AddParameter("work_category", workCategory);
                    request.AddParameter("preferred_start_datetime", preferredStartTime);
                    request.AddParameter("preferred_end_datetime", preferredEndTime);

                    try
                    {
                        if (File.Exists(jobRequest.ImagePath))
                        {
                            using (var fs = File.OpenRead(jobRequest.ImagePath))
                            {
                                var bytes = Utilities.StreamToBytes(fs);
                                request.AddFile("image", bytes, Guid.NewGuid().ToString() + ".jpg", "image/jpg");
                            }
                        }

                        if (File.Exists(jobRequest.VoiceNotePath))
                        {
                            using (var fs = File.OpenRead(jobRequest.VoiceNotePath))
                            {
                                var bytes = Utilities.StreamToBytes(fs);
                                request.AddFile("voice_note", bytes, Guid.NewGuid().ToString() + ".mp3", "audio/mpeg");
                            }
                        }

                        response = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }

            if (response != null && response.IsSuccessful)
            {
                var strResponse = response?.Content;
                dynamic responseData = string.IsNullOrEmpty(strResponse) ? "" : JsonConvert.DeserializeObject(strResponse);

                if (responseData != null)
                {
                    result.SystemGeneratedId = responseData.uuid;
                    result.IsSuccess = true;
                }
            }

            return result;
        }

        public async Task<bool> DeleteJob(string jobUUID)
        {
            if (string.IsNullOrEmpty(jobUUID))
                return false;

            var result = false;
            IRestResponse response = null;

            var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
            var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();

            if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
            {
                var fullUrl = baseUrl + "/requests/" + jobUUID + "/";
                var client = new RestClient(fullUrl);
                var request = new RestRequest(Method.PATCH);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic " + apiKey);
                request.AddParameter("status", Constants.DELETED);

                try
                {
                    response = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                }
                catch (Exception ex)
                {

                }
            }

            if (response != null && response.IsSuccessful)
            {
                var strResponse = response?.Content;
                dynamic responseData = string.IsNullOrEmpty(strResponse) ? "" : JsonConvert.DeserializeObject(strResponse);

                if (responseData != null)
                {
                    result = responseData.uuid != null;
                }
            }

            return result;
        }

        public async Task<IList<Job>> GetAcceptedJobs(string contractorUUID)
        {
            if (string.IsNullOrEmpty(contractorUUID))
                return null;

            var results = new List<Job>();
            var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();

            if (!string.IsNullOrEmpty(baseUrl))
            {
                var fullUrl = baseUrl + "/requests" + "?contractor=" + contractorUUID;
                await CallGetJobsApi(results, fullUrl);
            }

            return results;
        }

        public async Task<IList<Job>> GetAvailableJobs(string requestorUUID, string city, string workCategory)
        {
            if (string.IsNullOrEmpty(requestorUUID) || string.IsNullOrEmpty(city))
                return null;

            var results = new List<Job>();
            var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();

            if (!string.IsNullOrEmpty(baseUrl))
            {
                city = System.Net.WebUtility.UrlEncode(city);
                var fullUrl = baseUrl + "/requests" + "?city=" + city + "&&status=pending";

                if (!string.IsNullOrEmpty(workCategory))
                {
                    workCategory = System.Net.WebUtility.UrlEncode(workCategory);
                    fullUrl += "&&work_category=" + workCategory;
                }

                await CallGetJobsApi(results, fullUrl);
            }

            return results?
                .Where(p=>p.RequestorSystemUUID != requestorUUID)
                .Select(p=>p).ToList();
        }

        public async Task<IList<Job>> GetRequestedJobs(string requestorUUID, string status)
        {
            if (string.IsNullOrEmpty(requestorUUID))
                return null;

            var results = new List<Job>();
            var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();

            if (!string.IsNullOrEmpty(baseUrl))
            {
                var fullUrl = baseUrl + "/requests" + "?requestor_uuid=" + requestorUUID;

                if (!string.IsNullOrEmpty(status))
                {
                    fullUrl += "&status=" + status;
                }

                await CallGetJobsApi(results, fullUrl);
            }

            return results;
        }

        public async Task<bool> SuspendJob(string jobSystemUUID)
        {
            bool result = false;
            IRestResponse response = null;

            response = await CallApiSetJobStatus(jobSystemUUID, response, Constants.SUSPENDED);

            return result;
        }

        public async Task<EditJobRequestResponse> UpdateRequest(EditJobRequestRequest jobRequest)
        {
            EditJobRequestResponse result = null;
            IRestResponse response = null;

            if (jobRequest != null)
            {
                result = new EditJobRequestResponse();
                var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
                var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();

                if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
                {
                    var fullUrl = baseUrl + "/requests/" + jobRequest.SystemUUID + "/";
                    //var requestorUUID = jobRequest.RequestorSystemUUID;
                    var title = jobRequest.Title;
                    var scopeOfWork = jobRequest.ScopeOfWork;
                    var address = jobRequest.Address;
                    var city = jobRequest.City;
                    var country = jobRequest.Country;
                    var longitude = jobRequest.Longitude;
                    var latitude = jobRequest.Latitude;
                    var workCategory = jobRequest.Category;
                    var preferredStartTime = jobRequest.StartDateTime;
                    var preferredEndTime = jobRequest.EndDateTime;

                    var client = new RestClient(fullUrl);
                    var request = new RestRequest(Method.PATCH);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "Basic " + apiKey);
                    //request.AddParameter("requestor_uuid", requestorUUID);
                    request.AddParameter("title", title);
                    request.AddParameter("scope_of_work", scopeOfWork);
                    request.AddParameter("address", address);
                    request.AddParameter("city", city);
                    request.AddParameter("country", country);
                    request.AddParameter("longitude", longitude);
                    request.AddParameter("latitude", latitude);
                    request.AddParameter("work_category", workCategory);
                    request.AddParameter("preferred_start_datetime", preferredStartTime);
                    request.AddParameter("preferred_end_datetime", preferredEndTime);

                    try
                    {
                        if (File.Exists(jobRequest.ImagePath))
                        {
                            using (var fs = File.OpenRead(jobRequest.ImagePath))
                            {
                                var bytes = Utilities.StreamToBytes(fs);
                                request.AddFile("image", bytes, Guid.NewGuid().ToString() + ".jpg", "image/jpg");
                            }
                        }

                        if (File.Exists(jobRequest.VoiceNotePath))
                        {
                            using (var fs = File.OpenRead(jobRequest.VoiceNotePath))
                            {
                                var bytes = Utilities.StreamToBytes(fs);
                                request.AddFile("voice_note", bytes, Guid.NewGuid().ToString() + ".mp3", "audio/mpeg");
                            }
                        }

                        response = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }

            if (response != null && response.IsSuccessful)
            {
                var strResponse = response?.Content;
                dynamic responseData = string.IsNullOrEmpty(strResponse) ? "" : JsonConvert.DeserializeObject(strResponse);

                if (responseData != null)
                {
                    result.SystemGeneratedId = responseData.uuid;
                    result.IsSuccess = true;
                }
            }

            return result;
        }

        private async Task CallGetJobsApi(List<Job> results, string fullUrl)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(fullUrl).ConfigureAwait(false);
                    var content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        dynamic responseData = JsonConvert.DeserializeObject(content);
                        if (responseData != null && responseData.Count > 0)
                        {
                            foreach (dynamic item in responseData)
                            {
                                var job = new Job
                                {
                                    SystemUUID = item?.uuid,
                                    RequestorSystemUUID = item?.requestor_uuid,
                                    ImagePath = item?.image,
                                    Title = item?.title,
                                    ScopeOfWork = item?.scope_of_work,
                                    VoiceNotePath = item?.voice_note,
                                    Address = item?.address,
                                    City = item?.city,
                                    Country = item?.country,
                                    ContractorSystemUUID = item?.contractor,
                                    Category = item?.work_category,
                                    Status = item?.status,
                                    Comment = item?.comment,

                                    StartDateTime = item?.preferred_start_datetime,
                                    EndDateTime = item?.preferred_end_datetime,
                                    ModifiedDateTime = item?.modified,
                                    CreatedDateTime = item?.created,
                                };

                                string longitude = item?.longitude;
                                if (!string.IsNullOrEmpty(longitude))
                                {
                                    job.Longitude = float.Parse(longitude);
                                }

                                string latitude = item?.latitude;
                                if (!string.IsNullOrEmpty(latitude))
                                {
                                    job.Latitude = float.Parse(latitude);
                                }

                                string rating = item?.rating;
                                if (!string.IsNullOrEmpty(rating))
                                {
                                    job.Rating = float.Parse(rating);
                                }

                                results.Add(job);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
