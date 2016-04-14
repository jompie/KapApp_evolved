
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
	[Activity (Label = "WinkeleigenaarActivity")]			
	public class WinkeleigenaarActivity : Activity
	{
		Button btnVerkoperToevoegen;
		Button btnKledingtoevoegen;
		Button btnLogUit;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.WinkeleigenaarScherm);

			btnLogUit = FindViewById<Button> (Resource.Id.btn_eigenaarLogUit);
			btnLogUit.Click += delegate {
				StartActivity(typeof(MainActivity));
			};

			btnVerkoperToevoegen = FindViewById<Button> (Resource.Id.btn_eigenaarToevoegenVerkoper);
			btnVerkoperToevoegen.Click += delegate {
				
				StartActivity(typeof(VerkoperToevoegen));
			};

			btnKledingtoevoegen = FindViewById<Button> (Resource.Id.btn_eigenaarCatUploaden);
			btnKledingtoevoegen.Click += delegate {
				StartActivity(typeof(CatalogusToevoegenActivity));
			};
		}
	}
}

