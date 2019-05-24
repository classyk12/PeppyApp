using Acr.UserDialogs;
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
	public partial class FindCleaner : ContentPage
	{
        public CleanerService service = new CleanerService();
        public List<CleanerModel> CleanerList;

		public FindCleaner ()
		{
			InitializeComponent ();

            CleanerList = new List<CleanerModel>();
            findBtn.Clicked += Getcleaners;
		}
        private async void Getcleaners(object sender, EventArgs e)
        {
            ScaleButton();
            if (CrossConnectivity.Current.IsConnected)
            {


                using (UserDialogs.Instance.Loading("Please Wait..", null, null, true, MaskType.Gradient))
                {
                    try
                    {
                        if (locationpicker.SelectedIndex == -1)
                        {
                            await DisplayAlert("Error..", "Please select your city", "ok");
                            return;
                        }

                        var loc = locationpicker.Items[locationpicker.SelectedIndex];
                        var data = await service.GetCleaners(loc);

                        if (data.Count == 0)
                        {
                            await DisplayAlert("Oops!", "There are no cleaners in your area at the moment,Check back later", "ok");
                        }
                        else
                        {
                            CleanerList = data;
                            await DisplayAlert("Awesome", "we found " + data.Count.ToString() + " cleaners in your area ", "Proceed");
                            await Navigation.PushAsync(new CleanersPage(CleanerList, loc));
                        }

                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
                    }

                }
            }
            else
            {
                await DisplayAlert("Connection Timeout", "Check your internet connection and try again", "ok");
            }
        }

        public async void ScaleButton()
        {
            await findBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await findBtn.ScaleTo(1, 50, Easing.BounceIn);
        }   


    }
}