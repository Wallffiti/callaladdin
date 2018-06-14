using System.Collections.Generic;
using System.Threading.Tasks;

namespace CallAladdin.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IList<string>> GetCities(string area);
        Task<IList<string>> GetCountries();
    }
}