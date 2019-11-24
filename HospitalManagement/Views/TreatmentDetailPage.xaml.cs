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
        Treatment treatment;

        public TreatmentDetailPage(int PatientId, Treatment treatment)
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();           
            patientId = PatientId;
            this.treatment = treatment;
            GetData(treatment);
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
            var t =new Treatment();
            t.Cheif_Complaint = cheifcomplaint;
            t.Patient_History = patienthistory;
            t.Diagnosis = diagnosis;
            t.Treatment_Plan = treatmentplan;
            t.PatientId = patientId;
           
            await connection.InsertAsync(t);
           
        }

        private void OpenAddAppointmentPage(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MasterDetailPage;
          //  mainPage.Detail = new NavigationPage(new Appointment());
        }
    }
}
