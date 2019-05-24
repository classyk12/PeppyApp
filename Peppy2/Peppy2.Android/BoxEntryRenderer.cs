using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Peppy2.Controls;
using Peppy2.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BoxEntry), typeof(BoxEntryRenderer))]
namespace Peppy2.Droid
{
   public class BoxEntryRenderer : EntryRenderer
    {
        public BoxEntryRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();

                gradientDrawable.SetStroke(1, Android.Graphics.Color.Black);
                gradientDrawable.SetColor(Android.Graphics.Color.Transparent);

                Control.SetBackground(gradientDrawable);
                Control.SetPadding(5, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }
        }
    }
}