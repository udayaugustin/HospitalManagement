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
	public partial class PatientList : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Patient> patientList;
        public PatientList ()
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            GetData();
        }

        private async void GetData()
        {
            patientList = new List<Patient>();

            patientList = await connection.Table<Patient>().ToListAsync();
            patientList = patientList.ToList();           
            patientlistview.ItemsSource = patientList;

        }

        private void RefreshPatientList()
        {
            var patient = connection.Table<Patient>().ToListAsync().Result;
            var patients = new List<Patient>();
            if (patient != null)
                patientlistview.ItemsSource = patients;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var editmenuItem = sender as Xamarin.Forms.MenuItem;

            if (editmenuItem != null)
            {
                var patient = editmenuItem.BindingContext as Patient;

                if (patient != null)
                {
                    await Navigation.PushAsync(new EditPatient(patient));
                }
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {

            var deletemenuItem = sender as Xamarin.Forms.MenuItem;

            if (deletemenuItem != null)
            {
                var patient = deletemenuItem.BindingContext as Patient;

                if (patient != null)
                {
                    await connection.DeleteAsync(patient);

                }
                RefreshPatientList();
            }
        }

        private async void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var patient = e.Item as Patient;

            await Navigation.PushAsync(new PatientDetailPage(patient));
        }
    }
}