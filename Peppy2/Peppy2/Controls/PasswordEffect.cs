using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Peppy2.Controls
{
   public class PasswordEffect : RoutingEffect
    {
        public string EntryText { get; set; }
        public PasswordEffect() : base("Xamarin.PasswordEffect") { }
    }
}
