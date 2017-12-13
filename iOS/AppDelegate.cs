using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Controls;
using Xamarin.Themes;

namespace CompVerify.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            GunmetalTheme.Apply();

            var signature = new SignaturePadView()
            {
                StrokeWidth = 3f,
                StrokeColor = UIColor.Black
            };

            return base.FinishedLaunching(app, options);
        }
    }
}
