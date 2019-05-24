using System;
using System.Collections.Generic;
using System.Text;

namespace Peppy2.Models
{
    public class FeedbackModel
    {
        public object User { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string username { get; set; }
        public string Body { get; set; }
        public DateTime DateAdded { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }

        public string ShortDate
        {
            get => DateAdded.ToShortDateString();
        }
    }
}
