using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Models
{
   public class BookingsModel
    {
        public object Cleaner { get; set; }
        public object User { get; set; }
        public string BookingId { get; set; }

        public string FormatedBookingId
        {
            get => "PEPPY" + "-" + BookingId.Substring(22);
        }

        public int CleanerId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string ServiceType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double ServiceDuration { get; set; }
        public string ServiceTime { get; set; }
        public string ExtraDescription { get; set; }
        public bool IsNeedIroning { get; set; }
        public bool IsNeedCleaningMaterials { get; set; }
        public bool IsHavePets { get; set; }
        public string ModeOfEntry { get; set; }
        public string PaymentMethod { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public string City { get; set; }
        public string DirectionsOrLandmarks { get; set; }
        
        public bool IsCompleted { get; set; }
        //total cost of the booking
        public string TotalCost { get; set; }
        public  string IsDone
        {
            get
            {
                if (IsCompleted == true)
                    return "Completed";

                return "Pending";
            }
        }
        public string DateAdded
        {
            get
            {
                return DateCreated.ToShortDateString();
            }
        }
        

        
    }
}
