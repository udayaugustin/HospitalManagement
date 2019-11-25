using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Model
{
    public class Treatment
    {
        [AutoIncrement,PrimaryKey]
        public int Id { get; set; }

        public DateTime TreatmentDate { get; set; }

        public int PatientId { get; set; }

        public string Cheif_Complaint { get; set; }

        public string Patient_History { get; set; }

        public string Diagnosis { get; set; }

        public string TreatmentPlan {get; set;}

        public int TreatmentCost { get; set; }
    }
}
