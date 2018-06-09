using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Services
{
    public class MockLocationService : ILocationService
    {
        public IList<string> GetCities(string area)
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

        public IList<string> GetCountries()
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
