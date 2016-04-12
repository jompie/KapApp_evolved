
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
	[Activity (Label = "VerkoperToevoegen")]			
	public class VerkoperToevoegen : Activity
	{
		EditText txtNaam;
		EditText txtGebruikersnaam;
		EditText txtWachtwoord;
		EditText txtHerhaalWachtwoord;
		Button bevestigen;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.VerkoperToevoegen);

		}
	}
}

