
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

namespace KapApp_evolved
{
	[Activity (Label = "StylistActivity")]			
	public class StylistActivity : Activity
	{
		Button btn_stelAdviesSamen;
		Button btn_browseCatalogus;
		Button btnLogUit;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.StylistScherm);

			// Stel Advies Samen buttn.
			// Als deze button wordt ingedruk, wordt een scherm geladen waar de stylist een advies kan samenstellen
			btn_stelAdviesSamen = FindViewById<Button> (Resource.Id.btn_stylistStelAdviesSamen);
			btn_stelAdviesSamen.Click += delegate {
				StartActivity(typeof(AdviesMaken));
			};

			// Browse catalogus
			// Als deze button wordt ingedrukt, wordt een scherm geladen waar de stylist catalogussen kan browsen.
			btn_browseCatalogus = FindViewById<Button>(Resource.Id.btn_stylistBrowse);
			btn_browseCatalogus.Click += delegate {
				//StartActivity(typeoff());
			};
			btnLogUit = FindViewById<Button> (Resource.Id.btn_stylistLogUit);
			btnLogUit.Click += delegate {
				StartActivity(typeof(MainActivity));
			};
		}
	}
}

