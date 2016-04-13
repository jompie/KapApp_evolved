
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
using System.Collections;

namespace KapApp_evolved
{
	[Activity (Label = "BekijkHistorie")]			
	public class BekijkHistorie : Activity
	{
		BeheerFavorieten bf = new BeheerFavorieten();
		BeheerIngelogd bi = new BeheerIngelogd ();
		
		Button btnTerug;
		ListView lstFavorieten;

		List<string> favorieten;
		private string ingelogdAls;
		
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.HistorieScherm);

			ingelogdAls = bi.GetIngelogd ();
			favorieten = bf.GetFavorieten (ingelogdAls);
			string f = favorieten.Count.ToString();
			Toast.MakeText (this, f+ingelogdAls, ToastLength.Short).Show ();
			lstFavorieten = FindViewById<ListView> (Resource.Id.list_favorieten);

			ArrayAdapter adapter = new ArrayAdapter<String> (this,Android.Resource.Layout.SimpleListItem1, favorieten);
			lstFavorieten.Adapter = adapter;

			btnTerug = FindViewById<Button> (Resource.Id.btn_historieTerug);
			btnTerug.Click += delegate {
				StartActivity(typeof(MainActivity));
			};

		}
	}
}

