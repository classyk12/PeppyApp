using Peppy2.APIServices;
using Peppy2.Helpers;
using Peppy2.Models;
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
	public partial class CleanerDetails : ContentPage
	{
        private string _phone;
        BookingService service = new BookingService();
		public  CleanerDetails (CleanerModel cleaner)
		{
			InitializeComponent ();
            Settings.CleanerId = cleaner.Id.ToString();
            Title = cleaner.Name;
            profilepic.Source = cleaner.FullImagePath;
            cleanernameLbl.Text = cleaner.Name.ToUpper();
            locationLbl.Text = cleaner.Location.ToUpper();
            phoneLbl.Text = cleaner.PhoneNumber.ToUpper();
            countactivity.IsEnabled = true;
            countactivity.IsRunning = true;
            countactivity.IsVisible = true;

            _phone = phoneLbl.Text;

            if(CrossConnectivity.Current.IsConnected)
            {
                CleanerServiceCount();
            }
            else
            {
                 DisplayAlert("Connection Timeout", "We could not get your Cleaner service count at the moment,Check your Internet Connection and Try again", "ok");
            }
           
                               
		}

        public async void CleanerServiceCount()
        {
            try
            {
                var data = await service.CleanerCount(Settings.CleanerId);
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
