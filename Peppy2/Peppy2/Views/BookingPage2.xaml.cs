using Peppy2.Helpers;
using Peppy2.Models;
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
	public partial class BookingPage2 : ContentPage
	{
		public BookingPage2 ()
		{
			InitializeComponent ();

            List<Payment> methods = new List<Payment>
            {
                 new Payment{id = 1, method="Cash Payment"},
                 new Payment{id = 2, method="Card payment"}
            };
            paymentpicker.ItemsSource = methods;

            List<Locations> location = new List<Locations>
            {
                new Locations{Id = 1, city="Ajah"},
                 new Locations{Id = 2, city="Banana Island"},
                  new Locations{Id = 3, city="Ikoyi"},
                   new Locations{Id = 4, city="Lekki"},
                    new Locations{Id = 5, city="Magodo"},
                     new Locations{Id = 6, city="Victoria Island"},
            };
            citypicker.ItemsSource = location;           
        }

        private async void nextBtn_Clicked(object sender, EventArgs e)
        {
            ScaleButton();
            try
            {
                if (citypicker.SelectedIndex == -1)
                {
                    await DisplayAlert("Error", "Please select your city", "ok");
                    return;
                }

                if (string.IsNullOrEmpty(streetentry.Text) || string.IsNullOrEmpty(housenumberentry.Text))
                {
                    await DisplayAlert("Error", "Please enter your house number and street number", "ok");
                    return;
                }

                if (string.IsNullOrWhiteSpace(detailseditor.Text))
                {
                    await DisplayAlert("Error", "Please enter a short descripton telling us how your cleaner will get into your house", "ok");
                    return;
                }

                if (paymentpicker.SelectedIndex == -1)
                {
                    await DisplayAlert("Error", "Choose a payment method", "ok");
                    return;
                }

                storeDetails();
                await Navigation.PushAsync(new FinalBooking());
            }
            catch (Exception)
            {
             await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }

        }

        void storeDetails()
        {
            BookingSettings.City = citypicker.Items[citypicker.SelectedIndex];
            BookingSettings.Street = streetentry.Text;
            BookingSettings.HomeNumber = housenumberentry.Text;
            BookingSettings.DirectionsOrLandmarks = directionsentry.Text;
            BookingSettings.IsHavePets = petsswitch.IsToggled;
            BookingSettings.ModeOfEntry = detailseditor.Text;
            BookingSettings.PaymentMethod = paymentpicker.Items[paymentpicker.SelectedIndex];
        }

        public async void ScaleButton()
        {
            await nextBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await nextBtn.ScaleTo(1, 50, Easing.BounceIn);
        }
    }
}