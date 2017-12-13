using System;
using System.IO;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite.Net;
using SQLite.Net.Platform.SQLCipher.XamarinAndroid;

namespace sqlcipherandroidpcldemo
{
	[Activity (Label = "sqlcipher-android-pcl-demo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		private SQLiteConnection connection;
		private String password = "test";
		private TextView Content;

		public class T1
		{
			public String a { get; set; }
			public String b { get; set; }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			Button clearButton = FindViewById<Button> (Resource.Id.clear);
			Button runDemoButton = FindViewById<Button> (Resource.Id.runDemo);
			Content = FindViewById<TextView> (Resource.Id.Content);
			clearButton.Click += Clear;
			runDemoButton.Click += RunDemo;
		}

		private void Clear(object sender, EventArgs e)
		{
			if (connection == null)
				CreateConnection ();
			connection.DeleteAll<T1> ();
			DisplayContent ();
		}

		private void RunDemo(object sender, EventArgs e)
		{
			if (connection == null)
				CreateConnection ();
			connection.Insert (new T1{ a = "one for the money", b = "two for the show" });
			DisplayContent ();
		}

		private void DisplayContent()
		{
			var buffer = new StringBuilder ();
			var records = connection.Query<T1> ("select * from T1;", new object[]{ });
			records.ForEach (record => buffer.AppendFormat ("a:{0} b:{1}{2}", record.a, record.b, System.Environment.NewLine));
			Content.Text = buffer.ToString ();
		}

		private void CreateConnection()
		{
			var databasePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "sqlcipher.db");
			connection = new SQLiteConnection (new SQLitePlatformAndroid (password), databasePath);
			connection.CreateTable<T1> ();
		}
	}
}