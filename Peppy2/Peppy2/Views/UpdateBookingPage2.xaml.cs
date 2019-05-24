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
	public partial class UpdateBookingPage2 : ContentPage
	{
        public BookingService service = new BookingService();
		public UpdateBookingPage2 (BookingsModel model)
		{
			InitializeComponent ();
            if(model.IsCompleted == true)
            {
                infoLbl.IsVisible = true;
                infoLbl.Text = "Booking is already Marked as Completed";
                MainSL.IsEnabled = false;
                completedBtn.IsEnabled = false; 
                
            }

            BookingSettings.BookingId = model.BookingId;
            BookingSettings.StartDate = model.StartDate;
            BookingSettings.ServiceDuration = model.ServiceDuration;
            CleanerSettings.CleanerId = model.CleanerId;
            BookingSettings.IsNeedCleaningMaterials = model.IsNeedCleaningMaterials;
            BookingSettings.IsNeedIroning = model.IsNeedIroning;
            BookingSettings.IsHavePets = model.IsHavePets;
            


            servicetypeLbl.Text = model.ServiceType;
            servicedurationLbl.Text = model.ServiceDuration.ToString() + " " + "hours";
            FirstcleaningdateLbl.Text = model.StartDate;
            LastcleaningdateLbl.Text = model.EndDate;
            servicetimeLbl.Text = model.ServiceTime;
            extradescriptionLbl.Text = model.ExtraDescription;
            if(model.IsNeedIroning == true)
            {
                ironingLbl.Text = "Yes";
            }
            else
            {
                ironingLbl.Text = "No";
            }

            if (model.IsNeedCleaningMaterials == true)
            {
                cleaningmaterialLbl.Text = "Yes";
            }
            else
            {
                cleaningmaterialLbl.Text = "No";
            }

            TotalcostLbl.Text = model.TotalCost + " " + "Naira";
            cityLbl.Text = model.City;
            streetLbl.Text = model.Street;
            housenumberLbl.Text = model.HomeNumber;
            directionLbl.Text = model.DirectionsOrLandmarks;

            if (model.IsHavePets == true)
            {
                petsLbl.Text = "Yes";
            }
            else
            {
                petsLbl.Text = "No";
            }
            entryLbl.Text = model.ModeOfEntry;
            methodLbl.Text = model.PaymentMethod;
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
                        if (DateTime.Now.Date < Convert.ToDateTime(BookingSettings.StartDate))
                        {
                            await DisplayAlert("Sorry..", "You cant mark Booking as complete till after your start Date", "ok");
                            return;
                        }

                        var data = new BookingsModel
                        {
                            IsCompleted = true,
                            BookingId = BookingSettings.BookingId,
                            CleanerId = CleanerSettings.CleanerId,
                            UserId = Settings.UserId,
                            DateCreated = DateTime.Now,
                            StartDate = FirstcleaningdateLbl.Text,
                            EndDate = LastcleaningdateLbl.Text,
                            ServiceDuration = BookingSettings.ServiceDuration,
                            ExtraDescription = extradescriptionLbl.Text,
                            IsNeedIroning = BookingSettings.IsNeedIroning,
                            IsNeedCleaningMaterials = BookingSettings.IsNeedCleaningMaterials,
                            City = cityLbl.Text,
                            Street = streetLbl.Text,
                            HomeNumber = housenumberLbl.Text,
                            DirectionsOrLandmarks = directionLbl.Text,
                            ModeOfEntry = entryLbl.Text,
                            PaymentMethod = methodLbl.Text,
                            IsHavePets = BookingSettings.IsHavePets,
                            ServiceTime = servicetimeLbl.Text,
                            ServiceType = servicetypeLbl.Text,
                            TotalCost = TotalcostLbl.Text

                        };

                        var ask = await DisplayActionSheet("Are you sure you want to mark this booking as completed?", "", "", "Yes", "No");
                        if (ask == "Yes")
                        {
                            var response = await service.UpdateBooking(BookingSettings.BookingId, data);
                            if (!response)
                            {
                                await DisplayAlert("Error..", "We could not update your booking at the moment, try again", "ok");
                            }
                            else
                            {
                                await DisplayAlert("Awesome!", "You have completed this booking", "ok");
                                await Navigation.PopAsync();
                            }

                        }
                    }
                    catch (Exception)
                    {

                       await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
                    }

                }
            }
            else
            {
                await DisplayAlert("Connection Timeout", "Check your internet connection and try again", "Ok");
            }
        }

        public async void scaleButton()
        {
            await completedBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await completedBtn.ScaleTo(1, 50, Easing.BounceIn);
        }

    }
    
}