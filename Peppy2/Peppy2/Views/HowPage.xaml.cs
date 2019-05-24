using FormsControls.Base;
using Peppy2.Helpers;
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
    public partial class HowPage : AnimationPage
    {
        public HowPage()
        {
            InitializeComponent();

            FadeSL();
        }
        public async void FadeSL()
        {
            howSL.Opacity = 0;
            await howSL.FadeTo(1,2000);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ScaleButton();
            if (string.IsNullOrEmpty(Settings.UserName) && string.IsNullOrEmpty(Settings.Password))
            {
                Navigation.PushAsync(new Both());
            }
            else if (!string.IsNullOrWhiteSpace(Settings.AccessToken))
            {
                Navigation.PushAsync(new FindCleaner());
            }
        }

        public async void ScaleButton()
        {
            await HowBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await HowBtn.ScaleTo(1, 50, Easing.BounceIn);
        }
    }
}