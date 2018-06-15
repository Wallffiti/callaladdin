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

        public int CreateOrUpdate(UserIdentity userIdentity)
        {
            int rows = -1;
            userIdentity.Principal = Constants.PRINCIPAL;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserIdentity>();
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
                    con.CreateTable<UserIdentity>();
                    rows = con.DeleteAll<UserIdentity>();
                }
            }
            catch (Exception ex)
            {

            }

            return rows;
        }

        public UserIdentity GetUserIdentity()
        {
            UserIdentity result = null;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserIdentity>();
                    result = con.Query<UserIdentity>("select * from UserIdentity where Principal = ?", Constants.PRINCIPAL).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public IList<UserIdentity> GetAll()
        {
            IList<UserIdentity> results = null;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserIdentity>();
                    results = con.Query<UserIdentity>("select * from UserIdentity where Principal = ?", "*");
                }
            }
            catch (Exception ex)
            {

            }

            return results;
        }
    }
}
