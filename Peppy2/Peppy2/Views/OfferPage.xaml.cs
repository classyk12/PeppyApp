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
	public partial class OfferPage : ContentPage
	{
        public List<OfferModel> OfferList;
        public OfferService offer = new OfferService();
    

		public OfferPage ()
		{
			InitializeComponent ();
            OfferList = new List<OfferModel>();


            if(CrossConnectivity.Current.IsConnected)
            {
                GetAllOffers();
            }
            else
            {
                DisplayAlert("Connection Timeout", "Check your internet connection and try again", "ok");
            }
            
            //subscribes to OfferLVItemSelected event
            OfferLV.ItemSelected += OfferLVItemSelected;
            activity.IsEnabled = true;
            activity.IsVisible = true;
            activity.IsRunning = true;           
        }
     
        private async void GetAllOffers()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    var data = await offer.AllOffers();
                    if (data.Count == 0)
                    {
                        await DisplayAlert("We are deeply sorry", "There are no offers at the moment, check back later", "ok");
                    }
                    else
                    {
                        OfferList = data;
                        OfferLV.ItemsSource = OfferList;
                        activity.IsRunning = false;
                        activity.IsEnabled = false;
                        activity.IsVisible = false;
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
                }
            }
          
        }

        private async void OfferLVItemSelected(object sender, EventArgs e)
        {
            if(OfferLV.SelectedItem == null)
            {
                return;
            }
            var data = OfferLV.SelectedItem as OfferModel;
            OfferLV.SelectedItem = null;
            await Navigation.PushAsync(new OfferDetails(data));

        }

      
    }
}