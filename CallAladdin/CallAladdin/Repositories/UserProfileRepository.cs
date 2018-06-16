using CallAladdin.Model;
using CallAladdin.Model.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CallAladdin.Repositories.Interfaces;

namespace CallAladdin.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private string dbName = "";

        public UserProfileRepository()
        {
            dbName = GlobalConfig.Instance.GetByKey("call_aladdin.sqlite_path")?.ToString();
        }

        public int CreateOrUpdate(UserProfileEntity userProfileEntity)
        {
            int rows = -1;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserProfileEntity>();
                    rows = con.InsertOrReplace(userProfileEntity);
                }
            }
            catch (Exception ex)
            {

            }

            return rows;
        }

        public int DeleteUserProfile()
        {
            int rows = -1;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserProfileEntity>();
                    rows = con.DeleteAll<UserProfileEntity>();
                }
            }
            catch (Exception ex)
            {

            }

            return rows;
        }

        public UserProfileEntity GetUserProfile(string email)
        {
            UserProfileEntity result = null;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserProfileEntity>();
                    //result = con.Query<UserProfileEntity>("select * from UserProfileEntity where Email = ?", email).LastOrDefault();
                    result = con.Table<UserProfileEntity>().Where(p => p.Email == email)?.LastOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public IList<UserProfileEntity> GetAll()
        {
            IList<UserProfileEntity> results = null;

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(dbName))
                {
                    con.CreateTable<UserProfileEntity>();
                    results = con.Table<UserProfileEntity>()?.ToList();// con.Query<UserProfileEntity>("select * from UserProfileEntity where Email = ?", "*");
                }
            }
            catch (Exception ex)
            {

            }

            return results;
        }
    }
}
