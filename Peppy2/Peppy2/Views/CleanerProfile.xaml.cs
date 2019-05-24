using Peppy2.APIServices;
using Peppy2.Helpers;
using Plugin.Connectivity;
using Plugin.Messaging;
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
	public partial class CleanerProfile : ContentPage
	{
        
        private string _phone;
        public CleanerProfile ()
		{
			InitializeComponent ();
            profilepic.Source = CleanerSettings.Path;
            cleanernameLbl.Text = CleanerSettings.CleanerName;
            locationLbl.Text = CleanerSettings.Location;
            phoneLbl.Text = CleanerSettings.PhoneNumber;

            _phone = phoneLbl.Text;

            countactivity.IsEnabled = true;
            countactivity.IsRunning = true;
            countactivity.IsVisible = true;

            if(CrossConnectivity.Current.IsConnected)
            {
                CleanerServiceCount();
            }
            else
            {
                DisplayAlert("Connection Timeout", "Check your internet and try again", "Ok");
            }
            
        }

        public async void CleanerServiceCount()
        {
            try
            {
                BookingService service = new BookingService();
                var data = await service.CleanerCount(CleanerSettings.CleanerId.ToString());
                serviceLbl.Text = data + " " + "Completed Cleans";
                countactivity.IsEnabled = false;
                countactivity.IsRunning = false;
                countactivity.IsVisible = false;
            }
            catch (Exception)
            {
                await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
            }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //tap event for call
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(_phone);
        }
    }
    }
