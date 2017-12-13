using System;
using System.Drawing;
#if __UNIFIED__
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
#endif
using Xamarin.Themes;

namespace ThemeSample {
	public partial class ButtonsController : UIViewController {
		public ButtonsController (IntPtr handle) : base (handle)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;
		}

		public override void ViewDidLoad ()
		{
			var shadowLayer = this.CreateShadowWithFrame (new RectangleF (0, 0, 320, 5));
			View.Layer.AddSublayer (shadowLayer);

			// Applies style on specified element
			GunmetalTheme.Apply (View);
			GunmetalTheme.Apply (GreenButton, ButtonType.ConfirmLarge);
			GunmetalTheme.Apply (BlackButton, ButtonType.BlackLarge);
			GunmetalTheme.Apply (AluminiumButton, ButtonType.AluminiumLarge);

			base.ViewDidLoad ();
		}
		
		CALayer CreateShadowWithFrame (RectangleF frame)
		{
			var gradient = new CAGradientLayer ();
			gradient.Frame = frame;
			
			var lightColor = UIColor.Black.ColorWithAlpha (0);
			var darkColor = UIColor.Black.ColorWithAlpha (0.3f);
			
			gradient.Colors = new CGColor[]{darkColor.CGColor, lightColor.CGColor};
			
			return gradient;
		}
	}
}
