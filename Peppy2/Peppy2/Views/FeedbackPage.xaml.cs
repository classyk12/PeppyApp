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
    public partial class FeedbackPage : ContentPage
    {
        
        
        
        public FeedbackPage()
        {
            InitializeComponent();       
           

            ratingactivity.IsVisible = true;
            ratingactivity.IsEnabled = true;
            ratingactivity.IsRunning = true;

            feedbaclactivity.IsEnabled = true;
            feedbaclactivity.IsRunning = true;
            feedbaclactivity.IsVisible =true;

            useractivity.IsVisible = true;
            useractivity.IsRunning = true;
            useractivity.IsEnabled = true;


          
                //GetAllFeedbacks();
                GetMeanFeedback();
                GetFeedbackCount();
                GetUserCount();
           
          
           
        }
        
        public async void GetMeanFeedback()
        {
          if(  CrossConnectivity.Current.IsConnected)
                {

            
            try
            {
                FeedbackService service = new FeedbackService();
                var data = await service.MeanFeedback();
                if (string.IsNullOrEmpty(data))
                {
                    RatingLbl.Text = "0";
                    ratingactivity.IsVisible = false;
                    ratingactivity.IsEnabled = false;
                    ratingactivity.IsEnabled = false;
                }
                else
                {
                    RatingLbl.Text = data;
                    ratingactivity.IsVisible = false;
                    ratingactivity.IsEnabled = false;
                    ratingactivity.IsEnabled = false;
                    scaleLabel();
                }
            }
            catch (Exception)
            {
               await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
               
            }
            else
            {
                await DisplayAlert("Connection Timeout!", "Check your internet connection and try again", "ok");
            }

            

            
        }

        public async void GetFeedbackCount()
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                try
                {
                    FeedbackService service = new FeedbackService();
                    var data = await service.FeedbackCount();
                    if (string.IsNullOrEmpty(data))
                    {
                        FeedbackNoLbl.Text = "0";
                        feedbaclactivity.IsEnabled = false;
                        feedbaclactivity.IsRunning = false;
                        feedbaclactivity.IsVisible = false;

                    }
                    else
                    {
                        FeedbackNoLbl.Text = data;
                        feedbaclactivity.IsEnabled = false;
                        feedbaclactivity.IsRunning = false;
                        feedbaclactivity.IsVisible = false;
                        scaleLabel2();


                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
                }
            }
            else
            {
                await DisplayAlert("Connection Timeout!", "Check your internet connection and try again", "ok");
            }
        }

        public async void GetUserCount()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    FeedbackService service = new FeedbackService();
                    var data = await service.UserCount();
                    if (string.IsNullOrEmpty(data))
                    {
                        TotaluserLbl.Text = "0";
                        useractivity.IsVisible = false;
                        useractivity.IsRunning = false;
                        useractivity.IsEnabled = false;
                    }
                    else
                    {
                        TotaluserLbl.Text = data;
                        useractivity.IsVisible = false;
                        useractivity.IsRunning = false;
                        useractivity.IsEnabled = false;
                        scaleLabel3();
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
                }
            }
            else
            {
                await DisplayAlert("Connection Timeout!", "Check your internet connection and try again", "ok");
            }
        }

        private void FeedbackLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var select = (ListView)sender;
            select.SelectedItem = null;
        }

        private async void LabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewFeedbackPage());
        }

        private void feedbackLV_Refreshing(object sender, EventArgs e)
        {
            //GetAllFeedbacks();
            GetFeedbackCount();
            GetMeanFeedback();
            GetUserCount();
            feedbackLV.EndRefresh();
        }

      

        public async void scaleLabel()
        {
           uint timeout = 50;
            await RatingLbl.TranslateTo(-15, 0, timeout);

            await RatingLbl.TranslateTo(15, 0, timeout);

            await RatingLbl.TranslateTo(-10, 0, timeout);

            await RatingLbl.TranslateTo(10, 0, timeout);

            await RatingLbl.TranslateTo(-5, 0, timeout);

            await RatingLbl.TranslateTo(5, 0, timeout);

            RatingLbl.TranslationX = 0;
        }

        public async void scaleLabel2()
        {
            uint timeout = 50;
            await FeedbackNoLbl.TranslateTo(-15, 0, timeout);

            await FeedbackNoLbl.TranslateTo(15, 0, timeout);

            await FeedbackNoLbl.TranslateTo(-10, 0, timeout);

            await FeedbackNoLbl.TranslateTo(10, 0, timeout);

            await FeedbackNoLbl.TranslateTo(-5, 0, timeout);

            await FeedbackNoLbl.TranslateTo(5, 0, timeout);

            FeedbackNoLbl.TranslationX = 0;
        }

        public async void scaleLabel3()
        {
            uint timeout = 50;
            await TotaluserLbl.TranslateTo(-15, 0, timeout);

            await TotaluserLbl.TranslateTo(15, 0, timeout);

            await TotaluserLbl.TranslateTo(-10, 0, timeout);

            await TotaluserLbl.TranslateTo(10, 0, timeout);

            await TotaluserLbl.TranslateTo(-5, 0, timeout);

            await TotaluserLbl.TranslateTo(5, 0, timeout);

            TotaluserLbl.TranslationX = 0;
            
        }

       

     
    }
}