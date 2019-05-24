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
	public partial class SaveBookingPage : ContentPage
	{
		public SaveBookingPage ()
		{
			InitializeComponent ();
            servicetypeLbl.Text = BookingSettings.ServiceType;
            servicedurationLbl.Text = BookingSettings.ServiceDuration.ToString() + " " + "hours";
            FirstcleaningdateLbl.Text = BookingSettings.StartDate;
            LastcleaningdateLbl.Text = BookingSettings.EndDate;
            servicetimeLbl.Text = BookingSettings.ServiceTime;
            extradescriptionLbl.Text = BookingSettings.ExtraDescription;
            if(BookingSettings.IsNeedIroning == true)
            {
                ironingLbl.Text = "Yes";
            }
            else
            {
                ironingLbl.Text = "No";
            }

            if(BookingSettings.IsNeedCleaningMaterials == true)
            {
                cleaningmaterialLbl.Text = "Yes";
            }
            else
            {
                cleaningmaterialLbl.Text = "No";
            }
           
            if(BookingSettings.IsHavePets == true)
            {
                petsLbl.Text = "Yes";
            }
            else
            {
                petsLbl.Text = "No";
            }
           
            TotalcostLbl.Text = BookingSettings.TotalCost + " " + "Naira";
            cityLbl.Text = BookingSettings.City;
            streetLbl.Text = BookingSettings.Street;
            housenumberLbl.Text = BookingSettings.HomeNumber;
            directionLbl.Text = BookingSettings.DirectionsOrLandmarks;
            
            entryLbl.Text = BookingSettings.ModeOfEntry;
            methodLbl.Text = BookingSettings.PaymentMethod;



        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            scaleButton();
            if (CrossConnectivity.Current.IsConnected)
            {


                using (UserDialogs.Instance.Loading("Please Wait..", null, null, true, MaskType.Gradient))
                {
                    try
                    {
                        BookingService service = new BookingService();
                        var data = new BookingsModel
                        {
                            BookingId = Guid.NewGuid().ToString(),
                            City = BookingSettings.City,
                            CleanerId = CleanerSettings.CleanerId,
                            DateCreated = DateTime.Now.Date,
                            DirectionsOrLandmarks = BookingSettings.DirectionsOrLandmarks,
                            EndDate = BookingSettings.EndDate,
                            ExtraDescription = BookingSettings.ExtraDescription,
                            HomeNumber = BookingSettings.HomeNumber,
                            IsCompleted = false,
                            IsHavePets = BookingSettings.IsHavePets,
                            IsNeedCleaningMaterials = BookingSettings.IsNeedCleaningMaterials,
                            IsNeedIroning = BookingSettings.IsNeedIroning,
                            ModeOfEntry = BookingSettings.ModeOfEntry,
                            PaymentMethod = BookingSettings.PaymentMethod,
                            ServiceDuration = BookingSettings.ServiceDuration,
                            ServiceTime = BookingSettings.ServiceTime,
                            ServiceType = BookingSettings.ServiceType,
                            StartDate = BookingSettings.StartDate,
                            Street = BookingSettings.Street,
                            TotalCost = BookingSettings.TotalCost,
                            UserId = Settings.UserId
                        };

                        bool response = await service.SaveBooking(data);
                        if (!response)
                        {
                            await DisplayAlert("Error", "Sorry, We could not send your booking. Check Details and try again", "ok");
                            return;
                        }
                        else
                        {
                            await DisplayAlert("Successful..", "Your shedule have been recieved. We will contact you shortly", "ok");
                            Application.Current.MainPage = new PeppyMasterDetail();
                            
                        }
                    }
                    catch (Exception)
                    {
                      await  DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
                    }
                }
            }
            else
            {
                await DisplayAlert("Connection Timeout", "Check your internet connection and try again", "ok");
            }
        }

        public async void scaleButton()
        {
            await savebookingBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await savebookingBtn.ScaleTo(1, 50, Easing.BounceIn);
        }

    }
}