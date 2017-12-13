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

using Xamarin.Controls.iOS.ProgressBar;
using Xamarin.Controls.iOS.Switch;
using Xamarin.Themes;

namespace ThemeSample {
	public partial class ElementsController : UIViewController {
		const float KeyboardAnimationDuration = 0.3f;
		const float MinimumScrollFraction = 0.2f;
		const float MaximumScrollFraction = 0.8f;
		const float PortraitKeyboardHeight = 216;
		const float LandscapeKeyboardHeight = 140;
		
		float animatedDistance;

		UISlider slider;
		PopoverProgressBar progressBar;

		public ElementsController (IntPtr handle) : base (handle)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;
		}

		public override void ViewDidLoad ()
		{
			// Applies style on specified element
			GunmetalTheme.Apply (View);
			GunmetalTheme.Apply (textInput);

			var shadowLayer = CreateShadowWithFrame (new RectangleF (0, 0, 320, 5));

			View.Layer.AddSublayer (shadowLayer);

			var button = new UIButton (UIButtonType.Custom) {
				Frame = new RectangleF (1, 1, 29, 29),
			};

			if (GunmetalTheme.IsIOS7)
			{
				button.SetBackgroundImage (UIImage.FromFile ("action_menu.png"), UIControlState.Normal);
			}
			else
			{
				button.SetBackgroundImage (UIImage.FromFile ("GunmetalImages/navbar-button.png"), UIControlState.Normal);
			}

			
			var barButtonItem = new UIBarButtonItem (button);
			NavigationItem.LeftBarButtonItem = barButtonItem;
									
			progressBar = new PopoverProgressBar (new RectangleF (10, 30, 300, 24), ProgressBarColor.Blue);
			progressBar.SetProgress (0.5f);   
						
			slider = new UISlider (new RectangleF (10, 100, 300, 24)) {
				MaxValue = 1,
				MinValue = 0,
				Value = 0.5f
			};

			slider.ValueChanged += (sender, e) => ValueChanged ();

			bool showSwitchText = !UIDevice.CurrentDevice.CheckSystemVersion (7, 0);

			var onRect = new RectangleF (70, 150, 80, 36);
			var offRect = new RectangleF (180, 150, 80, 36);

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

			var segments = new [] { "Yes", "No", "Maybe" };
			var segmentControl = new UISegmentedControl (segments) {
				Frame = new RectangleF (40, 210, 250, 45),
				SelectedSegment = 0
			};
			
			scrollView.AddSubviews (progressBar, slider, onSwitch, offSwitch, segmentControl);

			textInput.WeakDelegate = this;
			textInput.ReturnKeyType = UIReturnKeyType.Done;
			textInput.LeftView = new UIView (new RectangleF (0, 0, 5, 30));
			textInput.LeftViewMode = UITextFieldViewMode.Always;

			base.ViewDidLoad ();
		}

		void ValueChanged ()
		{
			if (slider.Value >= 0.0f && slider.Value <= 1.0f)			
				progressBar.SetProgress (slider.Value);
		}

		[Export ("textFieldDidBeginEditing:")]
		void TextFieldDidBeginEditing (UITextField textField)
		{
			var textFieldRect = scrollView.Window.ConvertRectFromView (textField.Bounds, textField);
			var viewRect = scrollView.Window.ConvertRectFromView (scrollView.Bounds, scrollView);
			var midline = textFieldRect.Y + 0.5f * textFieldRect.Height;

			var numerator = midline - viewRect.Y - MinimumScrollFraction * viewRect.Height;
			var denominator = (MaximumScrollFraction - MinimumScrollFraction) * viewRect.Height;
			var heightFraction = numerator / denominator;
			
			if (heightFraction < 0.0f)			
				heightFraction = 0.0f;
			else if (heightFraction > 1.0f)
				heightFraction = 1.0f;
			
			var orientation = UIApplication.SharedApplication.StatusBarOrientation;

			if (orientation == UIInterfaceOrientation.Portrait || orientation == UIInterfaceOrientation.PortraitUpsideDown)			
				animatedDistance = (float)Math.Floor (PortraitKeyboardHeight * heightFraction);
			else
				animatedDistance = (float)Math.Floor (LandscapeKeyboardHeight * heightFraction);
			
			var viewFrame = scrollView.Frame;
			viewFrame.Y -= animatedDistance;
			
			UIView.Animate (KeyboardAnimationDuration, () => {
				scrollView.Frame = viewFrame;
			});
		}
		
		[Export ("textFieldDidEndEditing:")]
		void TextFieldDidEndEditing (UITextField textField)
		{
			var viewFrame = scrollView.Frame;
			viewFrame.Y += animatedDistance;
			
			UIView.Animate (KeyboardAnimationDuration, () => {
				scrollView.Frame = viewFrame;
			});
		}
		
		[Export ("textFieldShouldReturn:")]
		bool TextFieldShouldReturn (UITextField textField)
		{
			textField.ResignFirstResponder ();

			return true;
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
