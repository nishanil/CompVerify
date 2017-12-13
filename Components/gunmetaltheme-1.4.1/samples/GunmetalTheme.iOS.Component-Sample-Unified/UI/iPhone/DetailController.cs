using System;
using System.Drawing;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif
using Xamarin.Themes;

namespace ThemeSample {
	public class DetailController : UIViewController {
		UIImageView avatarImageView;
		UIImageView avatarBackgroundImageView;

		UILabel titleLabel;
		UITextView textView;

		public DetailController ()
		{
			Title = "Profile";
			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;

			InitSubviews ();
			ApplyStyles ();
		}

		void InitSubviews ()
		{
			avatarImageView = new UIImageView (UIImage.FromFile("avatar1.png")) {
				Frame = new RectangleF(15, 14, 50, 50)
			};

			avatarBackgroundImageView = new UIImageView (UIImage.FromFile("list-frame.png")) {
				Frame = new RectangleF(10, 10, 60, 60)
			};

			titleLabel = new UILabel (new RectangleF(78, 10, 214, 21)) {
				BackgroundColor = UIColor.Clear,
				Text = "Lance Maeyer",
				Font = UIFont.SystemFontOfSize(16),
				TextColor = UIColor.White
			};

			textView = new UITextView (new RectangleF(78, 33, 214, 200)) {
				BackgroundColor = UIColor.Clear,
				Text = "Lorem ipsum dolor sit amet, sed do consectetur",
				Font = UIFont.SystemFontOfSize(14),
				TextColor = UIColor.DarkGray
			};

			View.AddSubviews (avatarBackgroundImageView, avatarImageView, titleLabel, textView);
		}

		void ApplyStyles ()
		{
			View.BackgroundColor = UIColor.FromRGB (29, 29, 29);
		}
	}
}

