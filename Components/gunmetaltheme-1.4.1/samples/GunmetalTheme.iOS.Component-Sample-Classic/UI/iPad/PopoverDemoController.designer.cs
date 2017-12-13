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
	[Register ("PopoverDemoController")]
	partial class PopoverDemoController
	{
		[Outlet]
		UISearchBar searchBar { get; set; }

		[Outlet]
		UITableView tableView { get; set; }

		[Action ("done:")]
		partial void Done (NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (searchBar != null) {
				searchBar.Dispose ();
				searchBar = null;
			}

			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}
		}
	}
}
