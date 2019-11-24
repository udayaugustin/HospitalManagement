using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Model
{
    class Appoinment
    {
        [PrimaryKey,AutoIncrement]        
        public int Id { get; set; }

        public int TreatmentId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string TreatmentPlan { get; set; }

        public string TreatmentDone { get; set; }
    }
}
