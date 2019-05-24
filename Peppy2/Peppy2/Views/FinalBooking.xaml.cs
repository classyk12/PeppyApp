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
	public partial class FinalBooking : ContentPage
	{
		public FinalBooking ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ScaleButton2();
            Navigation.PushAsync(new SaveBookingPage());
        }

        public async void ScaleButton2()
        {
            await proceedBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await proceedBtn.ScaleTo(1, 50, Easing.BounceIn);
        }
    }
}