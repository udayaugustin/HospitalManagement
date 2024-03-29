﻿using HospitalManagement.Model;
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
	public partial class EditPatient : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Patient> patientList;
        Patient _selectedpatient;
        public EditPatient (Patient patient)
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _selectedpatient = patient;
            GetData(patient);
        }

        private async void  GetData(Patient patient)
        {
            Name.Text = patient.Name;
            PhoneNo.Text = patient.MobileNo;
            Age.Text = Convert.ToString(patient.Age);
            Sex.SelectedItem = patient.Sex;
            Address.Text = patient.Address;
        }

        private async void Update(object sender, EventArgs e)
        {
            _selectedpatient.Name = Name.Text;
            _selectedpatient.MobileNo = PhoneNo.Text;
            _selectedpatient.Age = Convert.ToInt32(Age.Text);
            _selectedpatient.Sex = Convert.ToString(Sex.SelectedItem);
            _selectedpatient.Address = Address.Text;
            await connection.UpdateAsync(_selectedpatient);
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new PatientList());
        }

    }
}