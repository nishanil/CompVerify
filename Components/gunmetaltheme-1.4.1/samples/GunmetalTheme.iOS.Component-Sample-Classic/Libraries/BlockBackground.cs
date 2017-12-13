using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace ThemeSample
{
	public class BlockBackground : UIWindow
	{
		UIWindow _previousKeyWindow;
		UIInterfaceOrientation lastOrientation;

		static BlockBackground _sharedInstance;
		public static BlockBackground SharedInstance { get { return _sharedInstance; } }
		public UIImage BackgroundImage;
		public bool VignetteBackground;

		static BlockBackground()
		{
			if (_sharedInstance == null)
				_sharedInstance = new BlockBackground();
		}
		/*
		- (id)copyWithZone:(NSZone*)zone
		{
			return this;
		}
		
		- (id)retain
		{
			return this;
		}
		
		- (unsigned)retainCount
		{
			return UINT_MAX;
		}
		
		- (oneway void)release
		{    
		}
		
		- (id)autorelease
		{
			return this;
		}
		*/

		public BlockBackground():base(UIScreen.MainScreen.Bounds)
		{
			this.WindowLevel = UIWindow.LevelStatusBar;
			this.Hidden = true;
			this.UserInteractionEnabled = false;
			this.BackgroundColor = UIColor.FromWhiteAlpha(0.4f, 0.5f);
			this.VignetteBackground = false;

			NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidChangeStatusBarFrameNotification, StatusBarDidChangeFrame);
		}
		
		public void AddToMainWindow(UIView view)
		{
			if (this.Hidden)
			{
				_previousKeyWindow = UIApplication.SharedApplication.KeyWindow;
				this.Alpha = 0.0f;
				this.Hidden = false;
				this.UserInteractionEnabled = true;
				this.MakeKeyWindow();
			}
			
			if (this.Subviews.Length > 0)
			{
				this.Subviews.Last().UserInteractionEnabled = false;
			}
			
			if (BackgroundImage != null)
			{
				UIImageView backgroundView = new UIImageView(BackgroundImage);
				backgroundView.Frame = this.Bounds;
				backgroundView.ContentMode = UIViewContentMode.ScaleToFill;
				this.AddSubview(backgroundView);
				//[backgroundView release];
				//[_backgroundImage release];
				BackgroundImage = null;
			}  
			this.StatusBarDidChangeFrame(null);
			view.AutoresizingMask = UIViewAutoresizing.FlexibleBottomMargin | UIViewAutoresizing.FlexibleLeftMargin
				| UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleTopMargin;
			this.AddSubview(view);
		}
		
		public void ReduceAlphaIfEmpty()
		{
			if (this.Subviews.Length == 1 || (this.Subviews.Length == 2 && (this.Subviews)[0] is UIImageView))
			{
				this.Alpha = 0.0f;
				this.UserInteractionEnabled = false;
			}
		}
		
		public void RemoveView(UIView view)
		{
			view.RemoveFromSuperview();
			
			if (this.Subviews.Length == 0)
			{
				this.Hidden = true;
				_previousKeyWindow.MakeKeyWindow();
				//_previousKeyWindow release];
				_previousKeyWindow = null;
			}
			else
			{
				UIView topView = this.Subviews.Last();
				if (topView is UIImageView)
				{
					// It's a background. Remove it too
					topView.RemoveFromSuperview();
				}

				this.Subviews.Last().UserInteractionEnabled = true;
			}
		}
		
		public override void Draw (RectangleF rect)
		{
			if (BackgroundImage != null || !VignetteBackground) return;
			var context = UIGraphics.GetCurrentContext();
			
			float[] locations = new float[2] {0.0f, 1.0f};
			float[] colors = new float[8] {0.0f,0.0f,0.0f,0.0f,0.0f,0.0f,0.0f,0.75f}; 
			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
			CGGradient gradient = new CGGradient(colorSpace, colors, locations);
			//CGColorSpaceRelease(colorSpace);
			
			PointF center = new PointF(this.Bounds.Width/2, this.Bounds.Height/2);
			float radius = Math.Min(this.Bounds.Width , this.Bounds.Height);
			context.DrawRadialGradient(gradient, center, 0, center, radius, CGGradientDrawingOptions.DrawsAfterEndLocation);
			//CGGradientRelease(gradient);
		}

		CGAffineTransform TransformForOrientation(UIInterfaceOrientation orientation)
		{
			switch (orientation) {
			case UIInterfaceOrientation.LandscapeLeft:
				return CGAffineTransform.MakeRotation(-(float)Math.PI/2);
				
			case UIInterfaceOrientation.LandscapeRight:
				return CGAffineTransform.MakeRotation((float)Math.PI/2);
				
			case UIInterfaceOrientation.PortraitUpsideDown:
				return CGAffineTransform.MakeRotation((float)Math.PI);
				
			case UIInterfaceOrientation.Portrait:
			default:
				return CGAffineTransform.MakeRotation(0);
			}
		}

		void StatusBarDidChangeFrame(NSNotification notification)
		{
			UIInterfaceOrientation orientation = UIApplication.SharedApplication.StatusBarOrientation;
			if (orientation == lastOrientation)	return;
			lastOrientation = orientation;
			
			this.Transform = this.TransformForOrientation(orientation);
			
			RectangleF frame = UIScreen.MainScreen.Bounds;
			frame.Location = new PointF(0, 0);
			this.Frame = frame;
		}

	}
}
