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
        private SQLiteAsyncConnection connection;
        private List<Treatment> TreatmentList;
        private Patient patient;
        public PatientDetail (Patient patient)
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            this.patient = patient;
            GetData();            
        }

        private async void GetData()
        {
            Name.Text = patient.Name;
            PhoneNo.Text = patient.MobileNo;
            Age.Text = Convert.ToString(patient.Age);
            Sex.Text = patient.Sex;
            Address.Text = patient.Address;            
            TreatmentList = new List<Treatment>();
            TreatmentList = await connection.Table<Treatment>().Where(t => t.PatientId == patient.Id).ToListAsync();
            var counter = 1;
            foreach(var treatment in TreatmentList)
            {
                treatment.SerialNo = counter++;
            }

            treatmentlist.ItemsSource = TreatmentList;
        }        

        private void AddTreatment(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail =  new NavigationPage(new TreatmentDetailPage(patient.Id, new Treatment()));
        }

        private void Treatmentlist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var treatment = e.SelectedItem as Treatment;
            Navigation.PushAsync(new TreatmentDetailPage(patient.Id, treatment));
        }
    }
}