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
	[Register ("DetailThemeiPadController")]
	partial class DetailsController
	{
		[Outlet]
		UINavigationItem navigationItem { get; set; }

		[Outlet]
		UIToolbar toolbar { get; set; }

		[Outlet]
		UIView shadowView { get; set; }

		[Outlet]
		UISlider slider { get; set; }

		[Outlet]
		UIScrollView scrollView { get; set; }

		[Outlet]
		UIBarButtonItem showPopoverButton { get; set; }

		[Outlet]
		UIButton greenButton { get; set; }

		[Outlet]
		UIButton greenButtonPressed { get; set; }

		[Outlet]
		UIButton blackButton { get; set; }

		[Outlet]
		UIButton blackButtonPressed { get; set; }

		[Outlet]
		UIButton redButton { get; set; }

		[Outlet]
		UIButton redButtonPressed { get; set; }

		[Outlet]
		UIButton grayButton { get; set; }

		[Outlet]
		UIButton grayButtonPressed { get; set; }

		[Outlet]
		UITextField textInputView { get; set; }

		[Action ("showAlert:")]
		partial void ShowAlert (NSObject sender);

		[Action ("valueChanged:")]
		partial void ValueChanged (NSObject sender);

		[Action ("togglePopover:")]
		partial void TogglePopover (NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (navigationItem != null) {
				navigationItem.Dispose ();
				navigationItem = null;
			}

			if (toolbar != null) {
				toolbar.Dispose ();
				toolbar = null;
			}

			if (shadowView != null) {
				shadowView.Dispose ();
				shadowView = null;
			}

			if (slider != null) {
				slider.Dispose ();
				slider = null;
			}

			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (showPopoverButton != null) {
				showPopoverButton.Dispose ();
				showPopoverButton = null;
			}

			if (greenButton != null) {
				greenButton.Dispose ();
				greenButton = null;
			}

			if (greenButtonPressed != null) {
				greenButtonPressed.Dispose ();
				greenButtonPressed = null;
			}

			if (blackButton != null) {
				blackButton.Dispose ();
				blackButton = null;
			}

			if (blackButtonPressed != null) {
				blackButtonPressed.Dispose ();
				blackButtonPressed = null;
			}

			if (redButton != null) {
				redButton.Dispose ();
				redButton = null;
			}

			if (redButtonPressed != null) {
				redButtonPressed.Dispose ();
				redButtonPressed = null;
			}

			if (grayButton != null) {
				grayButton.Dispose ();
				grayButton = null;
			}

			if (grayButtonPressed != null) {
				grayButtonPressed.Dispose ();
				grayButtonPressed = null;
			}

			if (textInputView != null) {
				textInputView.Dispose ();
				textInputView = null;
			}
		}
	}
}
