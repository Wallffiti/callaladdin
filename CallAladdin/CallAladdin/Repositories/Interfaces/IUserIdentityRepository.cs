using System.Collections.Generic;
using CallAladdin.Model.Entities;

namespace CallAladdin.Repositories.Interfaces
{
    public interface IUserIdentityRepository
    {
        int CreateOrUpdate(UserIdentity userIdentity);
        int DeleteUserIdentity();
        IList<UserIdentity> GetAll();
        UserIdentity GetUserIdentity();
    }
}