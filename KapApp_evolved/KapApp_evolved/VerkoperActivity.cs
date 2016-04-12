
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using CC;
using ZXing;

namespace KapApp_evolved
{
	[Activity (Label = "VerkoperActivity")]			
	public class VerkoperActivity : Activity
	{
		Button btnScanKorting;
		private string kortingscode;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.VerkoperScherm);

			btnScanKorting = FindViewById<Button> (Resource.Id.btn_verkoperScanQR);
			btnScanKorting.Click += async(sender, e) => {
				var scanner = new ZXing.Mobile.MobileBarcodeScanner(this);
				var result = await scanner.Scan();
				if(result != null)
				{
					kortingscode = result.ToString();
					Toast.MakeText(this, kortingscode, ToastLength.Short).Show();
				}
			};

		}
	}
}
		
	


