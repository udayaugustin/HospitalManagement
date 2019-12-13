using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HospitalManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingListTappedPage : TabbedPage
    {
        SQLiteAsyncConnection connection;
        ObservableCollection<Booking> bookingList;

        public BookingListTappedPage(string pageTitle)
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            GetData(pageTitle);

            //Set active page
            SetActivePage(pageTitle);

            this.CurrentPageChanged += BookingListTappedPage_CurrentPageChanged;
        }

        private async void GetData(string pageTitle)
        {
            var allBookings = new List<Booking>();
            allBookings = await connection.Table<Booking>().Where(b => b.IsCompleted == false)
                .OrderByDescending(b => b.CheckinDate)
                .ToListAsync();

            bookingList = new ObservableCollection<Booking>(allBookings);

            //Initiliase Today Listview
            UpdateBookingList();
        }

        private void BookingListTappedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            UpdateBookingList();
        }

        private void SetActivePage(string pageTitle)
        {
            if (pageTitle == "Today")
            {
                CurrentPage = Children[0];
            }
            else if (pageTitle == "Future")
            {
                CurrentPage = Children[1];
            }
            else
            {
                CurrentPage = Children[2];
            }
        }

        private void UpdateBookingList()
        {
            if (CurrentPage.Title == "Today")
            {
                DisplayTodayBookings();
            }
            else if (CurrentPage.Title == "Future")
            {
                DisplayFutureBookings();
            }
            else
            {
                DisplayAllBookings();
            }
        }

        private void DisplayTodayBookings()
        {
            var items = bookingList.Where(b => b.CheckinDate.Date == DateTime.Today.Date)
                .OrderByDescending(b => b.Id)
                .ToList();

            TodayListview.ItemsSource = items;

            TodayNoRecordLabel.IsVisible = !(items.Count > 0);
        }

        private void DisplayFutureBookings()
        {
            var items = bookingList.Where(b => b.CheckinDate.Date > DateTime.Today.Date)
                .OrderByDescending(b => b.CheckinDate)
                .ToList();
            FutureListview.ItemsSource = items;

            FutureNoRecordLabel.IsVisible = !(items.Count > 0);
        }

        private void DisplayAllBookings()
        {
            AllListview.ItemsSource = bookingList;

            AllNoRecordLabel.IsVisible = !(bookingList.Count > 0);
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var editmenuItem = sender as Xamarin.Forms.MenuItem;

            if (editmenuItem != null)
            {
                var booking = editmenuItem.BindingContext as Booking;

                if (booking != null)
                {
                    await Navigation.PushAsync(new MainPage());
                }
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var deletemenuItem = sender as Xamarin.Forms.MenuItem;

            if (deletemenuItem != null)
            {
                var booking = deletemenuItem.BindingContext as Booking;

                if (booking != null)
                {
                    await connection.DeleteAsync(booking);
                    bookingList.Remove(booking);
                    UpdateBookingList();
                }
            }
        }

        private async void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var booking = e.Item as Booking;

            await Navigation.PushAsync(new MainPage());
        }
    }
}
