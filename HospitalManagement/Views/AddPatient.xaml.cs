using HospitalManagement.Model;
using SQLite;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HospitalManagement.Views
{
    public partial class AddPatient : ContentPage
    {
        SQLiteAsyncConnection connection;

        public AddPatient()
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        private async void Save(object sender, EventArgs e)
        {
            var name = Name.Text;
            var age = Convert.ToInt32(Age.Text);
            var sex = Sex.ItemsSource;
            var mobileNo = MobileNo.Text;
            var address = Address.Text;


            var patient = new Patient
            {
                Name = name,
                Age = Convert.ToInt32(age),
                Sex = Convert.ToString(sex),
                MobileNo = mobileNo,
                Address = address,
            };
            await connection.InsertAsync(patient);
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new PatientList());
        }
    }
}
