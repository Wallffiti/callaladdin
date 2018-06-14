using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CallAladdin.Services
{
    public class MockLocationService : ILocationService
    {
        public async Task<IList<string>> GetCities(string area)
        {
            return new List<string>()
            {
                "Kuala Lumpur",
                "Petaling Jaya",
                "Ipoh",
                "Taiping",
                "Johor Bharu",
                "Miri"
            };
        }

        public async Task<IList<string>> GetCountries()
        {
            return new List<string>()
            {
                "Australia",
                "Malaysia",
                "Singapore",
                "Indonesia",
                "Thailand"
            };
        }
    }
}
