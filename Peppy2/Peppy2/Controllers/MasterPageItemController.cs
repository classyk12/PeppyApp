using Peppy2.Models;
using Peppy2.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Controllers
{
   public class MasterPageItemController
    {
       public List<MasterPageItems> pageItems { get; set; }
        public MasterPageItemController()
        {
            pageItems = new List<MasterPageItems>()
            {
                new MasterPageItems{ Title = "DashBoard" , Icon = "stopwatch.png", TargetType= typeof(Dashboard)},
                new MasterPageItems{ Title = "Bookings" , Icon = "wishlist.png", TargetType= typeof(BookingPage)},
                new MasterPageItems{ Title = "Offers" , Icon = "hand.png", TargetType= typeof(OfferPage)},
               
                new MasterPageItems{ Title = "How it works" , Icon = "file.png", TargetType= typeof(HowPage)},
                new MasterPageItems{ Title = "Feedbacks" , Icon = "speech.png", TargetType= typeof(FeedbackPage)},
              
                new MasterPageItems{ Title = "About" , Icon = "stopwatch.png", TargetType= typeof(AboutPage)},          
            };
           

        }
    }
}
