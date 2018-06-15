using CallAladdin.Model;
using CallAladdin.Model.Responses;
using System.Threading.Tasks;

namespace CallAladdin.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserSignupResponse> RegisterUserToAuthServer(UserRegistration userRegistration);
        Task<UserRegistrationOnServerResponse> CreateUser(UserRegistration userRegistration);
        Task<UserProfile> GetUserProfile(string localId);
    }
}