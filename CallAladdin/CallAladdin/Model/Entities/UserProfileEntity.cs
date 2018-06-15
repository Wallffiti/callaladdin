using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Model.Entities
{
    public class UserProfileEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegisteredAddress { get; set; }
        public string PathToProfileImage { get; set; }
        public bool IsContractor { get; set; }
    }
}
