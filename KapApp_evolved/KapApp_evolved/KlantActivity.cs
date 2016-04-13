
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

namespace KapApp_evolved
{
	[Activity (Label = "KlantActivity")]			
	public class KlantActivity : Activity
	{
		BeheerIngelogd bi = new BeheerIngelogd ();

		TextView txtWelkom;
		Button btn_basisinstellinen;
		Button btn_krijgAdvies;
		Button btn_historie;
		Button btn_logUit;

		private string ingelogdAls;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.KlantScherm);

			ingelogdAls =  bi.GetIngelogd();
			//Geef naam van klant weer in welkomsbericht
			txtWelkom = FindViewById<TextView>(Resource.Id.txt_klantWelkom);
			txtWelkom.Text = "Welkom "+ingelogdAls+",";

			btn_basisinstellinen = FindViewById<Button> (Resource.Id.btn_klantBasisinstellingen);
			btn_basisinstellinen.Click += delegate {
				StartActivity(typeof(BasisinstellingenActivity));
			};

			btn_krijgAdvies = FindViewById<Button> (Resource.Id.btn_klantKrijgAdvies);
			btn_krijgAdvies.Click += delegate {
				StartActivity(typeof(KrijgAdviesActivity));
			};

			btn_historie = FindViewById<Button> (Resource.Id.btn_klantHistorie);
			btn_historie.Click += delegate {
				StartActivity(typeof(BekijkHistorie));
			};

			btn_logUit = FindViewById<Button> (Resource.Id.btn_klantLogUit);
			btn_logUit.Click += delegate {
				StartActivity(typeof(MainActivity));
			};
		}
	}
}

