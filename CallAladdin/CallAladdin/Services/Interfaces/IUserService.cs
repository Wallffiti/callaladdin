using CallAladdin.Model;
using CallAladdin.Model.Responses;
using System.Threading.Tasks;

namespace CallAladdin.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserSignupResponse> RegisterUserToAuthServer(UserRegistration userRegistration);
        Task<object> CreateUser(UserRegistration userRegistration);
    }
}