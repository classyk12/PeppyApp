using Acr.UserDialogs;
using FormsControls.Base;
using Peppy2.APIServices;
using Peppy2.Controllers;
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
	public partial class NewFeedbackPage : AnimationPage
	{
       public int SelectedRating { get; set; }
        public NewFeedbackPage ()
		{
			InitializeComponent ();
            List<Ratings> rating = new List<Ratings>
            {
                new Ratings {Id = 1, Rating = 5, value =  "Excellent" },
                 new Ratings {Id = 2, Rating = 4, value =  "Very Good" },
                  new Ratings {Id = 3, Rating = 3, value =  "Good" },
                   new Ratings {Id = 4, Rating = 2, value =  "Bad" },
                    new Ratings {Id = 5, Rating = 1, value =  "Worst" }
        };
            servicepicker.ItemsSource = rating;
            FeedbackBtn.Clicked += SubmitFeedback;
           
            
            
        }

        private async void SubmitFeedback(object sender, EventArgs e)
        {
            scaleButton();
            if (CrossConnectivity.Current.IsConnected)
            {


                using (UserDialogs.Instance.Loading("Please Wait..", null, null, true, MaskType.Gradient))
                {
                    try
                    {
                        FeedbackService service = new FeedbackService();
                        var data = new FeedbackModel
                        {
                            UserId = Settings.UserId,
                            DateAdded = DateTime.Now.Date,
                            username = Settings.UserName,
                            Title = TitleEntry.Text,
                            Body = BodyEditor.Text,
                            Rating = SelectedRating
                        };

                        if (string.IsNullOrEmpty(TitleEntry.Text) || string.IsNullOrEmpty(BodyEditor.Text) || servicepicker.SelectedIndex == -1)
                        {
                            await DisplayAlert("Error..!!", "Fields cannot be empty", "ok");
                            return;
                        }

                        bool response = await service.SendFeedback(data);
                        if (!response)
                        {
                            await DisplayAlert("Sorry", "We could not send your feedback at the moment, check and try again", "ok");
                            return;
                        }
                        await DisplayAlert("Successful...", "Your Feedback was well recieved, Thank you", "ok");
                      
                        await Navigation.PopAsync();

                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");

                    }
                }
            }
            else
            {
                await DisplayAlert("Connection Timeout", "Check your connection and try again", "ok");
            }
        }

        private void servicepicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rating = (Ratings)servicepicker.SelectedItem;
            SelectedRating = rating.Rating;
        }

        public async void scaleButton()
        {
            await FeedbackBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await FeedbackBtn.ScaleTo(1, 50, Easing.BounceIn);
        }
    }
}