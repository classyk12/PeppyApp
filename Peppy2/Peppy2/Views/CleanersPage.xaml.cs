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
	
	public partial class CleanersPage : ContentPage
	{
		public CleanersPage (List<CleanerModel> cleaner, string location)
		{
			InitializeComponent ();
            Title = "All Cleaners in " + location;
            cleanerLV.ItemsSource = cleaner;
            
		}

        //private async void menuclick_Clicked(object sender, EventArgs e)
        //{          
        //    var menudata = sender as MenuItem;
        //    var data = menudata.CommandParameter as CleanerModel;           
        //    await Navigation.PushAsync(new CleanerDetails(data));
        //}

        private async void cleanerLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (cleanerLV.SelectedItem == null)
                {
                    return;
                }
                var data = cleanerLV.SelectedItem as CleanerModel;
                var cleanername = data.Name;
                var dialog = await DisplayActionSheet("You have selected " + data.Name.ToUpper() + " and you can not change your cleaner later, Continue?"
                      , "", "", "Yes", "View Cleaner Details", "Select another cleaner");
                if (dialog == "Yes")
                {
                    cleanerLV.SelectedItem = null;
                    await Navigation.PushAsync(new BookingPage(data));
                }
                else if (dialog == "View Cleaner Details")
                {
                    cleanerLV.SelectedItem = null;
                    await Navigation.PushAsync(new CleanerDetails(data));
                }
                else
                {
                    cleanerLV.SelectedItem = null;
                    return;
                }
            }
            catch (Exception)
            {
               await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
           
        }
    }
}