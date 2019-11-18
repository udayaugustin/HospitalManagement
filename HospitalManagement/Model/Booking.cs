using SQLite;
using System;
namespace HospitalManagement
{
    public class Booking
    {
        [AutoIncrement][PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        public int BookingCost { get; set; }

        public int AdvanceAmount { get; set; }

        public string PaymentMode { get; set; }

        public int BalanceAmount { get; set; }

        public int PaidAmount { get; set; }

        public bool IsCompleted { get; set; }
    }
}