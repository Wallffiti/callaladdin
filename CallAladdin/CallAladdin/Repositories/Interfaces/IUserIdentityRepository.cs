using System.Collections.Generic;
using CallAladdin.Model.Entities;

namespace CallAladdin.Repositories.Interfaces
{
    public interface IUserIdentityRepository
    {
        int CreateOrUpdate(UserIdentityEntity userIdentity);
        int DeleteUserIdentity();
        IList<UserIdentityEntity> GetAll();
        UserIdentityEntity GetUserIdentity();
    }
}