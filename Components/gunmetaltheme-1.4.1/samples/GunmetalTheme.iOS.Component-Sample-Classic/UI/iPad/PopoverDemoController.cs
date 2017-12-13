using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

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

using Xamarin.Themes;

namespace ThemeSample {
	public partial class PopoverDemoController : UIViewController {
		const string CellKey = "CountryCell";

		List<Dictionary<string, string[]>> itemsList;
		List<string> copiedItemsList;
		bool isRowUserSelectable;
		bool isSearching;

		public PopoverDemoController (IntPtr handle) : base (handle)
		{
			itemsList = new List<Dictionary<string,string[]>> ();
			copiedItemsList = new List<string> ();
		}

		public override void ViewWillAppear (bool animated)
		{
			ContentSizeForViewInPopover = new SizeF (320, 500);

			base.ViewWillAppear (animated);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Title";

			ContentSizeForViewInPopover = new SizeF (320, 500);
			
			var editButton = new UIBarButtonItem (UIBarButtonSystemItem.Edit, this, null);

			NavigationItem.RightBarButtonItem = editButton;			
			NavigationItem.Title = "Countries";

			NavigationController.NavigationBar.SetBackgroundImage (null, UIBarMetrics.Default);
			NavigationController.ToolbarHidden = false;

			var segmentedControl = new UISegmentedControl (new object[] {"Bookmarks", "Recents", "Contacts"}) {
				Frame = new RectangleF (0.0f, 5.0f, 310.0f, 30.0f),
				SelectedSegment = 0
			};

			// Applies style on specified element
			GunmetalTheme.Apply (segmentedControl, true);

			var tabBarItem = new UIBarButtonItem (segmentedControl);
			var flexibleSpace = new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace);

			ToolbarItems = new UIBarButtonItem[] { flexibleSpace, tabBarItem, flexibleSpace};
						
			var countriesToLiveInArray = new [] { @"Iceland", @"Greenland", @"Switzerland", @"Norway", @"New Zealand", @"Greece", @"Rome", @"Ireland" };
			var countriesLivedInArray = new []{@"India", @"U.S.A"};
			
			var countriesToLiveInDict = new Dictionary<string,string[]> { { @"Countries", countriesToLiveInArray } };
			var countriesLivedInDict = new Dictionary<string,string[]> { { @"Countries", countriesLivedInArray } };
			
			itemsList.Add (countriesToLiveInDict);
			itemsList.Add (countriesLivedInDict);
						
			//Add the search bar
			searchBar = new UISearchBar (new RectangleF (0, 0, 320, 40)) {
				AutocorrectionType = UITextAutocorrectionType.No,
				WeakDelegate = this
			};

			tableView.TableHeaderView = searchBar;
			tableView.WeakDelegate = this;
			tableView.WeakDataSource = this;
			
			isSearching = false;
			isRowUserSelectable = true;
		}
		
		public Action<PopoverDemoController> OnDone; 
		partial void Done (NSObject sender)
		{
			if (OnDone != null) 
				OnDone (this);
		}

		[Export ("searchBarTextDidBeginEditing:")]
		void SearchBarTextDidBeginEditing (UISearchBar searchBar)
		{
			isSearching = true;
			isRowUserSelectable = false;
			tableView.ScrollEnabled = false;
			
			//Add the done button.
			NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Done, DoneButtonClicked);
		}
		
		[Export ("searchBar:textDidChange:")]
		void SearchBarTextDidChange (UISearchBar searchBar, string searchText)
		{
			//Remove all objects first.
			copiedItemsList.Clear ();

			var isSearchTextSpecified = searchText.Length > 0;

			isSearching = isSearchTextSpecified;
			isRowUserSelectable = isSearchTextSpecified;
			tableView.ScrollEnabled = isSearchTextSpecified;

			if (isSearchTextSpecified)
				SearchTableView ();
			
			tableView.ReloadData ();
		}
		
		[Export ("searchBarSearchButtonClicked:")]
		void SearchBarSearchButtonClicked (UISearchBar searchBar)
		{
			SearchTableView ();
		}
		
		void SearchTableView ()
		{
			var searchText = searchBar.Text;
			var foundItems = new List<string> ();
			
			foreach (var dictionary in itemsList)
			{
				var array = dictionary[@"Countries"];

				foundItems.AddRange (array);
			}
			
			foreach (var item in foundItems)
			{
				var index = item.IndexOf (searchText, StringComparison.CurrentCultureIgnoreCase);
				
				if (index >= 0)
					copiedItemsList.Add (item);
			}

			foundItems = null;
		}
		
		void DoneButtonClicked (object sender, EventArgs e)
		{
			searchBar.Text = "";
			searchBar.ResignFirstResponder ();

			var editItem = new UIBarButtonItem (UIBarButtonSystemItem.Edit, null);
			NavigationItem.RightBarButtonItem = editItem;

			tableView.ScrollEnabled = true;			
			isRowUserSelectable = true;
			isSearching = false;

			tableView.ReloadData ();
		}

		[Export ("numberOfSectionsInTableView:")]
		int NumberOfSectionsInTableView (UITableView tableView)
		{
			return isSearching ? 1 : itemsList.Count;
		}
		
		[Export ("tableView:numberOfRowsInSection:")]
		int TableViewNumberOfRowsInSection (UITableView tableView, int section)
		{
			if (isSearching) {
				return copiedItemsList.Count;
			} else {
				//Number of rows it should expect should be based on the section
				var dictionary = itemsList[section];
				var array = dictionary[@"Countries"];

				return array.Length;
			}
		}
		
		[Export ("tableView:viewForHeaderInSection:")]
		UIView TableViewViewForHeaderInSection (UITableView tableView, int section)
		{
			var tableHeaderView = new UIImageView (UIImage.FromFile ("GunmetalImages/popover-table-header.png"));
			
			var titleLabel = new UILabel (new RectangleF (10, 0, 200, 20)) {
				Text = section == 0 ? "Countries to visit" : "Countries visited",
				TextColor = UIColor.Gray,
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear,
				Font = UIFont.BoldSystemFontOfSize (15.0f)
			};
			
			tableHeaderView.AddSubview (titleLabel);
			
			return tableHeaderView;
		}
		
		[Export ("tableView:cellForRowAtIndexPath:")]
		UITableViewCell TableViewCellForRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (CellKey);
			
			if (isSearching) {
				cell.TextLabel.Text = copiedItemsList[indexPath.Row];
			} else {
				//First get the dictionary object
				var dictionary = itemsList[indexPath.Section];
				var countries = dictionary[@"Countries"];

				var cellValue = countries[indexPath.Row];

				cell.TextLabel.Text = cellValue;
			}
			
			return cell;
		}
		
		[Export ("tableView:willSelectRowAtIndexPath:")]
		NSIndexPath TableViewWillSelectRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{
			return isRowUserSelectable ? indexPath : null;
		}
		
		[Export ("tableView:didSelectRowAtIndexPath:")]
		void TableViewDidSelectRowAtIndexPath (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);
		}
	}
}
