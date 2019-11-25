using System;
using SQLite;

namespace HospitalManagement.Model
{
    public class PatientTransaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public int  PaidAmount { get; set; }
    }
}
