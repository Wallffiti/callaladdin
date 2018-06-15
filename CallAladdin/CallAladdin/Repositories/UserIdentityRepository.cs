using CallAladdin.Model.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CallAladdin.Repositories.Interfaces;

namespace CallAladdin.Repositories
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        private string dbName = "";

        public UserIdentityRepository()
        {
            dbName = GlobalConfig.Instance.GetByKey("call_aladdin.sqlite_path")?.ToString();
        }

        public int CreateOrUpdate(UserIdentityEntity userIdentity)
        {
            int rows = -1;
            userIdentity.Principal = Constants.PRINCIPAL;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserIdentityEntity>();
                    rows = con.InsertOrReplace(userIdentity);
                }
            }
            catch (Exception ex)
            {

            }

            return rows;
        }

        public int DeleteUserIdentity()
        {
            int rows = -1;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserIdentityEntity>();
                    rows = con.DeleteAll<UserIdentityEntity>();
                }
            }
            catch (Exception ex)
            {

            }

            return rows;
        }

        public UserIdentityEntity GetUserIdentity()
        {
            UserIdentityEntity result = null;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserIdentityEntity>();
                    //result = con.Query<UserIdentityEntity>("select * from UserIdentity where Principal = ?", Constants.PRINCIPAL).LastOrDefault();
                    result = con.Table<UserIdentityEntity>().Where(p => p.Principal == Constants.PRINCIPAL).Single();
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public IList<UserIdentityEntity> GetAll()
        {
            IList<UserIdentityEntity> results = null;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserIdentityEntity>();
                    results = con.Table<UserIdentityEntity>()?.ToList(); //con.Query<UserIdentityEntity>("select * from UserIdentity where Principal = ?", "*");
                }
            }
            catch (Exception ex)
            {

            }

            return results;
        }
    }
}
