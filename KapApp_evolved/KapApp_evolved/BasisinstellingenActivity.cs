
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
	[Activity (Label = "BasisinstellingenActivity")]			
	public class BasisinstellingenActivity : Activity
	{
		BeheerBasisinstellingen bb = new BeheerBasisinstellingen();
		BeheerIngelogd bi = new BeheerIngelogd();

		Spinner spinGeslacht;
		Spinner spinOogkleur;
		Spinner spinHaarkleur;
		Spinner spinOndertoon;
		Spinner spinLichaamstype;

		Button btnBevestig;
		Button btnTerug;

		private Array geslachtlijst = new string[]{"Man", "Vrouw"};
		private Array oogkleurlijst = new string[]{"Blauw", "Groen", "Bruin"};
		private Array haarkleurlijst = new string[]{"Blond", "Bruin", "Rood", "Zwart"};
		private Array ondertoonlijst = new string[]{"Koel", "Warm"};
		private Array Lichaamstypelijst = new string[]{"Smal", "Gemiddeld", "Fors"};

		string geslacht;
		string oogkleur;
		string haarkleur;
		string ondertoon;
		string lichaamstype;
		string gebruikersnaamKlant;
		string kleurtype;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.BasisinstellingenScherm);

			// Spinners
			spinGeslacht = FindViewById<Spinner> (Resource.Id.spinner_geslacht);
			spinOogkleur = FindViewById<Spinner> (Resource.Id.spinner_oog);
			spinHaarkleur = FindViewById<Spinner> (Resource.Id.spinner_haar);
			spinOndertoon = FindViewById<Spinner> (Resource.Id.spinner_huid);
			spinLichaamstype = FindViewById<Spinner> (Resource.Id.spinner_lichaam);

			// Set content spinners
			zetInSpinner (geslachtlijst, spinGeslacht);
			zetInSpinner (oogkleurlijst, spinOogkleur);
			zetInSpinner (haarkleurlijst, spinHaarkleur);
			zetInSpinner (ondertoonlijst, spinOndertoon);
			zetInSpinner (Lichaamstypelijst, spinLichaamstype);

//			geslacht = spinnerSelectieGemaakt (spinGeslacht, geslacht);
//			oogkleur = spinnerSelectieGemaakt (spinOogkleur, oogkleur);
//			haarkleur = spinnerSelectieGemaakt (spinHaarkleur, haarkleur);
//			ondertoon =  spinnerSelectieGemaakt (spinOndertoon, ondertoon);
//			kleurtype = spinnerSelectieGemaakt (spinkleurtype, kleurtype);
//			lichaamstype = spinnerSelectieGemaakt (spinLichaamstype, lichaamstype);

			spinGeslacht.ItemSelected += (sender, e) =>
			{
				geslacht = spinnerWaardeSelectie(sender, e);
				Toast.MakeText(this, geslacht, ToastLength.Short).Show();
			};

			spinOogkleur.ItemSelected += (sender, e) =>
			{
				oogkleur = spinnerWaardeSelectie(sender, e);
				Toast.MakeText(this, oogkleur, ToastLength.Short).Show();
			};

			spinHaarkleur.ItemSelected += (sender, e) =>
			{
				haarkleur = spinnerWaardeSelectie(sender, e);
				Toast.MakeText(this, haarkleur, ToastLength.Short).Show();
			};

			spinOndertoon.ItemSelected += (sender, e) =>
			{
				ondertoon = spinnerWaardeSelectie(sender, e);
				Toast.MakeText(this, ondertoon, ToastLength.Short).Show();
			};

			spinLichaamstype.ItemSelected += (sender, e) =>
			{
				lichaamstype = spinnerWaardeSelectie(sender, e);
				Toast.MakeText(this, lichaamstype, ToastLength.Short).Show();
			};

			gebruikersnaamKlant = bi.GetIngelogd ();
			kleurtype = bepaalKleurtype (oogkleur, haarkleur, ondertoon);

			btnBevestig = FindViewById<Button> (Resource.Id.btn_basisBevestigen);
			btnBevestig.Click += delegate {
				//Zet waarden van spinners in een databes
				bb.InsertBasisinstelling(geslacht, oogkleur, haarkleur, ondertoon, kleurtype, lichaamstype, gebruikersnaamKlant);
				StartActivity(typeof(KlantActivity));
				Toast.MakeText(this, "Basisinstellingen opgeslagen", ToastLength.Short).Show();
			};
			btnTerug = FindViewById<Button> (Resource.Id.btn_basisTerug);
			btnTerug.Click += delegate {
				StartActivity(typeof(KlantActivity));
			};

		}

		private void zetInSpinner(Array lijst , Spinner spinner)
		{
			ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, lijst);
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
			spinner.Adapter = adapter;
		}

		private string spinnerWaardeSelectie (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			string value = spinner.GetItemAtPosition (e.Position).ToString();
			return value;
		}

		private string bepaalKleurtype(string oog, string haar, string ondertoon)
		{
			if (ondertoon == "Koel")
			{
				if (oog == "Blauw" | oog == "Groen")
					return "Zomer";
				
				else
					return "Winter";
			}
			if (ondertoon == "Warm") 
			{
				if (haar == "Rood" | oog == "Blauw" | oog =="Groen")
					return "Lente";
				else 
					return "Herfts";
			}
			return null;

		}
//		private string spinnerSelectieGemaakt(Spinner spinner, string waarde)
//		{
//			spinner.ItemSelected += (sender, e) =>
//			{
//				string waarde = spinnerWaardeSelectie(sender, e);
//				return waarde;
//			};
//		}
	}
}

