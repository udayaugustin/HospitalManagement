using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalManagement.Model;
using SQLite;
using Xamarin.Forms;
using System.Linq;

namespace HospitalManagement.Views
{
    public partial class CashBook : ContentPage
    {
        private List<Patient> patientList;
        private SQLiteAsyncConnection connection;
        private Patient _selectedPatient;
        private List<PatientTransaction> patientTransactionList;
        private int totalIncome;
        private DateTime startDate;

        public CashBook()
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            TransactionTitle.Text = "Date: " + startDate.Date.ToString("dd-MMM-yy") + " To " + DateTime.Today.Date.ToString("dd-MMM-yy");

            GetData();
        }

        private async Task GetData()
        {
            patientList = new List<Patient>();
            patientList = await connection.Table<Patient>().ToListAsync();
            PatientListPicker.ItemsSource = patientList;
            PatientListPicker.ItemDisplayBinding = new Binding("Name");

            patientTransactionList = new List<PatientTransaction>();
            patientTransactionList  = await connection.Table<PatientTransaction>()
                .Where(p => p.Date >= startDate && p.Date <= DateTime.Today.Date)
                .OrderByDescending(p =>p.Id)
                .ToListAsync();
            patientTransctionListView.ItemsSource = null;
            patientTransctionListView.ItemsSource = patientTransactionList;

            totalIncome = patientTransactionList.Sum(p => p.PaidAmount);
            
            TotalIncome.Text = "Total Income Rs." + totalIncome;
        }

        private void AddPayment(object sender, EventArgs e)
        {
            var patientTransaction = new PatientTransaction
            {
                PatientId = _selectedPatient.Id,
                PatientName = _selectedPatient.Name,
                Date = DateTime.Now.Date,
                PaidAmount = Convert.ToInt32(PaidAmount.Text),
            };

            connection.InsertAsync(patientTransaction);

            PatientListPicker.SelectedItem = null;
            PaidAmount.Text = "";
        }

        private void PatientSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            _selectedPatient = (Patient)picker.SelectedItem;
        }
    }
}
