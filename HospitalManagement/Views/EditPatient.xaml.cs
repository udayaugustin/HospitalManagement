using HospitalManagement.Model;
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
		public EditPatient (Patient patient)
		{
			InitializeComponent ();
		}
	}
}