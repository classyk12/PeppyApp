using Peppy2.Constants;
using System;
using System.Collections.Generic;
using System.Text;


namespace Peppy2.Models
{
    public class RegisterModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime DateJoined { get; set; }
        public string PhoneNumber { get; set; }
        public string Alias { get; set; }
        public string ImagePath { get; set; }
        public object ImageArray { get; set; }
        //returns the full path from the Api Url
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
