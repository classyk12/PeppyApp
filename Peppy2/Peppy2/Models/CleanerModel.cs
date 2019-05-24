using Peppy2.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Models
{
    public class CleanerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAvailable { get; set; }
        public string ImagePath { get; set; }  
        
        public string FullImagePath {

            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                    return string.Empty;

                return string.Format(UrlConstants.url2, ImagePath.Substring(1));
            }
        }          
        public object ImageArray { get; set; }

    }
}
