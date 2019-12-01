using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalManagement.Model;
using SQLite;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace HospitalManagement.Views
{
    public partial class CashBook : TabbedPage
    {
        private List<Patient> patientList;
        private SQLiteAsyncConnection connection;
        private Patient _selectedPatient;
        private ObservableCollection<PatientTransaction> patientTransactionList;
        private ObservableCollection<ExpenseTransaction> expenseTransactionList;
        private int totalIncome;
        private int totalExpense;
        private DateTime startDate;
        public CashBook()
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ExpenseTransactionTitle.Text = ReceivedTransactionTitle.Text = "Date: " + startDate.Date.ToString("dd-MMM-yy") + " To " + DateTime.Today.Date.ToString("dd-MMM-yy");
            type.ItemsSource = ExpenseType.ExpenseTypeList();
            
            GetPatientList();
            GetPatientTransactionList();
            GetExpenseTransactionList();
        }

        private async Task GetPatientList()
        {
            patientList = new List<Patient>();
            patientList = await connection.Table<Patient>().ToListAsync();
            PatientListPicker.ItemsSource = patientList;
            PatientListPicker.ItemDisplayBinding = new Binding("Name");                                   
        }

        private async void UpdateReceivedAmount(object sender, EventArgs e)
        {
            var patientTransaction = new PatientTransaction
            {
                PatientId = _selectedPatient.Id,
                PatientName = _selectedPatient.Name,
                Date = ReceivedDate.Date,
                ReceivedAmount = Convert.ToInt32(ReceivedAmount.Text),
            };
            await connection.InsertAsync(patientTransaction);

            patientTransactionList.Insert(0, patientTransaction);
            totalIncome += Convert.ToInt32(ReceivedAmount.Text);
            UpdateTotalIncomeLabel();

            PatientListPicker.SelectedItem = null;
            ReceivedAmount.Text = "";
        }

        private void PatientSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            _selectedPatient = (Patient)picker.SelectedItem;
        }

        private async void UpdatePaidAmount(object sender, EventArgs e)
        {
            var expenseTransaction = new ExpenseTransaction
            {
                Date = PaidDate.Date,
                Name = Name.Text,
                Type = Convert.ToString(type.SelectedItem),
                PaidAmount = Convert.ToInt32(PaidAmount.Text)
            };
            
            await connection.InsertAsync(expenseTransaction);
            expenseTransactionList.Insert(0, expenseTransaction);

            totalExpense += Convert.ToInt32(PaidAmount.Text);
            UpdateTotalExpanseLabel();

            Name.Text = "";
            PaidAmount.Text = "";            
        }
        private async Task GetPatientTransactionList()
        {
            patientTransactionList = new ObservableCollection<PatientTransaction>(await connection.Table<PatientTransaction>()
                .Where(p => p.Date >= startDate && p.Date <= DateTime.Today.Date)
                .OrderByDescending(p => p.Id)
                .ToListAsync());
            patientTransctionListView.ItemsSource = null;
            patientTransctionListView.ItemsSource = patientTransactionList;

            totalIncome = patientTransactionList.Sum(p => p.ReceivedAmount);
            UpdateTotalIncomeLabel();
        }

        private async Task GetExpenseTransactionList()
        {
            expenseTransactionList = new ObservableCollection<ExpenseTransaction>(
                await connection.Table<ExpenseTransaction>()
                .Where(p => p.Date >= startDate && p.Date <= DateTime.Today.Date)
                .OrderByDescending(p => p.Id)
                .ToListAsync());
            expenseTransctionListView.ItemsSource = null;
            expenseTransctionListView.ItemsSource = expenseTransactionList;

            totalExpense = expenseTransactionList.Sum(e => e.PaidAmount);
            UpdateTotalExpanseLabel();
        }

        private void UpdateTotalIncomeLabel()
        {
            TotalIncome.Text = "Total Income Rs." + totalIncome;
        }
        
        private void UpdateTotalExpanseLabel()
        {
            TotalExpense.Text = "Total Expense Rs." + totalExpense;
        }
    }
}
