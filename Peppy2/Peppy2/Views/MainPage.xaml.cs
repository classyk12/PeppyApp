using FormsControls.Base;
using Peppy2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Peppy2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GotoHowPage(object sender, EventArgs e)
        {
          
            try
            {
                await Navigation.PushAsync(new HowPage());
            }
            catch (Exception)
            {

                await DisplayAlert("Prompt..", "Press the Back and re-open app", "");
            }
        }

        private async void GotoBothPage(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new Both();
            try
            {
                await Navigation.PushAsync(new Both());
            }
            catch (Exception)
            {
                await DisplayAlert("Prompt..", "Press the Back and re-open app", "");
            }
        }

        public async void ScaleButton()
        {
            await Learnbtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await Learnbtn.ScaleTo(1, 50, Easing.BounceIn);
        }
    }
}
