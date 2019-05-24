using FormsControls.Base;
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
	public partial class OfferDetails : AnimationPage
	{
		public OfferDetails (OfferModel offer)
		{
			InitializeComponent ();
            OfferPic.Source = offer.FullImagePath;
            TitleLbl.Text = offer.Title;
            DescriptionLbl.Text = offer.Description;
            ExpiryDateLbl.Text = "Offer Valid till " + offer.ExpiryDate.ToShortDateString();
            AvailableLbl.Text = "Offer Availability: " + offer.IsAvailable.ToString();
                        
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            scaleButton();
            await DisplayAlert("Ok " + Settings.UserName.ToUpper(),"You will be notified", "ok");
        }

        public async void scaleButton()
        {
            await notifyBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await notifyBtn.ScaleTo(1, 50, Easing.BounceIn);
        }
    }
}