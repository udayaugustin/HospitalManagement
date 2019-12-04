using HospitalManagement.Model;
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
	public partial class AppointmentList : ContentPage
	{
        private SQLiteAsyncConnection connection;
        private List<Appoinment> appoinmentlist;

		public AppointmentList ()
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            GetData();
		}

        public async void GetData()
        {
            appoinmentlist = new List<Appoinment>();
            appoinmentlist = await connection.Table<Appoinment>().Where(t => t.Date == DateTime.Today).ToListAsync();
            var counter = 1;
            foreach (var appoinment in appoinmentlist)
            {
                appoinment.SerialNo = counter++;
            }
            appointmentlistview.ItemsSource = appoinmentlist;
        }

        private void Appointmentlistview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var appoinment = e.SelectedItem as Appoinment;
          //  Navigation.PushAsync(TreatmentDetailPage());
        }
    }
}