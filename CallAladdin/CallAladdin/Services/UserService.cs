using CallAladdin.Model;
using CallAladdin.Model.Responses;
using CallAladdin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CallAladdin.Services
{
    public class UserService : IUserService
    {
        public async Task<object> CreateUser(UserRegistration userRegistration)
        {
            //TODO: replace the below dummy data
            return new
            {
                success = true
            };

            //return null;
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
            }
            else
            {
                var baseUrl = GlobalConfig.Instance.GetByKey("com.google.android.firebase.restful.api.url")?.ToString();
                var apiKey = GlobalConfig.Instance.GetByKey("com.google.android.firebase.API_KEY")?.ToString();

                if (!string.IsNullOrEmpty(baseUrl) && !string.IsNullOrEmpty(apiKey))
                {
                    fullUrl = baseUrl + "/signupNewUser?key=" + apiKey;
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var body = new
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
                        }
                    }
                }
            }

            return result;
        }
    }
}
