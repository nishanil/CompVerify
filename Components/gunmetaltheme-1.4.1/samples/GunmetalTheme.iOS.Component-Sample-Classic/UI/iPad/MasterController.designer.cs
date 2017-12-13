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
	[Register ("MasterViewController")]
	partial class MasterController
	{
		[Outlet]
		UITableView masterTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (masterTableView != null) {
				masterTableView.Dispose ();
				masterTableView = null;
			}
		}
	}
}
