using Peppy2.APIServices;
using Peppy2.Helpers;
using Peppy2.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Peppy2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dashboard : ContentPage
	{
        public List<Items> items = new List<Items>();

        public  Dashboard ()
		{
			InitializeComponent ();
            var all = new List<Items>
            {
                new Items {Title = "First", Icon = "dash.jpg"},
                   new Items {Title = "First", Icon = "dash1.jpg"},
                      new Items {Title = "First", Icon = "dash2.jpg"},
                         new Items {Title = "First", Icon = "dash3.jpg"},
                            new Items {Title = "First", Icon = "dash4.jpg"},
            };


            feedbackactivity.IsEnabled = true;
            feedbackactivity.IsRunning = true;
            feedbackactivity.IsVisible = true;

            bookingsactivity.IsVisible = true;
            bookingsactivity.IsRunning = true;
            bookingsactivity.IsEnabled = true;

            cleanersactivity.IsEnabled = true;
            cleanersactivity.IsRunning = true;
            cleanersactivity.IsVisible = true;

            dateactivity.IsEnabled = true;
            dateactivity.IsRunning = true;
            dateactivity.IsVisible = true;


            FeedbackCount();
            BookingsCount();
            CleanerCount();
            GetLatestDate();
            cubeview.ItemsSource = all;



        }

        public async void BookingsCount()
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {

                    BookingService service = new BookingService();
                    var data = await service.GetBookingCount(Settings.UserId);
                    if (string.IsNullOrWhiteSpace(data))
                    {
                        TotalBookingLbl.Text = "N/A";
                        bookingsactivity.IsVisible = false;
                        bookingsactivity.IsRunning = false;
                        bookingsactivity.IsEnabled = false;
                        BookingFrameShake();
                    }
                    else
                    {
                        TotalBookingLbl.Text = data;
                        bookingsactivity.IsVisible = false;
                        bookingsactivity.IsRunning = false;
                        bookingsactivity.IsEnabled = false;
                        BookingFrameShake();
                    }
                }
                catch (Exception)
                {

                    await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
                }

            }
            else
            {
                await DisplayAlert("Connection Timeout", "Check your internet connection and try again", "ok");
            }




        }

        public async void CleanerCount()
        {
            try
            {
                BookingService service = new BookingService();
                var data = await service.GetCleanerCount(Settings.UserId);
                if (string.IsNullOrWhiteSpace(data))
                {
                    TotalCleanerLbl.Text = "0";
                    cleanersactivity.IsVisible = false;
                    cleanersactivity.IsRunning = false;
                    cleanersactivity.IsEnabled = false;
                    CleanerFrameShake();
                }
                else
                {
                    TotalCleanerLbl.Text = data;
                    cleanersactivity.IsVisible = false;
                    cleanersactivity.IsRunning = false;
                    cleanersactivity.IsEnabled = false;
                    CleanerFrameShake();
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

        public async void FeedbackCount()
        {
            try
            {
                FeedbackService service = new FeedbackService();
                var data = await service.GetUserFeedbackCount(Settings.UserId);
                if (string.IsNullOrWhiteSpace(data))
                {
                    TotalFeedbackLbl.Text = "0";
                    feedbackactivity.IsEnabled = false;
                    feedbackactivity.IsRunning = false;
                    feedbackactivity.IsVisible = false;
                    FeedbackFrameShake();
                }
                else
                {
                    TotalFeedbackLbl.Text = data;
                    feedbackactivity.IsEnabled = false;
                    feedbackactivity.IsRunning = false;
                    feedbackactivity.IsVisible = false;
                    FeedbackFrameShake();
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

        public async void GetLatestDate()
        {
            try
            {
                BookingService service = new BookingService();
                var data = await service.GetBookingDate(Settings.UserId);
                if (data == null)
                {
                    BookingDateLbl.Text = "N/A";
                    dateactivity.IsEnabled = false;
                    dateactivity.IsRunning = false;
                    dateactivity.IsVisible = false;
                    LastBookingFrame();
                }
                else
                {
                    BookingDateLbl.Text = data.DateCreated.ToLongDateString();
                    dateactivity.IsEnabled = false;
                    dateactivity.IsRunning = false;
                    dateactivity.IsVisible = false;
                    LastBookingFrame();
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

        public async void BookingFrameShake()
        {
            uint timeout = 50;
            await bookingsframe.TranslateTo(-15, 0, timeout);

            await bookingsframe.TranslateTo(15, 0, timeout);

            await bookingsframe.TranslateTo(-10, 0, timeout);

            await bookingsframe.TranslateTo(10, 0, timeout);

            await bookingsframe.TranslateTo(-5, 0, timeout);

            await bookingsframe.TranslateTo(5, 0, timeout);

            bookingsframe.TranslationX = 0;

        }

        public async void FeedbackFrameShake()
        {
  
          uint timeout = 50;
            await feedbackframe.TranslateTo(-15, 0, timeout);

            await feedbackframe.TranslateTo(15, 0, timeout);

            await feedbackframe.TranslateTo(-10, 0, timeout);

            await feedbackframe.TranslateTo(10, 0, timeout);

            await feedbackframe.TranslateTo(-5, 0, timeout);

            await feedbackframe.TranslateTo(5, 0, timeout);

            feedbackframe.TranslationX = 0;

        }

        public async void CleanerFrameShake()
        {
            uint timeout = 50;
            await cleanerframe.TranslateTo(-15, 0, timeout);

            await cleanerframe.TranslateTo(15, 0, timeout);

            await cleanerframe.TranslateTo(-10, 0, timeout);

            await cleanerframe.TranslateTo(10, 0, timeout);

            await cleanerframe.TranslateTo(-5, 0, timeout);

            await cleanerframe.TranslateTo(5, 0, timeout);

            cleanerframe.TranslationX = 0;

        }

        public async void LastBookingFrame()
        {

            uint timeout = 50;
            await lastbookingframe.TranslateTo(-15, 0, timeout);

            await lastbookingframe.TranslateTo(15, 0, timeout);

            await lastbookingframe.TranslateTo(-10, 0, timeout);

            await lastbookingframe.TranslateTo(10, 0, timeout);

            await lastbookingframe.TranslateTo(-5, 0, timeout);

            await lastbookingframe.TranslateTo(5, 0, timeout);

            lastbookingframe.TranslationX = 0;

        }


      

    }

    
}