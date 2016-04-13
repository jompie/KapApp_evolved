
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
	[Activity (Label = "BekijkHistorie")]			
	public class BekijkHistorie : Activity
	{
		Button btnTerug;
		
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.HistorieScherm);

			btnTerug = FindViewById<Button> (Resource.Id.btn_historieTerug);
			btnTerug.Click += delegate {
				StartActivity(typeof(MainActivity));
			};

		}
	}
}

