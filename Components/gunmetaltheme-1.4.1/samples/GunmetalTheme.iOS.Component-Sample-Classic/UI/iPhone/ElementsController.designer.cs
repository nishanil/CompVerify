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

namespace ThemeSample {
	[Register ("iPhoneControlsController")]
	partial class ElementsController
	{
		[Outlet]
		UIScrollView scrollView { get; set; }

		[Outlet]
		UITextField textInput { get; set; }

		[Outlet]
		UIProgressView nativePB { get; set; }

		[Action ("valueChanged:")]
		partial void valueChanged (NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (textInput != null) {
				textInput.Dispose ();
				textInput = null;
			}

			if (nativePB != null) {
				nativePB.Dispose ();
				nativePB = null;
			}
		}
	}
}
