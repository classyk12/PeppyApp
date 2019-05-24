using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Constants
{
    public class UrlConstants
    {
        public static string url = "https://peppycleaners.peppyapi.name.ng"; //root directory

        public static string url2 = "https://peppycleaners.peppyapi.name.ng/{0}"; //a url with a subdirectory

        public static string registerurl = "https://peppycleaners.peppyapi.name.ng/api/Account/Register";//register url

        public static string loginurl = "https://peppycleaners.peppyapi.name.ng/Token";//url for user login

        public static string offerurl = "https://peppycleaners.peppyapi.name.ng/api/Offers";//used for sending Offer request

        public static string cleanerurl = "https://peppycleaners.peppyapi.name.ng/api/Cleaners";//used for sending cleaner offer

        public static string feedbackurl = "https://peppycleaners.peppyapi.name.ng/api/Feedbacks";//used for sending cleaner offer

        public static string meanfeedbackurl = "https://peppycleaners.peppyapi.name.ng/api/Feedbacks4"; //used to get mean feedback

        public static string feedbackcounturl =  "https://peppycleaners.peppyapi.name.ng/api/Feedbacks2";//used to get feedback count

        public static string usercountfeedbackurl = "https://peppycleaners.peppyapi.name.ng/api/Feedbacks3"; //used to get user count in feedback

        public static string bookingsurl = "https://peppycleaners.peppyapi.name.ng/api/Bookings";//used to sending bookings requests

        public static string bookingsurl2 = "https://peppycleaners.peppyapi.name.ng/api/Bookings/";//used to sending bookings requests

        public static string donebooking = "https://peppycleaners.peppyapi.name.ng/api/DoneBooking";//used to getting Completed Bookings

        public static string pendingbooking= "https://peppycleaners.peppyapi.name.ng/api/PendingBooking";//used to getting Pending Bookings

    }
}
