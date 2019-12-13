using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Model
{
    public class Appoinment
    {
        [PrimaryKey,AutoIncrement]        
        public int Id { get; set; }

        public int TreatmentId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string TreatmentPlan { get; set; }

        public string TreatmentDone { get; set; }

        public int SerialNo { get; set; }


        //The below fields are used to stored the next appointment
        public bool IsSheduleNextVisit { get; set; }

        public DateTime NextAppointmentDate { get; set; }

        public TimeSpan NextAppointmentTime { get; set; }        
    }
}
