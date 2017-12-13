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
	public partial class MasterController : UIViewController {
		const string CellKey = "MasterCell";

		public MasterController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			// Applies style on specified element
			GunmetalTheme.Apply (View);

			Title = "Items";   
			
			var navBarImage = UIImage.FromFile ("GunmetalImages/menubar-left.png").CreateResizableImage (new UIEdgeInsets (10, 10 , 10, 10), UIImageResizingMode.Stretch);
			
			NavigationController.NavigationBar.SetBackgroundImage (navBarImage, UIBarMetrics.Default);
			
			masterTableView.WeakDelegate = this;
			masterTableView.WeakDataSource = this;

			base.ViewDidLoad ();
		}
		

		[Export ("numberOfSectionsInTableView:")]
		int NumberOfSectionsInTableView (UITableView tableView)
		{
			return 1;
		}

		[Export ("tableView:numberOfRowsInSection:")]
		int TableViewNumberOfRowsInSection (UITableView tableView, int section)
		{
			return 5;
		}
		
		[Export ("tableView:cellForRowAtIndexPath:")]
		UITableViewCell TableViewCellForRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{	
			var cell = (MasterCell)tableView.DequeueReusableCell (CellKey);
			
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;			
			cell.BackgroundImageView.Image = GunmetalTheme.SharedTheme.CellBackground;
			cell.DisclosureImageView.Image = UIImage.FromFile ("GunmetalImages/list-arrow.png");
			
			return cell;
			
		}
		
		[Export ("tableView:heightForRowAtIndexPath:")]
		float TableViewHeightForRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{
			return 80;
		}
		
		[Export ("tableView:didSelectRowAtIndexPath:")]
		void TableViewDidSelectRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{
		}
	}
}
