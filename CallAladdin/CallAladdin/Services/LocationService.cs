using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Services
{
    public class LocationService : ILocationService
    {
        public IList<string> GetCountries()
        {
            return null;
        }

        public IList<string> GetCities(string area)
        {
            return null;
        }
    }
}
