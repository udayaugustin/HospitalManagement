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
        List<Treatment> TreatmentList;
        Treatment _selectedtreatment;

        public TreatmentDetailPage(int Id)
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _selectedtreatment = new Treatment();
         //   treatment = connection.Table<Treatment>().Where(t => t.PatientId == _selectedtreatment.Id).ToString();
            patientId = Id;
         //   GetData(treatment);
        }

        private void GetData(Treatment treatment)
        {
            CheifComplaint.Text = treatment.Cheif_Complaint;
            PatientHistory.Text = treatment.Patient_History;
            Diagnosis.Text = treatment.Diagnosis;
            TreatmentPlan.Text = treatment.Treatment_Plan;
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
          //  mainPage.Detail = new NavigationPage(new Appointment());
        }
    }
}
