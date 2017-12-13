using System;
using System.Drawing;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;
#endif

using Xamarin.Controls.iOS;
using Xamarin.Controls.iOS.ProgressBar;
using Xamarin.Controls.iOS.Switch;
using Xamarin.Controls.iOS.AlertView;
using Xamarin.Themes;

namespace ThemeSample {
	public partial class DetailsController : UIViewController {
		UIPopoverController popoverController;
		PopoverProgressBar progressBar;

		public DetailsController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			// Applies style on specified element
			GunmetalTheme.Apply (View, UIDeviceOrientation.LandscapeLeft);
			GunmetalTheme.Apply (textInputView);
			GunmetalTheme.Apply (UISegmentedControl.Appearance);
			
			slider.Frame = new RectangleF (218, 410, 327, 24);

			var shadowLayer = this.CreateShadowWithFrame (new RectangleF (0, 0, 768, 5));
			View.Layer.AddSublayer (shadowLayer);

			bool showSwitchText = !UIDevice.CurrentDevice.CheckSystemVersion (7, 0);

			var onRect = new RectangleF (300, 280, 80, 36);
			var offRect = new RectangleF (390, 280, 80, 36);

			if (!showSwitchText) {
				onRect.X += 20;
				onRect.Width -= 20;
				offRect.Width -= 20;
			}
			
			var onSwitch = new SwitchOnOff (onRect);
			var offSwitch = new SwitchOnOff (offRect);

			onSwitch.ShowText (showSwitchText);
			offSwitch.ShowText (showSwitchText);

			onSwitch.SetOn (true);			
			offSwitch.SetOn (false);    
			
			progressBar = new PopoverProgressBar (new RectangleF (218, 340, 327, 24), ProgressBarColor.Blue);
			progressBar.SetProgress (0.5f);			

			var data = new [] { "Yes", "No", "Maybe" };
			var segment = new UISegmentedControl (data);
			segment.Frame = new RectangleF (250, 460, 250, 45);
			segment.SelectedSegment = 0;

			scrollView.AddSubviews (segment, onSwitch, offSwitch, progressBar);

			slider.ExclusiveTouch = true;

			greenButton.SetBackgroundImage (GunmetalTheme.SharedTheme.ButtonImage(ButtonType.Confirm, UIControlState.Normal), UIControlState.Normal);
			greenButtonPressed.SetBackgroundImage (GunmetalTheme.SharedTheme.ButtonImage(ButtonType.Confirm, UIControlState.Highlighted), UIControlState.Normal);

			blackButton.SetBackgroundImage (GunmetalTheme.SharedTheme.ButtonImage(ButtonType.Black, UIControlState.Normal), UIControlState.Normal);
			blackButtonPressed.SetBackgroundImage (GunmetalTheme.SharedTheme.ButtonImage(ButtonType.Black, UIControlState.Highlighted), UIControlState.Normal);

			redButton.SetBackgroundImage (GunmetalTheme.SharedTheme.ButtonImage(ButtonType.Cancel, UIControlState.Normal), UIControlState.Normal);
			redButtonPressed.SetBackgroundImage (GunmetalTheme.SharedTheme.ButtonImage(ButtonType.Cancel, UIControlState.Highlighted), UIControlState.Normal);

			grayButton.SetBackgroundImage (GunmetalTheme.SharedTheme.ButtonImage(ButtonType.Aluminium, UIControlState.Normal), UIControlState.Normal);
			grayButtonPressed.SetBackgroundImage (GunmetalTheme.SharedTheme.ButtonImage(ButtonType.Aluminium, UIControlState.Highlighted), UIControlState.Normal);

			base.ViewDidLoad ();
		}
		
		partial void ShowAlert (NSObject sender)
		{
			var alert = new AlertView ("Alert Title", "This is an alert message");
			
			alert.SetDestructiveButtonWithTitle ("Cancel", null);
			alert.AddButtonWithTitle ("OK", () => {
				this.ShowAlert (null);
			});

			alert.Show ();
		}

		partial void ValueChanged (NSObject sender)
		{
			if (slider.Value >= 0.0f && slider.Value <= 1.0f)			
				progressBar.SetProgress (slider.Value);
		}
		
		CALayer CreateShadowWithFrame (RectangleF frame)
		{
			var darkColor = UIColor.Black.ColorWithAlpha (0.3f);
			var lightColor = UIColor.Black.ColorWithAlpha (0);

			var gradient = new CAGradientLayer () {
				Frame = frame,
				Colors = new[] { darkColor.CGColor, lightColor.CGColor }
			};			
				
			return gradient;
		}

		[Obsolete ("Deprecated in iOS 6.0", false)]
		public override void ViewDidUnload ()
		{
			navigationItem = null;

			base.ViewDidUnload ();
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (!segue.Identifier.Equals ("showPopover"))
				return;

			TogglePopover (null);

			var popoverController = ((UIStoryboardPopoverSegue)segue).PopoverController;
			popoverController.PopoverBackgroundViewType = typeof (CustomPopoverBackgroundView);

			this.popoverController = popoverController;				
			this.popoverController.WeakDelegate = this;
		}

		partial void TogglePopover (NSObject sender)
		{
			if (popoverController == null)
				return;

			popoverController.Dismiss (true);
			popoverController = null;
		}

		[Export ("splitViewController:willHideViewController:withBarButtonItem:forPopoverController:")]
		void SplitViewControllerWillHide (UISplitViewController splitViewController, UIViewController viewController, 
		                                 UIBarButtonItem barButtonItem,	UIPopoverController popoverController)
		{
			barButtonItem.Title = "Master";
			barButtonItem.TintColor = UIColor.Blue;

			var items = navigationItem.LeftBarButtonItems.ToList ();

			items.Insert (0, barButtonItem);

			navigationItem.LeftBarButtonItems = items.ToArray (); 
			GunmetalTheme.Apply (barButtonItem);
		}

		[Export ("splitViewController:willShowViewController:invalidatingBarButtonItem:")]
		void SplitViewControllerWillShow (UISplitViewController splitController, UIViewController viewController, 
		                                 UIBarButtonItem barButtonItem)
		{
			var items = navigationItem.LeftBarButtonItems.ToList ();

			items.Remove (barButtonItem);

			navigationItem.LeftBarButtonItems = items.ToArray ();
		}

		[Export ("createBarButtonWithImageName:andSelectedImage:")]
		UIBarButtonItem CreateBarButton (NSString imageName, NSString selectedImageName)
		{
			var buttonImage = UIImage.FromFile (imageName.ToString ());
			
			var button = new UIButton (new RectangleF (0, 0, (float)buttonImage.Size.Width, (float)buttonImage.Size.Height));

			button.SetImage (buttonImage, UIControlState.Normal);
			button.SetImage (UIImage.FromFile (selectedImageName.ToString ()), UIControlState.Highlighted);

			var barButton = new UIBarButtonItem (button);
			GunmetalTheme.Apply (barButton);

			return barButton;
		}
		
		[Export ("createBarButtonWithImageName:selectedImage:andSelector:")]
		UIBarButtonItem CreateBarButton (NSString imageName, NSString selectedImageName, Selector selector)
		{
			var buttonImage = UIImage.FromFile (imageName.ToString ());
			
			var button = new UIButton (new RectangleF (0, 0, (float)buttonImage.Size.Width, (float)buttonImage.Size.Height));

			button.SetImage (buttonImage, UIControlState.Normal);
			button.SetImage (UIImage.FromFile (selectedImageName.ToString ()), UIControlState.Highlighted);
			button.AddTarget (this, selector, UIControlEvent.TouchUpInside);
			
			var barButton = new UIBarButtonItem (button);
			GunmetalTheme.Apply (barButton);
			
			return barButton;
		}
	}
}
