
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using FFImageLoading.Forms.Platform;
using ImageCircle.Forms.Plugin.Droid;
using Lottie.Forms.Droid;
using PanCardView.Droid;
using Plugin.CurrentActivity;

namespace Peppy2.Droid
{
    [Activity(Label = "Peppy", Icon = "@mipmap/bicon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.Orientation, 
        ScreenOrientation = ScreenOrientation.Portrait )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);           
            ImageCircleRenderer.Init();
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            AnimationViewRenderer.Init();
            UserDialogs.Init(this);
            FormsControls.Droid.Main.Init(this);
            CardsViewRenderer.Preserve();
            CachedImageRenderer.Init(true);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override Resources Resources
        {
            get
            {
                Resources res = base.Resources;
                Configuration config = new Configuration();
                config.SetToDefaults();
                res.UpdateConfiguration(config, res.DisplayMetrics);
                return res;
            }
        }

    }
}
