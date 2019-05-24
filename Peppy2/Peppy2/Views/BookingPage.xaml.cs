using FormsControls.Base;
using Peppy2.Helpers;
using Peppy2.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Peppy2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingPage : AnimationPage
    {
        public int selectedindex { get; set; }

        public BookingPage(CleanerModel cleaner)
        {
            InitializeComponent();
            cleanerpic.Source = cleaner.FullImagePath;
            cleanernameLbl.Text = cleaner.Name.ToUpper();

            CleanerSettings.CleanerId = cleaner.Id;
            CleanerSettings.CleanerName = cleaner.Name;
            CleanerSettings.Location = cleaner.Location;
            CleanerSettings.PhoneNumber = cleaner.PhoneNumber;
            CleanerSettings.Path = cleaner.FullImagePath;
            PopulatePicker();
            PopulateTimePicker();
            ironingswitch.Toggled += Ironswitch;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CleanerProfile());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            ScaleButton();
            if (CrossConnectivity.Current.IsConnected)
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
                }

                //method that stores data from the page into settings
                StoreDetails();

                await Navigation.PushAsync(new BookingPage2());
            }
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

        private void durationpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var duration = (ServiceDuration)durationpicker.SelectedItem;
                selectedindex = duration.DoubleTime;
                if (servicepicker.SelectedIndex == -1)
                {
                    return;
                }
                if (servicepicker.Items[servicepicker.SelectedIndex] == "One Time Service")
                {
                    VATcostLbl.Text = "0.00";
                    totalcostLbl.Text = "0.00";
                    var cost = selectedindex * 2500;
                    int vat = Convert.ToInt32(cost * 0.1);
                    int TotalCost = cost + vat;
                    totalcostLbl.Text = TotalCost.ToString();
                    VATcostLbl.Text = vat.ToString();
                }
                if (servicepicker.Items[servicepicker.SelectedIndex] == "Weekly Service")
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
            catch (Exception)
            {

                DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
           
     
        }

        private void servicepicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (servicepicker.SelectedIndex == 0)
                {
                    VATcostLbl.Text = "0.00";
                    totalcostLbl.Text = "0.00";
                    endDateLbl.IsVisible = false;
                    endDateLbl.IsEnabled = false;
                    enddatepicker.IsEnabled = false;
                    enddatepicker.IsVisible = false;
                }

                else
                {
                    VATcostLbl.Text = "0.00";
                    totalcostLbl.Text = "0.00";
                    endDateLbl.IsVisible = true;
                    endDateLbl.IsEnabled = true;
                    enddatepicker.IsEnabled = true;
                    enddatepicker.IsVisible = true;
                }
            }
            catch (Exception)
            {


                DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

        private async void startdatepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                if (startdatepicker.Date < DateTime.Now.Date)
                {
                    await DisplayAlert("Error..", "Date can either be today or later", "ok");
                    startdatepicker.Focus();
                }
                string totaldays = (startdatepicker.Date.Date - DateTime.Now.Date).TotalDays.ToString();
                startdateinfolbl.Text = totaldays + " " + "days from Today";

            }
            catch (Exception)
            {


               await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }


        }

        private async void enddatepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
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
                var cost = 2500 * selectedindex * NumberOfWeeks;
                int vat = Convert.ToInt32(cost * 0.1);
                int TotalCost = cost + vat;
                totalcostLbl.Text = TotalCost.ToString();
                VATcostLbl.Text = vat.ToString();
            }
            catch (Exception)
            {

                await DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

        private async void Ironswitch (object sender, ToggledEventArgs e)
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
            catch (Exception )
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
               
            }
        }

         void StoreDetails()
        {
            try
            {
                BookingSettings.ServiceType = servicepicker.Items[servicepicker.SelectedIndex];
                BookingSettings.ServiceDuration = durationpicker.SelectedIndex;

                //stores only startdate value when service type is set to One Time
                if (servicepicker.SelectedIndex == 0)
                {
                    BookingSettings.StartDate = startdatepicker.Date.ToLongDateString();
                    BookingSettings.EndDate = "";
                }
                //stores both start date and end date if the service type is set to Weekly Service
                else if (servicepicker.SelectedIndex == 1)
                {
                    BookingSettings.StartDate = startdatepicker.Date.ToLongDateString();
                    BookingSettings.EndDate = enddatepicker.Date.ToLongDateString();
                }

                BookingSettings.ServiceTime = servicetimepicker.Time.ToString();
                BookingSettings.ExtraDescription = DescriptionEditor.Text;
                BookingSettings.IsNeedIroning = ironingswitch.IsToggled;
                BookingSettings.IsNeedCleaningMaterials = materialsswitch.IsToggled;
                BookingSettings.TotalCost = totalcostLbl.Text;
            }
            catch (Exception)
            {
                DisplayAlert("Hello" + " " + Settings.UserName, "We trust you are doing fine", "Sure");
            }
        }

        public async void ScaleButton()
        {
            await bookBtn.ScaleTo(0.95, 50, Easing.CubicOut);
            await bookBtn.ScaleTo(1, 50, Easing.CubicIn);
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new IncludedPage());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CleaningProducts());
        }

        
    }
       

    }
