﻿using CallAladdin.Model;
using CallAladdin.Model.Requests;
using CallAladdin.Model.Responses;
using CallAladdin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using RestSharp;
using CallAladdin.Helper;
using Newtonsoft.Json.Linq;

namespace CallAladdin.Services
{
    public class UserService : IUserService
    {
        public async Task<UserRegistrationOnServerResponse> CreateUser(UserRegistration userRegistration, string localId)
        {
            if (string.IsNullOrEmpty(localId) || userRegistration == null)
                return null;

            var result = new UserRegistrationOnServerResponse();
            //result.IsSuccess = true;    //DEBUG
            var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
            var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();
            IRestResponse response = null;

            if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
            {
                var fullUrl = baseUrl + "/user_profiles/";
                var name = userRegistration.Name;
                var city = userRegistration.City;
                //var tempPhone = userRegistration.Mobile.Replace(" ", "").Trim();
                //var phone = tempPhone.StartsWith("+") ? tempPhone : "+" + tempPhone;
                var phone = userRegistration.Mobile;
                var address = string.IsNullOrEmpty(userRegistration.CompanyAddress) ? "Unspecified" : userRegistration.CompanyAddress;
                var country = userRegistration.Country;
                var email = userRegistration.Email;
                var isContractor = userRegistration.IsRegisteredAsContractor;
                var category = string.IsNullOrEmpty(userRegistration.Category) ? "" : userRegistration.Category;
                var companyName = string.IsNullOrEmpty(userRegistration.CompanyName) ? "Unspecified" : userRegistration.CompanyName;
                var companyAddress = string.IsNullOrEmpty(userRegistration.CompanyAddress) ? "Unspecified" : userRegistration.CompanyAddress;


                var client = new RestClient(fullUrl);
                var request = new RestRequest(Method.POST);
                //request.AddHeader("postman-token", "c51b5943-db03-a887-372e-79ef32cb03c1");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic " + apiKey);
                request.AddParameter("name", name);
                request.AddParameter("city", city);
                request.AddParameter("phone", phone);
                request.AddParameter("address", address);
                request.AddParameter("country", country);
                request.AddParameter("email", email);
                request.AddParameter("identifier_for_vendor", localId);
                request.AddParameter("is_contractor", isContractor);
                request.AddParameter("work_categories", category);
                request.AddParameter("company_name", companyName);
                request.AddParameter("company_address", companyAddress);

                try
                {
                    if (File.Exists(userRegistration.ProfileImagePath))
                    {
                        using (var fs = File.OpenRead(userRegistration.ProfileImagePath))
                        {
                            var bytes = Utilities.StreamToBytes(fs);
                            request.AddFile("image", bytes, Guid.NewGuid().ToString() + ".jpg", "image/jpg");

                            response = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        response = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                    }
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
                    result.SystemGeneratedId = responseData.uuid;
                    result.IsSuccess = true;
                }
            }

            return result;
        }

        public async Task<UserProfile> GetUserProfileByUUID(string uuid)
        {
            UserProfile result = null;

            var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
            var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();

            if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
            {
                var fullUrl = baseUrl + "/user_profiles" + "/" + uuid + "/";
                result = await GetUserProfile(result, fullUrl);
            }

            return result;
        }

        public async Task<UserProfile> GetUserProfileByAuthLocalId(string localId)
        {
            UserProfile result = null;

            var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
            var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();

            if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
            {
                var fullUrl = baseUrl + "/user_profiles" + "?identifier_for_vendor=" + localId;
                result = await GetUserProfile(result, fullUrl);
            }

            return result;
        }

        private bool IsArray(dynamic data)
        {
            if (data != null)
            {
                dynamic count = data.Count;
                int val;
                if (count != null && int.TryParse(count.ToString(), out val))
                    return true;
            }

            return false;
        }

        private async Task<UserProfile> GetUserProfile(UserProfile result, string fullUrl)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(fullUrl).ConfigureAwait(false);
                    var content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        dynamic responseData = JsonConvert.DeserializeObject(content);
                        dynamic item = null; //responseData?[0];

                        if (IsArray(responseData))
                        {
                            item = responseData?[0];
                        }
                        else
                        {
                            item = responseData;
                        }

                        if (item != null)
                        {
                            result = new UserProfile
                            {
                                SystemUUID = item.uuid,
                                Name = item.name,
                                Email = item.email,
                                PathToProfileImage = item.image,
                                CompanyRegisteredAddress = item.address,
                                Longitude = item.longitude,
                                Latitude = item.latitude,
                                Mobile = item.phone,
                                City = item.city,
                                Country = item.country,
                                IsContractor = item.is_contractor,
                                CompanyName = item.company_name,
                                Category = item.work_categories,
                                CreatedDate = item.created
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    //result = new UserProfile()    //DEBUG
                    //{
                    //    Name = "Jackson",
                    //    Mobile = "012 345 678",
                    //    Email = "dimensionconcept@yahoo.com",
                    //    City = "Miri",
                    //    Country = "Malaysia",
                    //    Category = Constants.INTERIOR_DESIGN_CARPENTERS,
                    //    CompanyName = "Dimension Concept Interior Design Sdn. Bhd.",
                    //    CompanyRegisteredAddress = "LOT 1234, Senadin Phase 2, Jalan 12345, 98000 Miri, Sarawak",
                    //    OverallRating = 4,
                    //    TotalReviewers = 102,
                    //    LastReviewedDate = new DateTime(2018, 5, 1),
                    //    IsContractor = localId == "contractor"
                    //};
                }
            }

            return result;
        }

        public async Task<UserLoginResponse> LoginUserToAuthServer(UserLogin userLogin)
        {
            UserLoginResponse result = null;
            string fullUrl = "";
            var baseUrl = GlobalConfig.Instance.GetByKey("com.google.android.firebase.restful.api.url")?.ToString();
            var apiKey = GlobalConfig.Instance.GetByKey("com.google.android.firebase.API_KEY")?.ToString();

            if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
            {
                fullUrl = baseUrl + "/verifyPassword?key=" + apiKey;
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var body = new UserLoginRequest
                        {
                            email = userLogin.Email,
                            password = userLogin.Password,
                            returnSecureToken = true
                        };
                        var bodyStr = JsonConvert.SerializeObject(body);
                        var stringContent = new StringContent(bodyStr, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(fullUrl, stringContent).ConfigureAwait(false);
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        result = new UserLoginResponse();
                        dynamic deserializedContent = JsonConvert.DeserializeObject(content);

                        if (deserializedContent?.error != null)
                        {
                            result.IsError = true;
                            result.ErrorMessage = deserializedContent.error?.message?.ToString();
                        }

                        if (deserializedContent?.idToken != null)
                        {
                            result.IdToken = deserializedContent.idToken.ToString();
                            var expiresIn = deserializedContent.expiresIn?.ToString();
                            result.ExpiresIn = int.Parse(expiresIn);
                            result.LocalId = deserializedContent.localId?.ToString();
                            result.RefreshToken = deserializedContent.refreshToken?.ToString();
                            result.Email = deserializedContent.email?.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return result;

        }

        public async Task<UserSignupResponse> RegisterUserToAuthServer(UserRegistration userRegistration)
        {
            UserSignupResponse result = null;
            string fullUrl = "";
            var isUsingPasswordless = (bool)GlobalConfig.Instance.GetByKey("use_passwordless");

            if (isUsingPasswordless)
            {
                //TODO: replace all below dummy data
                result = new UserSignupResponse();
                result.IdToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjdhM2QxOTA0ZjE4ZTI1Nzk0ODgzMWVhYjgwM2UxMmI3OTcxZTEzYWIifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vY2FsbC1hbGFkZGluLXByb2plY3QiLCJhdWQiOiJjYWxsLWFsYWRkaW4tcHJvamVjdCIsImF1dGhfdGltZSI6MTUyODcwNDY3MSwidXNlcl9pZCI6Inh4enlLU1ZRb0JkOXp3YXNQS1pZS3lhSXJFTjIiLCJzdWIiOiJ4eHp5S1NWUW9CZDl6d2FzUEtaWUt5YUlyRU4yIiwiaWF0IjoxNTI4NzA0NjcxLCJleHAiOjE1Mjg3MDgyNzEsImVtYWlsIjoidGVzdGVyMUB0ZXN0LmNvbSIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJlbWFpbCI6WyJ0ZXN0ZXIxQHRlc3QuY29tIl19LCJzaWduX2luX3Byb3ZpZGVyIjoicGFzc3dvcmQifX0.fYl8oUedvNKwhTIT5XYz - H92Ddd1uNNp9YJB1fL0XSoHba0tkn4UpT2lbyrYKoJjXhfnPg2CEexvpkxRBUAMDk0JyfoM9nr1jdn6sBMUS_ILao95zI7jZOJ3mIyI6hl1S9GBIYGYRrvbM3su6JV4KNBv7SrHjNDvTI6UylUFVj6PgeFSM4oAJN4lLt1Ry2NVNAnDEhF_rCHshLwVw - IBP_J9OudknmS0R5OdAhD7gFrKsDkeCRE_ysNyrP_19Ys4FWgNui9sBQNHERcqQvUDR9qX - LSRccqTUbAeAHSf4Bfr0TBids3TPhbwWCgbk1X_byIV7EdIKd1jMU6Bxzd30g";
                result.RefreshToken = "AK2wQ-zMoiNL4L4yw1MkDG_qkjT2OBES8KNJKYNQKo0fhRiavidLAmBXQeLetYlwkxhnxj7woP69cFC5auhBWyatYkhT6r96mE7UA9RnvUDbLvVQNKoaRSIeSAcOxoKuWf2X8F6LInWr_aQzkKAgF2hKDfkjy-H4HWurJAvnmieqrY48XCSpkJSLjh2AHnAlGYTRQUZ_bMx66vlCT6Xg82_bbPba6JYhtWjYqO7P-Y2bg3fAi0dBAEk";
                result.ExpiresIn = 3600;
                result.LocalId = "xxzyKSVQoBd9zwasPKZYKyaIrEN2";
                result.Email = "tester0@test.com";
            }
            else
            {
                var baseUrl = GlobalConfig.Instance.GetByKey("com.google.android.firebase.restful.api.url")?.ToString();
                var apiKey = GlobalConfig.Instance.GetByKey("com.google.android.firebase.API_KEY")?.ToString();

                if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
                {
                    fullUrl = baseUrl + "/signupNewUser?key=" + apiKey;
                    try
                    {
                        using (var httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            //var body = new
                            //{
                            //    email = userRegistration.Email,
                            //    password = userRegistration.Password,
                            //    returnSecureToken = true

                            //};
                            var body = new UserSignupRequest()
                            {
                                email = userRegistration.Email,
                                password = userRegistration.Password,
                                returnSecureToken = true
                            };
                            var bodyStr = JsonConvert.SerializeObject(body);
                            var stringContent = new StringContent(bodyStr, Encoding.UTF8, "application/json");
                            var response = await httpClient.PostAsync(fullUrl, stringContent).ConfigureAwait(false);
                            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            result = new UserSignupResponse();
                            dynamic deserializedContent = JsonConvert.DeserializeObject(content);

                            if (deserializedContent?.error != null)
                            {
                                result.IsError = true;
                                result.ErrorMessage = deserializedContent.error?.message?.ToString();
                            }

                            if (deserializedContent?.idToken != null)
                            {
                                result.IdToken = deserializedContent.idToken.ToString();
                                var expiresIn = deserializedContent.expiresIn?.ToString();
                                result.ExpiresIn = int.Parse(expiresIn);
                                result.LocalId = deserializedContent.localId?.ToString();
                                result.RefreshToken = deserializedContent.refreshToken?.ToString();
                                result.Email = deserializedContent.email?.ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }

            return result;
        }

        public async Task<bool> SendForgottenPasswordLink(string email)
        {
            bool result = false;
            string fullUrl = "";

            var baseUrl = GlobalConfig.Instance.GetByKey("com.google.android.firebase.restful.api.url")?.ToString();
            var apiKey = GlobalConfig.Instance.GetByKey("com.google.android.firebase.API_KEY")?.ToString();

            if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(email))
            {
                fullUrl = baseUrl + "/getOobConfirmationCode?key=" + apiKey;
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var body = new ForgottenPasswordRequest
                        {
                            requestType = "PASSWORD_RESET",
                            email = email
                        };
                        var bodyStr = JsonConvert.SerializeObject(body);
                        var stringContent = new StringContent(bodyStr, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(fullUrl, stringContent).ConfigureAwait(false);
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var deserializedContent = JsonConvert.DeserializeObject<ForgottenPasswordResponse>(content);

                        result = deserializedContent != null && !string.IsNullOrEmpty(deserializedContent.email) && !string.IsNullOrEmpty(deserializedContent.kind);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return result;
        }

        public async Task<bool> UpdateUserProfile(UserProfile userProfile, string localId)
        {
            IRestResponse response = null;
            var result = false;

            if (userProfile != null && !string.IsNullOrEmpty(localId))
            {
                var baseUrl = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
                var apiKey = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.key")?.ToString();

                if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
                {
                    var fullUrl = baseUrl + "/user_profiles/" + userProfile.SystemUUID + "/";

                    var name = userProfile.Name;
                    var city = userProfile.City;
                    var phone = userProfile.Mobile;
                    var address = string.IsNullOrEmpty(userProfile.CompanyRegisteredAddress) ? "Unspecified" : userProfile.CompanyRegisteredAddress;
                    var country = userProfile.Country;
                    var email = userProfile.Email;
                    var isContractor = userProfile.IsContractor;
                    var category = string.IsNullOrEmpty(userProfile.Category) ? "" : userProfile.Category;
                    var companyName = string.IsNullOrEmpty(userProfile.CompanyName) ? "Unspecified" : userProfile.CompanyName;
                    var companyAddress = string.IsNullOrEmpty(userProfile.CompanyRegisteredAddress) ? "Unspecified" : userProfile.CompanyRegisteredAddress;

                    var client = new RestClient(fullUrl);
                    var request = new RestRequest(Method.PATCH);

                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "Basic " + apiKey);
                    request.AddParameter("name", name);
                    request.AddParameter("city", city);
                    request.AddParameter("phone", phone);
                    request.AddParameter("address", address);
                    request.AddParameter("country", country);
                    request.AddParameter("email", email);
                    request.AddParameter("identifier_for_vendor", localId);
                    request.AddParameter("is_contractor", isContractor);
                    request.AddParameter("work_categories", category);
                    request.AddParameter("company_name", companyName);
                    request.AddParameter("company_address", companyAddress);

                    try
                    {
                        //if it is not email, then assume that it is path for a file locally
                        if (!Validators.ValidateUrl(userProfile.PathToProfileImage))
                        {
                            if (File.Exists(userProfile.PathToProfileImage))
                            {
                                using (var fs = File.OpenRead(userProfile.PathToProfileImage))
                                {
                                    var bytes = Utilities.StreamToBytes(fs);
                                    request.AddFile("image", bytes, Guid.NewGuid().ToString() + ".jpg", "image/jpg");
                                }
                            }
                        }

                        response = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            result = response != null && response.IsSuccessful;

            return result;
        }
    }
}
