using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalManagement.Model;
using SQLite;
using Xamarin.Forms;
using System.Linq;

namespace HospitalManagement.Views
{
    public partial class ReportPage : TabbedPage
    {
        private DateTime fromDate;
        private DateTime toDate;
        private SQLiteAsyncConnection connection;
        private List<IncomeReportModel> incomeReportList;
        private List<ExpenseTransaction> expenseReportList;
        private int totalExpense;
        private int totalIncome;

        public  ReportPage()
        {
            InitializeComponent();

            fromDate = IncomeFromDate.Date;
            toDate = IncomeToDate.Date;
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            SetDatePickerDate();

            GetIncomeReport();
            GetExpenseReport();
        }

        private void SetDatePickerDate()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            IncomeFromDate.Date = ExpenseFromDate.Date = Reportfromdate.Date = startDate;
        }

        private async Task GetIncomeReport()
        {
            fromDate = IncomeFromDate.Date;
            toDate = IncomeToDate.Date;

            var patientList =  await connection.Table<Patient>().OrderByDescending(p => p.Id).ToListAsync();
            incomeReportList = new List<IncomeReportModel>();

            foreach (var patient in patientList)
            {
                var treatmentList = await connection.Table<Treatment>().Where(t => t.PatientId == patient.Id && t.TreatmentDate >= fromDate.Date && t.TreatmentDate <= toDate.Date).ToListAsync();
                var totalTreatmentCost = treatmentList.Sum(t => t.TreatmentCost);

                var patientTransaction = await connection.Table<PatientTransaction>().Where(t => t.PatientId == patient.Id).ToListAsync();
                var receivedAmount = patientTransaction.Sum(p => p.ReceivedAmount);

                var balance = totalTreatmentCost - receivedAmount;
                var incomeReport = new IncomeReportModel
                {
                    Name = patient.Name,
                    ReceivedAmount = receivedAmount,
                    BalanceAmount = balance
                };

                incomeReportList.Add(incomeReport);
            }

            incomeListview.ItemsSource = incomeReportList;
            totalIncome = incomeReportList.Sum(i => i.ReceivedAmount);
            TotalIncome.Text = "Total Income Rs." + totalIncome;
            TotalPending.Text = "Total Pending Rs." + incomeReportList.Sum(i => i.BalanceAmount);
        }

        private void GenerateIncomeReport(object sender, EventArgs e)
        {
            GetIncomeReport();
        }

        private void GenerateExpenseReport(object sender, EventArgs e)
        {
            GetExpenseReport();
        }

        private async Task GetExpenseReport()
        {
            fromDate = ExpenseFromDate.Date;
            toDate = ExpenseToDate.Date;
            expenseReportList = new List<ExpenseTransaction>(
                await connection.Table<ExpenseTransaction>()
                .Where(p => p.Date >= fromDate && p.Date <= toDate)
                .OrderByDescending(p => p.Id)
                .ToListAsync());
            expenseTransctionListView.ItemsSource = null;
            expenseTransctionListView.ItemsSource = expenseReportList;

            totalExpense = expenseReportList.Sum(e => e.PaidAmount);
            TotalExpense.Text = "Total Expense Rs." + totalExpense;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            listtitle.IsVisible = true;
            listvalue.IsVisible = true;
            GetReport();
        }

        private async Task GetReport()
        {
            fromDate = Reportfromdate.Date;
            toDate = Reporttodate.Date;
            int materialtotal;
            int medicinestotal;
            int renttotal;
            int electrycitytotal;
            int garbagetotal;
            int otherstotal;

            TotalIncomereport.Text = "Total Revinue Rs." + totalIncome;
            expenseReportList = new List<ExpenseTransaction>(await connection.Table<ExpenseTransaction>().Where(p => p.Date >= fromDate && p.Date <= toDate).ToListAsync());
            totalExpense = expenseReportList.Sum(p =>p.PaidAmount);
            TotalExpancereport.Text = "Total Expense Rs." + totalExpense;
            expenseReportList = await connection.Table<ExpenseTransaction>().Where(r => r.Date >= fromDate && r.Date <= toDate && r.Type == "Materials").ToListAsync();
            materialtotal = expenseReportList.Sum(r => r.PaidAmount);
            MaterialAmount.Text = Convert.ToString(materialtotal);
            expenseReportList = await connection.Table<ExpenseTransaction>().Where(r => r.Date >= fromDate && r.Date <= toDate && r.Type == "Medicines").ToListAsync();
            medicinestotal = expenseReportList.Sum(r => r.PaidAmount);
            MedicinesAmount.Text = Convert.ToString(medicinestotal);
            expenseReportList = await connection.Table<ExpenseTransaction>().Where(r => r.Date >= fromDate && r.Date <= toDate && r.Type == "Rent").ToListAsync();
            renttotal = expenseReportList.Sum(r => r.PaidAmount);
            RentAmount.Text = Convert.ToString(renttotal);
            expenseReportList = await connection.Table<ExpenseTransaction>().Where(r => r.Date >= fromDate && r.Date <= toDate && r.Type == "Electrycity").ToListAsync();
            electrycitytotal = expenseReportList.Sum(r => r.PaidAmount);
            ElectrycityAmount.Text = Convert.ToString(electrycitytotal);
            expenseReportList = await connection.Table<ExpenseTransaction>().Where(r => r.Date >= fromDate && r.Date <= toDate && r.Type == "Garbage").ToListAsync();
            garbagetotal = expenseReportList.Sum(r => r.PaidAmount);
            Garbage.Text = Convert.ToString(garbagetotal);
            expenseReportList = await connection.Table<ExpenseTransaction>().Where(r => r.Date >= fromDate && r.Date <= toDate && r.Type == "Others").ToListAsync();
            otherstotal = expenseReportList.Sum(r => r.PaidAmount);
            OthersAmount.Text = Convert.ToString(otherstotal);

            //TotalIncomereport.Text = "Total Income Rs." + incomeReportList.Sum(i => i.ReceivedAmount);

        }
             
    }

    public class IncomeReportModel
    {
        public string Name { get; set; }

        public int ReceivedAmount { get; set; }

        public int BalanceAmount { get; set; }
    }
}
