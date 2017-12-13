using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace GunmetalTheme.Controls.iOS.ProgressBar {
	public class ADVPopoverProgressBar : UIView
	{
		const float LEFT_PADDING = 5.0f;
		const float RIGHT_PADDING = 3.0f;
		const float PERCENT_VIEW_WIDTH = 30.0f;
		const float MIN_WIDTH = 55.0f;

		UIView percentView;    
		UIImageView bgImageView;
		UIImageView progressImageView;
		UIImage progressFillImage;

		float progress;

		[Export("initWithFrame:andProgressBarColor:")]
		public ADVPopoverProgressBar(RectangleF frame, ADVProgressBarColor barColor): base(frame)
		{
			bgImageView = new UIImageView(new RectangleF(0, 0, frame.Width, 24));
			
			bgImageView.Image = UIImage.FromFile("progress-track.png");
			this.AddSubview(bgImageView);
			
			progressFillImage = UIImage.FromFile("progress-fill.png").CreateResizableImage(new UIEdgeInsets(0, 20, 0, 40));
			progressImageView = new UIImageView(new RectangleF(-2, 0, 0, 32));
			this.AddSubview(progressImageView);
			
			percentView = new UIView(new RectangleF(5, 4, PERCENT_VIEW_WIDTH, 15));
			percentView.Hidden = true;
			
			UILabel percentLabel = new UILabel(new RectangleF(0, 0, PERCENT_VIEW_WIDTH, 14));
			percentLabel.Tag = 1;
			percentLabel.Text = "0%";
			percentLabel.BackgroundColor = UIColor.Clear;
			percentLabel.TextColor = UIColor.Black;
			percentLabel.Font = UIFont.BoldSystemFontOfSize(11);
			percentLabel.TextAlignment = UITextAlignment.Center;
			percentLabel.AdjustsFontSizeToFitWidth = true;
			percentView.AddSubview(percentLabel);
			
			this.AddSubview(percentView);
		}

		public void SetProgress(float theProgress)
		{
			if (this.progress != theProgress) {        
				if (theProgress >= 0 && theProgress <= 1) {            
					progress = theProgress;
					
					progressImageView.Image = progressFillImage;
					
					RectangleF frame = progressImageView.Frame;
					
					frame.X = -3;
					frame.Y = -5;
					frame.Height = bgImageView.Frame.Height + 3;
					float width = (bgImageView.Frame.Width + 6 - MIN_WIDTH) * progress;
					width += MIN_WIDTH;
					
					float percentage = progress*100;
					bool display = percentage == 0;
					
					frame.Width = width;
					
					progressImageView.Frame = frame.Integral();
					progressImageView.Hidden = display;
					
					float leftEdge = width - PERCENT_VIEW_WIDTH - 12;
					percentView.Frame = new RectangleF(leftEdge, percentView.Frame.Y, PERCENT_VIEW_WIDTH, percentView.Frame.Height).Integral();
					
					UILabel percentLabel = (UILabel)percentView.ViewWithTag(1);
					percentLabel.Text = string.Format("{0}%", (int)percentage);
					percentView.Hidden = display;
				}
			}
		}
	}
}
