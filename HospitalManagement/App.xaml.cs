using System;
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
            (MainPage as MasterDetailPage).Detail = new NavigationPage(new PatientDetailPage());

            //MainPage = new NavigationPage(new PatientDetailPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<Booking>();
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
