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
                new MenuItem{ Title = "Patient"},                             
                new MenuItem{ Title = "Report"},
                            
            };

            listView.ItemsSource = menuList;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            mainPage = Application.Current.MainPage as MasterDetailPage;
            var menu = e.Item as MenuItem;
            switch (menu.Title)
            {
                case "Patient":
                    mainPage.Detail = new NavigationPage(new AddPatient());
                break;
                    
                case "Report":
                    mainPage.Detail = new NavigationPage(new MainPage());
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
