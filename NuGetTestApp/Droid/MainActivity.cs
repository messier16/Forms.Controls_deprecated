using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Messier16.Forms.Android.Controls;
using Xamarin.Forms.Platform.Android;

namespace TestApp.Droid
{
    [Activity(Label = "TestApp.Droid", 
	          Icon = "@drawable/icon", 
	          MainLauncher = true, 
	          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
	          Theme = "@style/MyTheme")]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
			// set the layout resources first
			FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
			FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Messier16Controls.InitAll();

			Xamarin.Forms.Forms.ViewInitialized += (object sender, Xamarin.Forms.ViewInitializedEventArgs e) =>
			{
				if (!string.IsNullOrWhiteSpace(e.View.AutomationId))
				{
					e.NativeView.ContentDescription = e.View.AutomationId;
				}
			};

            LoadApplication(new App());
        }
    }
}

