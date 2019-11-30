using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HospitalManagement.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FinalReport : ContentPage
	{
        private DateTime Fromdate;
        private DateTime Todate;
        private SQLiteAsyncConnection connection;
		public FinalReport ()
		{

			InitializeComponent ();
            Fromdate = fromdate.Date;
            Todate = todate.Date;
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            Getfinalreport();
        }

        private void Getfinalreport()
        {

        }

    }
}