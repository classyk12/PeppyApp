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
    public partial class UpdateBookingPage : ContentPage
    {
        public int selectedindex { get; set; }
        public UpdateBookingPage(BookingsModel model)
        {
            InitializeComponent();
            if(model.IsCompleted == true)
            {
                infoLbl.IsVisible = true;
                infoLbl.Text = "This Booking is already marked complete and cannot be updated";
                MainSL.IsEnabled = false;
                updateBtn.IsEnabled = false;
            }
            BookingSettings.BookingId = model.BookingId;
            CleanerSettings.CleanerId = model.CleanerId;
            startdatepicker.Date = Convert.ToDateTime(model.StartDate);

            if(model.EndDate == "")
            {
                enddatepicker.Date = DateTime.Now.Date;
            }
            else
            {
                enddatepicker.Date = Convert.ToDateTime(model.EndDate);
            }

            
            DescriptionEditor.Text = model.ExtraDescription;
            ironingswitch.IsToggled = model.IsNeedIroning;
            materialsswitch.IsToggled = model.IsNeedCleaningMaterials;
            totalcostLbl.Text = model.TotalCost;
            VATcostLbl.Text = (Convert.ToInt32(totalcostLbl.Text) * 0.1).ToString();
            streetentry.Text = model.Street;
            housenumberentry.Text = model.HomeNumber;
            directionsentry.Text = model.DirectionsOrLandmarks;
            petsswitch.IsToggled = model.IsHavePets;
            detailseditor.Text = model.ModeOfEntry;





            PopulatePicker();
            PopulateTimePicker();
            ironingswitch.Toggled += Ironswitch;
            materialsswitch.Toggled += materialsswitch_Toggled;
            PopulateCityPicker();
            PopulateMethodPicker();



        }





        public void PopulateMethodPicker()
        {
            List<Payment> methods = new List<Payment>
            {
                 new Payment{id = 1, method="Cash Payment"},
                 new Payment{id = 2, method="Card payment"}
            };
            paymentpicker.ItemsSource = methods;
        }

        public void PopulateCityPicker()
        {

            List<Locations> location = new List<Locations>
            {
                new Locations{Id = 1, city="Ajah"},
                 new Locations{Id = 2, city="Banana Island"},
                  new Locations{Id = 3, city="Ikoyi"},
                   new Locations{Id = 4, city="Lekki"},
                    new Locations{Id = 5, city="Magodo"},
                     new Locations{Id = 6, city="Victoria Island"},
            };
            citypicker.ItemsSource = location;
        }


        public void PopulatePicker()
        {
            List<ServiceTypes> types = new List<ServiceTypes>()
            {
                new ServiceTypes {ServiceType = "One Time Service"},
                new ServiceTypes{ServiceType = "Weekly Service"}
            };
            servicepicker.ItemsSource = types;
        }
        public void PopulateTimePicker()
        {
            List<ServiceDuration> durations = new List<ServiceDuration>
            {
                new ServiceDuration {stringTime = "2 hours", DoubleTime = 2},
                new ServiceDuration {stringTime = "3 hours", DoubleTime = 3},
                new ServiceDuration {stringTime = "4 hours", DoubleTime = 4},
                new ServiceDuration {stringTime = "5 hours", DoubleTime = 5},
                new ServiceDuration {stringTime = "6 hours", DoubleTime = 6},
                new ServiceDuration {stringTime = "7 hours", DoubleTime = 7},
                new ServiceDuration {stringTime = "8 hours", DoubleTime = 8},
            };
            durationpicker.ItemsSource = durations;
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
                        //checks for empty picker values
                        if (servicepicker.SelectedIndex == -1 || durationpicker.SelectedIndex == -1)
                        {
                            await DisplayAlert("Error..", "Service type or Cleaning Duration cannot be empty. Please select a value.", "ok");
                            return;
                        }
                        //checks if TimePicker value is greater than 4pm
                        if (servicetimepicker.Time > TimeSpan.FromHours(16))
                        {
                            await DisplayAlert("Error", "Service time should be 4pm or earlier", "ok");
                            return;
                        }

                        //checks if the startDate is less than current Date
                        if (startdatepicker.Date.Date < DateTime.Now)
                        {
                            await DisplayAlert("Error", "Start Date should be later than today's Date", "ok");
                            return;
                        }
                        //checks if the end date is greater than start date
                        if (servicepicker.SelectedIndex == 1 && enddatepicker.Date.Date <= startdatepicker.Date.Date)
                        {
                            await DisplayAlert("Error..", "Please End date must later than the start date", "ok");
                            return;
                        }
                        if (servicepicker.SelectedIndex == 1 && enddatepicker.Date.Date < DateTime.Now)
                        {
                            await DisplayAlert("Error..", "End Date must be later than start date", "ok");
                            return;
                        }

                        if (citypicker.SelectedIndex == -1)
                        {
                            await DisplayAlert("Error", "Please select your city", "ok");
                            return;
                        }

                        if (string.IsNullOrEmpty(streetentry.Text) || string.IsNullOrEmpty(housenumberentry.Text))
                        {
                            await DisplayAlert("Error", "Please enter your house number and street number", "ok");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(detailseditor.Text))
                        {
                            await DisplayAlert("Error", "Please enter a short descripton telling us how your cleaner will get into your house", "ok");
                            return;
                        }

                        if (paymentpicker.SelectedIndex == -1)
                        {
                            await DisplayAlert("Error", "Choose a payment method", "ok");
                            return;
                        }

                        //if (startdatepicker.Date > enddatepicker.Date && servicepicker.SelectedIndex == 0)
                        //{
                        //    await DisplayAlert("Error", "Start Date cannot be greater than", "ok");
                        //    return;
                        //}

                        BookingService service = new BookingService();
                        var data = new BookingsModel
                        {
                            BookingId = BookingSettings.BookingId,
                            City = citypicker.Items[citypicker.SelectedIndex],
                            CleanerId = CleanerSettings.CleanerId,
                            DateCreated = DateTime.Now.Date,
                            DirectionsOrLandmarks = directionsentry.Text,
                            EndDate = enddatepicker.Date.ToLongDateString(),
                            ExtraDescription = DescriptionEditor.Text,
                            HomeNumber = housenumberentry.Text,
                            IsCompleted = false,
                            IsHavePets = petsswitch.IsToggled,
                            IsNeedCleaningMaterials = materialsswitch.IsToggled,
                            IsNeedIroning = ironingswitch.IsToggled,
                            ModeOfEntry = detailseditor.Text,
                            PaymentMethod = paymentpicker.Items[paymentpicker.SelectedIndex],
                            ServiceDuration = selectedindex,
                            ServiceTime = servicetimepicker.Time.ToString(),
                            ServiceType = servicepicker.Items[servicepicker.SelectedIndex],
                            StartDate = startdatepicker.Date.ToLongDateString(),
                            Street = streetentry.Text,
                            TotalCost = totalcostLbl.Text,
                            UserId = Settings.UserId
                        };
                        bool response = await service.UpdateBooking(BookingSettings.BookingId, data);
                        if (response)
                        {
                            await DisplayAlert("Awesome!", "Booking Updated Successfully", "ok");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error..", "Sorry something went wrong and we could  not update your booking, Try again", "ok");
                            return;
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
                await DisplayAlert("Connection Timeout", "Check your internet connection and try again", "ok");
            }
        }

        private void durationpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var duration = (ServiceDuration)durationpicker.SelectedItem;
            selectedindex = duration.DoubleTime;

            if(servicepicker.SelectedIndex == -1)
            {
                DisplayAlert("Error..", "Select a service type", "ok");
            }
            //if (servicepicker.Items[servicepicker.SelectedIndex] == "One Time Service")
            if(servicepicker.SelectedIndex == 0 || servicepicker.SelectedIndex == -1)
            {
                VATcostLbl.Text = "0.00";
                totalcostLbl.Text = "0.00";
                var cost = selectedindex * 2500;
                int vat = Convert.ToInt32(cost * 0.1);
                int TotalCost = cost + vat;
                totalcostLbl.Text = TotalCost.ToString();
                VATcostLbl.Text = vat.ToString();
            }
            //if (servicepicker.Items[servicepicker.SelectedIndex] == "Weekly Service")
            if (servicepicker.SelectedIndex == 1 || servicepicker.SelectedIndex == -1)
            {
                VATcostLbl.Text = "0.00";
                totalcostLbl.Text = "0.00";
                var weeks = enddatepicker.Date.Date - startdatepicker.Date.Date;
                int NumberOfWeeks = Convert.ToInt32(weeks.TotalDays / 7);
                var cost = 2500 * selectedindex * NumberOfWeeks;
                int vat = Convert.ToInt32(cost * 0.1);
                double TotalCost = cost + vat;
                totalcostLbl.Text = TotalCost.ToString();
                VATcostLbl.Text = vat.ToString();
            }


        }

        private void servicepicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (servicepicker.SelectedIndex == 0)
            {
                VATcostLbl.Text = "0.00";
                totalcostLbl.Text = "0.00";
                endDateLbl.IsVisible = false;
                endDateLbl.IsEnabled = false;
                enddatepicker.IsEnabled = false;
                enddatepicker.IsVisible = false;
                InfoLbl.IsVisible = false;
                durationpicker.IsEnabled = true;
            }

            else
            {
                VATcostLbl.Text = "0.00";
                totalcostLbl.Text = "0.00";
                endDateLbl.IsVisible = true;
                endDateLbl.IsEnabled = true;
                enddatepicker.IsEnabled = true;
                enddatepicker.IsVisible = true;
                durationpicker.IsEnabled = true;
            }
        }

        private async void startdatepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (startdatepicker.Date < DateTime.Now.Date)
            {
                await DisplayAlert("Error..", "Date can either be today or later", "ok");
                startdatepicker.Focus();
            }
            string totaldays = (startdatepicker.Date.Date - DateTime.Now.Date).TotalDays.ToString();
            startdateinfolbl.Text = totaldays + " " + "days from Today";



        }

        private async void enddatepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (enddatepicker.Date <= startdatepicker.Date.Date)
            {
                await DisplayAlert("Error..", "Please select a date later than the start date", "ok");
                enddatepicker.Focus();
            }
            if (enddatepicker.Date - startdatepicker.Date.Date < TimeSpan.FromDays(7))
            {
                await DisplayAlert("Error..", "End Date and start Date should be at least 7 days( 1 week) apart", "ok");
                enddatepicker.Focus();

            }

            TimeSpan weeks = enddatepicker.Date.Date - startdatepicker.Date.Date;
            //extra paparazi for End and Start Dates
            string TotalDays = (enddatepicker.Date.Date - startdatepicker.Date.Date).TotalDays.ToString();
            int NumberOfWeeks = Convert.ToInt32(weeks.TotalDays / 7);
            InfoLbl.Text = TotalDays + " Days" + " " + "between" + " " + "start Date and End Date" + " " + "and" + " " + NumberOfWeeks.ToString() + " " + "cleans" + " " + "between Dates";
            InfoLbl.IsVisible = true;
            var cost = 2500 * selectedindex * NumberOfWeeks;
            int vat = Convert.ToInt32(cost * 0.1);            
            int TotalCost = cost + vat;
            totalcostLbl.Text = TotalCost.ToString();
            VATcostLbl.Text = vat.ToString();
        }

        private async void Ironswitch(object sender, ToggledEventArgs e)
        {

            try
            {
                if (ironingswitch.IsToggled == true)
                {
                    var decimalTotal = Convert.ToInt32(totalcostLbl.Text);
                    var NewTotal = Convert.ToInt32(decimalTotal + 1000.00);
                    totalcostLbl.Text = NewTotal.ToString();
                }
                else
                {
                    var decimalTotal = Convert.ToInt32(totalcostLbl.Text);
                    var NewTotal = Convert.ToInt32(decimalTotal - 1000.00);
                    totalcostLbl.Text = NewTotal.ToString();
                }

            }
            catch (Exception)
            {
                await DisplayAlert("Error..", "Select a service type and a Service Duration first", "ok");
                ironingswitch.IsToggled = false;
            }
        }

        private async void materialsswitch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (materialsswitch.IsToggled == true)
                {
                    var decimalTotal = Convert.ToInt32(totalcostLbl.Text);
                    int NewTotal = Convert.ToInt32(decimalTotal + 1550.00);
                    totalcostLbl.Text = NewTotal.ToString();
                }
                else
                {
                    var decimalTotal = Convert.ToInt32(totalcostLbl.Text);
                    var NewTotal = Convert.ToInt32(decimalTotal - 1550.00);
                    totalcostLbl.Text = NewTotal.ToString();
                }

            }
            catch (Exception)
            {
                await DisplayAlert("Error..", "Select a service type and a Service Duration first", "ok");
                materialsswitch.IsToggled = false;
                ironingswitch.IsToggled = false;
            }
        }

        public async void scaleButton()
        {
            await updateBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await updateBtn.ScaleTo(1, 50, Easing.BounceIn);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new IncludedPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CleaningProducts());
        }
    }
}