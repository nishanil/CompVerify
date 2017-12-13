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
	[Register ("MasterCell")]
	partial class MasterCell
	{
		[Outlet]
		UILabel titleLabel { get; set; }

		[Outlet]
		UILabel descriptionLabel { get; set; }

		[Outlet]
		UIImageView disclosureImageView { get; set; }

		[Outlet]
		UIImageView avatarImageView { get; set; }

		[Outlet]
		UIImageView backgroundImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (descriptionLabel != null) {
				descriptionLabel.Dispose ();
				descriptionLabel = null;
			}

			if (disclosureImageView != null) {
				disclosureImageView.Dispose ();
				disclosureImageView = null;
			}

			if (avatarImageView != null) {
				avatarImageView.Dispose ();
				avatarImageView = null;
			}

			if (backgroundImageView != null) {
				backgroundImageView.Dispose ();
				backgroundImageView = null;
			}
		}
	}
}
