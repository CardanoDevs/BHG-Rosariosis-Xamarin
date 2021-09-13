using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Firebase;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using Plugin.InAppBilling;
using Shared.XFStructure;
using System;
using Xamarin.Forms;

namespace XFStructure.Droid
{
    [Activity(Label = "GlobalSIS", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/logo", Theme = "@style/splashscreen",
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
#pragma warning disable 0219, 0649
        static bool falseflag = false;
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        static MainActivity()
        {
           
            if (falseflag)
            {
                var ignore = new FitWindowsFrameLayout(Forms.Context);
            }
        }
#pragma warning restore 0219, 0649

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {

                Forms.SetFlags("RadioButton_Experimental");

                CrossCurrentActivity.Current.Init(this, savedInstanceState);
                //FirebaseApp.InitializeApp(this);

                this.Window.AddFlags(WindowManagerFlags.Fullscreen);
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;
                base.OnCreate(savedInstanceState);

                //FirebaseAnalytics.GetInstance(Forms.Context);

                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
                global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
                LoadApplication(new App());
                //FirebasePushNotificationManager.ProcessIntent(this, Intent);

            }
            catch (Exception)
            {

            }          
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
        }
    }
}