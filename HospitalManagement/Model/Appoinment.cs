using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Model
{
    class Appoinment
    {
        [AutoIncrement][PrimaryKey]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string TreatmentPlan { get; set; }

        public string TreatmentDone { get; set; }
    }
}
