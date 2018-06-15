using System.Collections.Generic;
using CallAladdin.Model.Entities;

namespace CallAladdin.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        int CreateOrUpdate(UserProfileEntity userProfileEntity);
        int DeleteUserProfile();
        IList<UserProfileEntity> GetAll();
        UserProfileEntity GetUserProfile(string email);
    }
}