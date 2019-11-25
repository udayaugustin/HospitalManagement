using System;
using SQLite;

namespace HospitalManagement.Model
{
    public class ExpenseTransaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }        

        public int PaidAmount { get; set; }
    }
}