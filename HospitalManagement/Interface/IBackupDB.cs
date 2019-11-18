using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    public interface IBackupDB
    {
        Task<bool> SyncDBAsync(string username);
    }
}
