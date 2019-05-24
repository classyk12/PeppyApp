using FormsControls.Base;
using Peppy2.Helpers;
using Plugin.Messaging;
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
	public partial class AboutPage : AnimationPage
	{
        private string _phone1;
        private string _phone2;
        private string _email;
		public AboutPage ()
		{
			InitializeComponent ();
            aboutSL.FadeTo(1, 2000);

            _phone1 = phone1.Text;
            _phone2 = phone2.Text;
            _email = email.Text;
            
            FirstLbl.Text = "Hello" + " " + Settings.UserName.ToUpper() + " " + "You Can Reach Us Anytime!";
           
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //tap event for email
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                emailMessenger.SendEmail(_email, "Write a Subject", "Write Email Body here");

            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //tap event for call
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(_phone1);
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            //tap event for call
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(_phone2);
        }
    }
}