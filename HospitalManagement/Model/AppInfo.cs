using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Model
{
    public class AppInfo
    {
        [PrimaryKey]
        public string Username { get; set; }

        public string AppID { get; set; }

        public DateTime LastSynchDate { get; set; }

        public bool IsAppIdValidated { get; set; }
    }
}
