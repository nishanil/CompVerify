using System;
using System.Collections.Generic;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif
using Xamarin.Themes;

namespace ThemeSample {
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {
		// class-level declarations
		public override UIWindow Window { get; set; }

		public static bool HideStatusBar {get {return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;}}
		//public static bool HideStatusBar {get {return false;}}

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// Applies theme styles globally
			GunmetalTheme.Apply ();

			//UIButton.Appearance.TintColor = UIColor.White;

			var idiom = UIDevice.CurrentDevice.UserInterfaceIdiom;

			if (idiom == UIUserInterfaceIdiom.Pad) 
				InitPad ();
			else 
				ConfigureiPhoneTabBar ();

			app.SetStatusBarStyle (UIStatusBarStyle.LightContent, true);
			UIApplication.SharedApplication.StatusBarHidden = HideStatusBar;
			
			// Override point for customization after application launch.
			return true;
		}

		void InitPad ()
		{
			var splitViewController = (UISplitViewController)this.Window.RootViewController;
			
			splitViewController.WeakDelegate = splitViewController.ViewControllers.Last ();
		}
		
		
		void ConfigureiPhoneTabBar ()
		{
			var tabBarController = (UITabBarController)this.Window.RootViewController;
			
			var tab1 = tabBarController.ViewControllers[0];
			var tab2 = tabBarController.ViewControllers[1];
			var tab3 = tabBarController.ViewControllers[2];
			var tab4 = tabBarController.ViewControllers[3];

			ConfigureTabBarItemWithImageName ("tab-icon1.png", "Elements", tab1);
            ConfigureTabBarItemWithImageName ("tab-icon2.png", "Buttons", tab2);
            ConfigureTabBarItemWithImageName ("tab-icon3.png", "List", tab3);
            ConfigureTabBarItemWithImageName ("tab-icon4.png", "Other", tab4);
		}
		
		void ConfigureTabBarItemWithImageName (string imageUrl, string title, UIViewController viewController)
		{
			var icon = UIImage.FromFile (imageUrl);
			var tabBarItem = new UITabBarItem (title, icon, 0);

			tabBarItem.SetFinishedImages (icon, icon);

			viewController.TabBarItem = tabBarItem;
		}
	}
}

