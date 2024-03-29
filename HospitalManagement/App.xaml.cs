﻿using System;
using HospitalManagement.Model;
using HospitalManagement.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HospitalManagement
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();            
            MainPage = new MasterPage();
            (MainPage as MasterDetailPage).Detail = new NavigationPage(new PatientList());

            //MainPage = new NavigationPage(new PatientDetailPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<Patient>();
            connection.CreateTableAsync<Treatment>();
            connection.CreateTableAsync<Appoinment>();
            connection.CreateTableAsync<PatientPaymentOverview>();
            connection.CreateTableAsync<PatientTransaction>();
            connection.CreateTableAsync<ExpenseTransaction>();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
