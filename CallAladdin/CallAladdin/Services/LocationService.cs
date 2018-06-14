using CallAladdin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CallAladdin.Services
{
    public class LocationService : ILocationService
    {
        private string url = "";

        public LocationService()
        {
            url = GlobalConfig.Instance.GetByKey("com.call.aladdin.project.api.url")?.ToString();
        }

        public async Task<IList<string>> GetCountries()
        {
            IList<string> results = null;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url + "/countries").ConfigureAwait(false);
                if (response?.IsSuccessStatusCode == true)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    dynamic deserializedContent = JsonConvert.DeserializeObject<List<dynamic>>(content);
                    results = new List<string>();
                    foreach (dynamic item in deserializedContent)
                    {
                        results.Add(item?.name?.ToString());
                    }
                }
            }

            return results;
        }

        public async Task<IList<string>> GetCities(string area)
        {
            IList<string> results = null;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url + "/cities").ConfigureAwait(false);
                if (response?.IsSuccessStatusCode == true)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    dynamic deserializedContent = JsonConvert.DeserializeObject<List<dynamic>>(content);
                    results = new List<string>();
                    foreach (var item in deserializedContent)
                    {
                        results.Add(item?.name?.ToString());
                    }
                }
            }

            return results;
        }
    }
}
