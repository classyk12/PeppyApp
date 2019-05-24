using FormsControls.Base;
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
	public partial class BookingDetailsPage : AnimationPage
	{
		public BookingDetailsPage (BookingsModel model)
		{
			InitializeComponent ();

            servicetypeLbl.Text = model.ServiceType;
            servicedurationLbl.Text = model.ServiceDuration.ToString() + " " + "hours";
            FirstcleaningdateLbl.Text = model.StartDate;
            LastcleaningdateLbl.Text = model.EndDate;
            servicetimeLbl.Text = model.ServiceTime;
            extradescriptionLbl.Text = model.ExtraDescription;
            if (model.IsNeedIroning == true)
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

            if(model.IsCompleted == true)
            {
                statusLbl.Text = "Completed";
            }
            else
            {
                statusLbl.Text = "Pending";
            }

        }
	}
}