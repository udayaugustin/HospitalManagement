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

<<<<<<< HEAD
        public string Address { get; set; }

       

=======
        public string Address { get; set; }        
>>>>>>> c5f36a7... Serila no added in treatment model
    }
}
