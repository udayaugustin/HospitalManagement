using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Model
{
    public class Patient
    {
        [AutoIncrement][PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public string MobileNo { get; set; }

        public string Address { get; set; }
    }
}
