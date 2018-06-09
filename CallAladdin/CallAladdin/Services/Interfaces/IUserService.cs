using CallAladdin.Model;

namespace CallAladdin.Services.Interfaces
{
    public interface IUserService
    {
        bool RegisterUser(UserRegistration userRegistration);
    }
}