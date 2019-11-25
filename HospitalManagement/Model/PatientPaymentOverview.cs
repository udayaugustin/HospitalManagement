using SQLite;

namespace HospitalManagement.Model
{
    public class PatientPaymentOverview
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int OverallBalance { get; set; }

        public int PaidAmount { get; set; }
    }
}
