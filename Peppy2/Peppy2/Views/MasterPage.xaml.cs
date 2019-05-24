using Peppy2.Constants;
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
    public partial class MasterPage : ContentPage
    {
      

        public MasterPage()
        {
            InitializeComponent();
            Details();


        }

        public void Details()
        {
            usernameLbl.Text = Settings.UserName;
            emailLbl.Text = Settings.Email;
            dateLbl.Text = "JOINED: " + Settings.Date;
           
            profileImg.Source = Settings.ProfileImagePath;

          


        }





        private async void LogoutEvent(object sender, EventArgs e)
        {
            try
            {
                var dialog = await DisplayActionSheet("Are you sure you want to log out?", "", "", "Yes", "No");
                if (dialog == "Yes")
                {
                    Settings.UserName = "";
                    Settings.Password = "";
                    Settings.AccessToken = "";
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                return;

            }
            catch (Exception)
            {

                await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
         
           
        }
    }
}