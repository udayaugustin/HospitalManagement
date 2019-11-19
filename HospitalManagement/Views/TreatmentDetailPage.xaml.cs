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
    public partial class TreatmentDetailPage : TabbedPage
    {
        SQLiteAsyncConnection connection;
        int  patientId;

        public TreatmentDetailPage(int Id)
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            patientId = Id;
        }
        private async void Save(object sender, EventArgs e)
        {
            var cheifcomplaint = CheifComplaint.Text;
            var patienthistory = PatientHistory.Text;
            var diagnosis = Diagnosis.Text;
            var treatmentplan = TreatmentPlan.Text;

            var treatment = new Treatment
            {
                Cheif_Complaint = cheifcomplaint,
                Patient_History = patienthistory,
                Diagnosis = diagnosis,
                Treatment_Plan = treatmentplan,
                PatientId = patientId,
            };
            await connection.InsertAsync(treatment);
           
        }

        private void OpenAddAppointmentPage(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new Appointment());
        }
    }
}
