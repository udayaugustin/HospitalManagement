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
    public partial class MasterPage : MasterDetailPage
    {
        MasterDetailPage mainPage;
        public MasterPage()
        {
            InitializeComponent();

            SetupMenu();            
        }

        public void SetupMenu()
        {
            var menuList = new List<MenuItem>
            {
                new MenuItem{ Title = "Patient List", Image = "component.png"},
                new MenuItem{ Title = "Cash Book", Image = "money.png" },
                new MenuItem{ Title = "Report", Image = "todo.png"},
                new MenuItem{ Title= "Final Report", Image = "todo.png"},
                new MenuItem{ Title= "Signout", Image = "signout.png"}
            };

            listView.ItemsSource = menuList;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            mainPage = Application.Current.MainPage as MasterDetailPage;
            var menu = e.Item as MenuItem;
            switch (menu.Title)
            {
                case "Patient List":
                    mainPage.Detail = new NavigationPage(new PatientList());
                    break;

                case "Cash Book":
                    mainPage.Detail = new NavigationPage(new CashBook());
                    break;

                case "Report":
                    mainPage.Detail = new NavigationPage(new ReportPage());
                    break;
                case "Final Report":
                    mainPage.Detail = new NavigationPage(new FinalReport());
                    break;

                case "Signout":
                    //  mainPage.Detail = new NavigationPage(new AddPatient());
                    break;

            }

            mainPage.IsPresented = false;
        }

        private void Synch(object sender, EventArgs e)
        {
            mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new SyncData());
        }
    }
}
