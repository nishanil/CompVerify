using System;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

using Xamarin.Themes;

namespace ThemeSample {
	public partial class ListViewController : UITableViewController {
		const string CellKey = "ProfileCell";

		public ListViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Applies style on specified element
			GunmetalTheme.Apply (View);
		}

		[Export ("tableView:heightForRowAtIndexPath:")]
		float TableViewHeightForRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{
			return 80;
		}

		[Export ("numberOfSectionsInTableView:")]
		int NumberOfSectionsInTableView (UITableView tableView)
		{
			return 1;
		}
		
		[Export ("tableView:numberOfRowsInSection:")]
		int NumberOfRowsInSection (UITableView tableView, int section)
		{
			return 2;
		}
		
		[Export ("tableView:cellForRowAtIndexPath:")]
		UITableViewCell TableViewCellForRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (MasterCell)tableView.DequeueReusableCell (CellKey);

			cell.DisclosureImageView.Image = GunmetalTheme.SharedTheme.CellDisclosureImage;
			cell.BackgroundImageView.Image = GunmetalTheme.SharedTheme.CellBackground;
									
			return cell;
		}
		
		[Export ("tableView:didSelectRowAtIndexPath:")]
		void TableViewDidSelectRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);
			NavigationController.PushViewController (new DetailController(), true);
		}
	}
}
