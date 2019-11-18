using SQLite;

namespace HospitalManagement
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

