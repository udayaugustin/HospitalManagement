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
    public partial class PatientDetailPage : TabbedPage
    {
        SQLiteAsyncConnection connection;
        List<Patient> patientList;
        Patient _selectedpatient;
        public PatientDetailPage(Patient patient)
        {
            InitializeComponent();
        }

        private void Save(object sender, EventArgs e)
        {
            
        }

        private void OpenAddAppointmentPage(object sender, EventArgs e)
        {

        }
    }
}
