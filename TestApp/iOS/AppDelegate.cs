using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Messier16.Forms.iOS.Controls;
using Xamarin.Forms.Platform.iOS;

namespace TestApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            Messier16Controls.InitAll();

            UIControl.Appearance.TintColor = App.AppTint.ToUIColor();

            return base.FinishedLaunching(app, options);
        }
    }
}

