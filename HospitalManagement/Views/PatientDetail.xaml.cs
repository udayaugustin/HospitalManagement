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
	public partial class PatientDetail : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Patient> patientList;
        Patient _selectedpatient;
        public PatientDetail (Patient patient)
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _selectedpatient = patient;
            GetData(patient);
        }

        private void GetData(Patient patient)
        {
            Name.Text = patient.Name;
            PhoneNo.Text = patient.MobileNo;
            Age.Text = Convert.ToString(patient.Age);
            Sex.Text = patient.Sex;
            Address.Text = patient.Address;
          
        }

        private async void Add_Appoinment(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Appointment());
        }

        private void Treatment(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new TreatmentDetailPage());
        }
    }
}