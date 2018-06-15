using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Database
{
    public class UserIdentity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Principal { get; set; }
        public string IdToken { get; set; }
        public string LocalId { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
