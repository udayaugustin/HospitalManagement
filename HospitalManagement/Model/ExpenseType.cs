using System;
using System.Collections.Generic;

namespace HospitalManagement.Model
{
    public class ExpenseType
    {
        public static List<string> ExpenseTypeList()
        {
            return new List<string>
            {
                "Materials",
                "Medicines",
                "Rent",
                "Electrycity",
                "Garbage",
                "Others"
            };
        }
    }
}
