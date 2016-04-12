
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
using ZXing;
using Android.Graphics;

namespace KapApp_evolved
{
	[Activity (Label = "KrijgAdviesActivity")]			
	public class KrijgAdviesActivity : Activity
	{
		Button btnQR;
		ImageView imgQR;
		string kortingscode;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.KrijgAdviesScherm);

			btnQR = FindViewById<Button> (Resource.Id.btn_adviesGenereerQR);
			imgQR = FindViewById<ImageView> (Resource.Id.img_adviesQR);
			kortingscode = "2 halen 1 betalen. Geldt voor alle onderbroeken en sokken.";
			btnQR.Click += delegate {
				imgQR.SetImageBitmap(GetQRCode(kortingscode));
			};
		
		}

		private Bitmap GetQRCode(string tekst)
		{
			var writer = new BarcodeWriter {
				Format = BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions {
					Height = 600,
					Width = 600
				}
			};
			return writer.Write (tekst);
		}
	}
}

