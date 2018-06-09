using System.Collections.Generic;

namespace CallAladdin.Services.Interfaces
{
    public interface ILocationService
    {
        IList<string> GetCities(string area);
        IList<string> GetCountries();
    }
}