using System.Collections.Generic;
using CallAladdin.Model.Database;

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