﻿using HospitalManagement.Model;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HospitalManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TreatmentDetailPage : TabbedPage
    {
        private SQLiteAsyncConnection connection;
        private int  patientId;
        private Treatment treatment;
        private int treatmentId;
        private ObservableCollection<Appoinment> appoinmentList;        

        public TreatmentDetailPage(int Id, Treatment treatment)
        {
            InitializeComponent();
            BindingContext = this;

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            this.treatment = treatment;
            patientId = Id;

            if(treatment.Id != 0)
            {
                treatmentId = treatment.Id;
                GetData(treatment);
                GetAppointments();
            }                            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetAppointments();
        }

        private void GetData(Treatment treatment)
        {
            CheifComplaint.Text = treatment.Cheif_Complaint;
            PatientHistory.Text = treatment.Patient_History;
            Diagnosis.Text = treatment.Diagnosis;
            TreatmentPlan.Text = treatment.TreatmentPlan;
            TreatmentCost.Text = treatment.TreatmentCost.ToString();
        }

        private async Task GetAppointments()
        {
            if(treatmentId != 0)
            {
                var list = await connection.Table<Appoinment>().Where(a => a.TreatmentId == treatmentId).ToListAsync();
                appoinmentList = new ObservableCollection<Appoinment>(list);
                listView.ItemsSource = null;
                listView.ItemsSource = appoinmentList;
            }                
        }

        private async void Save(object sender, EventArgs e)
        {
            var cheifcomplaint = CheifComplaint.Text;
            var patienthistory = PatientHistory.Text;
            var diagnosis = Diagnosis.Text;
            var treatmentplan = TreatmentPlan.Text;
            var treatmentCost = TreatmentCost.Text;

            treatment.Cheif_Complaint = cheifcomplaint;
            treatment.Patient_History = patienthistory;
            treatment.Diagnosis = diagnosis;
            treatment.TreatmentPlan = treatmentplan;
            treatment.PatientId = patientId;
            treatment.TreatmentCost = Convert.ToInt32(treatmentCost);
            
            var result = await connection.InsertAsync(treatment);
            treatmentId = result;
        }

        private void OpenAddAppointmentPage(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail.Navigation.PushModalAsync(new AddAppointment(treatmentId));
        }

        private void UpdateAppointment(object sender, EventArgs e)
        {
            var appointment = (sender as Button).BindingContext;
            connection.UpdateAsync(appointment);
        }
    }
}
