using Acr.UserDialogs;
using Peppy2.APIServices;
using Peppy2.Helpers;
using Peppy2.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Peppy2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyBookings : ContentPage
	{
        public List<BookingsModel> mybookings;
        public List<BookingsModel> truebookings;
        public List<BookingsModel> falsebookings;
        public MyBookings ()
		{
			InitializeComponent ();
            mybookings = new List<BookingsModel>();
            truebookings = new List<BookingsModel>();
            falsebookings = new List<BookingsModel>();
            SortedBtn.IsVisible = false;

            if (CrossConnectivity.Current.IsConnected)
            {
                GetBookings();
            }
            else
            {
                DisplayAlert("Connection Timeout", "Check your internet connection and try again", "ok");
            }
           
            bookingactivity.IsEnabled = true;
            bookingactivity.IsRunning = true;
            bookingactivity.IsVisible = true;

            trueactivity.IsEnabled = true;
            trueactivity.IsRunning = true;
            trueactivity.IsVisible = true;

            falseactivity.IsEnabled = true;
            falseactivity.IsRunning = true;
            falseactivity.IsVisible = true;


           


        }

        private void scheduleBtn_Clicked(object sender, EventArgs e)
        {
            ScaleButton();
            Navigation.PushAsync(new FindCleaner());
        }

        public async void GetBookings()
        {
            try
            {
                BookingService service = new BookingService();
                var data = await service.GetBookingByUserId(Settings.UserId);
                if (data.Count == 0)
                {
                    NoBookingCV.IsVisible = true;
                    BookingSL.IsVisible = false;
                    SortedCV.IsVisible = false;
                }
                else
                {
                    NoBookingCV.IsVisible = false;
                    SortedCV.IsVisible = false;
                    BookingSL.IsVisible = true;
                    mybookings = data;
                    bookingLV.ItemsSource = mybookings;
                    bookingactivity.IsEnabled = false;
                    bookingactivity.IsRunning = false;
                    bookingactivity.IsVisible = false;
                    SortedBtn.IsVisible = true;


                }
            }
            catch (Exception)
            {

               await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

    

        private async void bookingLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (bookingLV.SelectedItem == null)
            {
                return;
            }
              
          var dialog =  await DisplayActionSheet("What do you want to do with this booking?", "", "", "Update Booking", "Mark as Complete","View Booking Details","Cancel Booking","Cancel");
            if (dialog == "Update Booking")
            {
                var select = bookingLV.SelectedItem as BookingsModel;
                bookingLV.SelectedItem = null;
                await Navigation.PushAsync(new UpdateBookingPage(select));
            }
            else if (dialog == "Mark as Complete")
            {
                var select = bookingLV.SelectedItem as BookingsModel;
                bookingLV.SelectedItem = null;
                await Navigation.PushAsync(new UpdateBookingPage2(select));
            }
            else if (dialog == "View Booking Details")
            {
                var select = bookingLV.SelectedItem as BookingsModel;
                bookingLV.SelectedItem = null;
                await Navigation.PushAsync(new BookingDetailsPage(select));
            }
            else if (dialog == "Cancel Booking")
            {
                using (UserDialogs.Instance.Loading("Please Wait..", null, null, true, MaskType.Gradient))
                {
                    var select = bookingLV.SelectedItem as BookingsModel;
                    bookingLV.SelectedItem = null;
                    var ask = await DisplayActionSheet("Are you sure you want to delete cancel and delete this booking?", "", "", "Yes", "No");
                    if (ask == "Yes")
                    {
                        BookingService service = new BookingService();
                        var delete = await service.DeleteBooking(select.BookingId);
                        if (!delete)
                        {
                            await DisplayAlert("Error..", "We could not delete your booking", "ok");
                        }
                        await DisplayAlert("Succesful..", "Booking have been cancelled and deleted successfully", "Ok");
                        GetBookings();

                    }
                }
            }

            else
            {
                bookingLV.SelectedItem = null;
                return;
            
            }
        }

        private  void bookingLV_Refreshing(object sender, EventArgs e)
        {
             GetBookings();
            bookingLV.EndRefresh();
        }

        private void SortedClicked(object sender, EventArgs e )
        {
            ScaleButton3();
            SortedCV.IsVisible = true;
            BookingSL.IsVisible = false;
            NoBookingCV.IsVisible = false;
            CompletedBooking();
            PendingBookings();
        }

        private void UnsortedClicked(object sender, EventArgs e)
        {
            ScaleButton2();
            BookingSL.IsVisible = true;
            SortedCV.IsVisible = false;
            NoBookingCV.IsVisible = false;
           
        }

        public async void CompletedBooking()
        {
            try
            {
                BookingService service = new BookingService();
                var data = await service.GetCompletedBookings(Settings.UserId);
                if (data.Count == 0)
                {
                    CompletedLV.IsVisible = false;
                    SortedLvl.Text = "You have no completed bookings";
                    trueactivity.IsEnabled = false;
                    trueactivity.IsRunning = false;
                    trueactivity.IsVisible = false;
                 
                }
                else
                {

                    SortedLvl.Text = "Your Completed Booking(s)";
                    truebookings = data;
                    CompletedLV.ItemsSource = truebookings;
                    trueactivity.IsEnabled = false;
                    trueactivity.IsRunning = false;
                    trueactivity.IsVisible = false;

                }
            }
            catch (Exception)
            {
                await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

        public async void PendingBookings()
        {
            try
            {
                BookingService service = new BookingService();
                var data = await service.GetUnCompletedBookings(Settings.UserId);
                if (data.Count == 0)
                {
                    UnCompletedLV.IsVisible = false;
                    UnsortedLbl.Text = "You have no pending bookings";
                    falseactivity.IsEnabled = false;
                    falseactivity.IsRunning = false;
                    falseactivity.IsVisible = false;

                }
                else
                {

                    UnsortedLbl.Text = "Your Pending Booking(s)";
                    falsebookings = data;
                    UnCompletedLV.ItemsSource = falsebookings;
                    falseactivity.IsEnabled = false;
                    falseactivity.IsRunning = false;
                    falseactivity.IsVisible = false;

                }
            }
            catch (Exception)
            {

               await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

        private void CompletedLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = (ListView)sender;
            select.SelectedItem = null;
        }

        private void UnCompletedLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = (ListView)sender;
            select.SelectedItem = null;
        }

        public async void ScaleButton()
        {
            await scheduleBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await scheduleBtn.ScaleTo(1, 50, Easing.BounceIn);
        }

        public async void ScaleButton2()
        {
            await UnSortBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await UnSortBtn.ScaleTo(1, 50, Easing.BounceIn);
        }

        public async void ScaleButton3()
        {
            await SortedBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await SortedBtn.ScaleTo(1, 50, Easing.BounceIn);
        }
    }
           
        }
    
