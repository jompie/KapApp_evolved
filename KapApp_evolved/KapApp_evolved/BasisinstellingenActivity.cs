
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
		Spinner spinkleurtype;
		Spinner spinLichaamstype;

		Button btnBevestig;

		private Array geslachtlijst = new string[]{"Man", "Vrouw"};
		private Array oogkleurlijst = new string[]{"Blauw", "Groen", "Bruin"};
		private Array haarkleurlijst = new string[]{"Blond", "Bruin", "Rood", "Zwart"};
		private Array ondertoonlijst = new string[]{"Koel", "Warm"};
		private Array kleurtypelijst = new string[]{"Lente", "Zomer", "Herfst", "Winter"};
		private Array Lichaamstypelijst = new string[]{"Smal", "Gemiddeld", "Fors"};

		string geslacht;
		string oogkleur;
		string haarkleur;
		string ondertoon;
		string kleurtype;
		string lichaamstype;
		string gebruikersnaamKlant;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.BasisinstellingenScherm);

			// Spinners
			spinGeslacht = FindViewById<Spinner> (Resource.Id.spinner_geslacht);
			spinOogkleur = FindViewById<Spinner> (Resource.Id.spinner_oog);
			spinHaarkleur = FindViewById<Spinner> (Resource.Id.spinner_haar);
			spinOndertoon = FindViewById<Spinner> (Resource.Id.spinner_huid);
			spinkleurtype = FindViewById<Spinner> (Resource.Id.spinner_kleurType);
			spinLichaamstype = FindViewById<Spinner> (Resource.Id.spinner_lichaam);

			// Set content spinners
			zetInSpinner (geslachtlijst, spinGeslacht);
			zetInSpinner (oogkleurlijst, spinOogkleur);
			zetInSpinner (haarkleurlijst, spinHaarkleur);
			zetInSpinner (ondertoonlijst, spinOndertoon);
			zetInSpinner (kleurtypelijst, spinkleurtype);
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
			};

			spinOogkleur.ItemSelected += (sender, e) =>
			{
				oogkleur = spinnerWaardeSelectie(sender, e);		
			};

			spinHaarkleur.ItemSelected += (sender, e) =>
			{
				haarkleur = spinnerWaardeSelectie(sender, e);		
			};

			spinOndertoon.ItemSelected += (sender, e) =>
			{
				ondertoon = spinnerWaardeSelectie(sender, e);		
			};

			spinkleurtype.ItemSelected += (sender, e) =>
			{
				kleurtype = spinnerWaardeSelectie(sender, e);		
			};

			spinLichaamstype.ItemSelected += (sender, e) =>
			{
				lichaamstype = spinnerWaardeSelectie(sender, e);		
			};

			gebruikersnaamKlant = bi.GetIngelogd ();

			btnBevestig = FindViewById<Button> (Resource.Id.btn_basisBevestigen);
			btnBevestig.Click += delegate {
				//Zet waarden van spinners in een databes
				bb.InsertBasisinstelling(geslacht, oogkleur, haarkleur, ondertoon, kleurtype, lichaamstype, gebruikersnaamKlant);
				SetContentView(Resource.Layout.KlantScherm);
				Toast.MakeText(this, "Basisinstellingen opgeslagen", ToastLength.Short).Show();
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

