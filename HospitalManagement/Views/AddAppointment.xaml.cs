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
	public partial class AddAppointment : ContentPage
	{
        SQLiteAsyncConnection connection;

        public AddAppointment ()
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        }

        private async void Save(object sender, EventArgs e)
        {
            var date = datepicker.Date;
            var time = timepicker.Time;
            var treatment_plan = treatmentplan.Text;
            var treatment_done = treatmentdone.Text;

            var appointment = new Appoinment()
            {
                Date = date,
                Time = Convert.ToDateTime(time),
                TreatmentPlan = treatment_plan,
                TreatmentDone = treatment_done

            };
            await connection.InsertAsync(appointment);

        }
    }
}