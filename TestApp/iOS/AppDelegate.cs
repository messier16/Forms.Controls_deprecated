using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Messier16.Forms.iOS.Controls;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

namespace TestApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

#if ENABLE_TEST_CLOUD
			// requires Xamarin Test Cloud Agent
			Xamarin.Calabash.Start();
#endif
			global::Xamarin.Forms.Forms.Init();

			Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) =>
			{
				// http://developer.xamarin.com/recipes/testcloud/set-accessibilityidentifier-ios/
				if (null != e.View.AutomationId)
				{
					e.NativeView.AccessibilityIdentifier = e.View.AutomationId;
				}
			};

			LoadApplication(new App());

            Messier16Controls.InitAll();

//            UIControl.Appearance.TintColor = App.AppTint.ToUIColor();

            return base.FinishedLaunching(app, options);
        }
    }
}

