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
        public PatientDetailPage()
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
