using Peppy2.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Models
{
   public class OfferModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
      
        //gets and sets the date when the offer expires
        public DateTime ExpiryDate { get; set; }

        public string ImagePath { get; set; }
        public object ImageArray { get; set; }
        public bool IsAvailable { get; set; }

        public string FullImagePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImagePath))
                    return string.Empty;

                return string.Format(UrlConstants.url2, ImagePath.Substring(1));
            }
        }
    }
}
