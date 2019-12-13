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
        private SQLiteConnection syncConnection;
        private List<Appoinment> appoinmentlist;

		public AppointmentList ()
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            syncConnection = DependencyService.Get<ISQLiteDb>().GetNormalConnection();
            GetData();
		}

        public async void GetData()
        {
            appoinmentlist = new List<Appoinment>();
            appoinmentlist = await connection.Table<Appoinment>().Where(t => t.Date == DateTime.Today).ToListAsync();
            var appointmentData = (from a in syncConnection.Table<Appoinment>() where a.Date == DateTime.Today
                         join t in syncConnection.Table<Treatment>()
                         on a.TreatmentId equals t.Id
                         join p in syncConnection.Table<Patient>()
                         on t.PatientId equals p.Id
                         select new AppointmentListModel
                         {
                             Treatment = t,
                             AppointmentInfo = a,
                             Patient = p
                         }).ToList();
                         
            var counter = 1;
            foreach (var appoinment in appointmentData)
            {
                appoinment.SerialNo = counter++;                
            }
            appointmentlistview.ItemsSource = appointmentData;
        }

        private void Appointmentlistview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var appoinment = e.SelectedItem as Appoinment;
          //  Navigation.PushAsync(TreatmentDetailPage());
        }
    }

    public class AppointmentListModel
    {
        public Treatment Treatment { get; set; }

        public Appoinment AppointmentInfo { get; set; }

        public Patient Patient { get; set; }

        public int SerialNo { get; set; }

        public string AppointDateTime
        {
            get
            {                  
                return AppointmentInfo.Date.Date.ToShortDateString() + " " + AppointmentInfo.Time.Hours + ":" + AppointmentInfo.Time.Minutes;
            }
        }
    }
}