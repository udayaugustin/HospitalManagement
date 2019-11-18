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
	public partial class LicenseViolation : ContentPage
	{
		public LicenseViolation ()
		{
			InitializeComponent ();
		}

        private void NaviagateToLogin(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}