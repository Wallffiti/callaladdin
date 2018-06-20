using CallAladdin.Model;
using CallAladdin.Model.Responses;
using System.Threading.Tasks;

namespace CallAladdin.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserSignupResponse> RegisterUserToAuthServer(UserRegistration userRegistration);
        Task<UserLoginResponse> LoginUserToAuthServer(UserLogin userLogin);
        Task<UserRegistrationOnServerResponse> CreateUser(UserRegistration userRegistration, string localId);
        Task<UserProfile> GetUserProfile(string localId);
        Task<bool> UpdateUserProfile(UserProfile userProfile);
        Task<bool> SendForgottenPasswordLink(string email);
    }
}