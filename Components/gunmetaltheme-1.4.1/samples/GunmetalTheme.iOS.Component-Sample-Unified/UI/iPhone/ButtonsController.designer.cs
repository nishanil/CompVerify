// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//


#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

namespace ThemeSample
{
	[Register ("iPhoneElementsController")]
	partial class ButtonsController
	{
		[Outlet]
		UIButton GreenButton { get; set; }

		[Outlet]
		UIButton BlackButton { get; set; }

		[Outlet]
		UIButton AluminiumButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (GreenButton != null) {
				GreenButton.Dispose ();
				GreenButton = null;
			}

			if (BlackButton != null) {
				BlackButton.Dispose ();
				BlackButton = null;
			}

			if (AluminiumButton != null) {
				AluminiumButton.Dispose ();
				AluminiumButton = null;
			}
		}
	}
}
