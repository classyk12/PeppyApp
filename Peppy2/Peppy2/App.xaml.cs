using FormsControls.Base;
using Peppy2.Helpers;
using Peppy2.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Peppy2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new OfferPage());
            //checks if user is logged in before
            if (!string.IsNullOrWhiteSpace(Settings.AccessToken))
            {
                MainPage = new NavigationPage(new PeppyMasterDetail())
                {
                    BarBackgroundColor = Color.DodgerBlue,
                    BarTextColor = Color.White
                    
                
                };
            }

            else if (string.IsNullOrWhiteSpace(Settings.UserName) && string.IsNullOrWhiteSpace(Settings.Password))
            {
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Color.Purple,
                    BarTextColor = Color.White
                };

            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
