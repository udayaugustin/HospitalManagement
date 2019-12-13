using System;
using System.IO;
using HospitalManagement.Droid;
using SQLite;

using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace HospitalManagement.Droid
{
	public class SQLiteDb : ISQLiteDb
	{
		public SQLiteAsyncConnection GetConnection()
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "MySQLite.db3");

			return new SQLiteAsyncConnection(path);
		}

        public SQLiteConnection GetNormalConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteConnection(path);
        }
    }
}

