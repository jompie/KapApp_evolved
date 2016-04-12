﻿
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
	[Activity (Label = "AdviesMaken")]			
	public class AdviesMaken : Activity
	{
		BeheerKledingstukken bk = new BeheerKledingstukken();
		BeheerAdvies ba = new BeheerAdvies ();
		BeheerIngelogd bi = new BeheerIngelogd ();

		EditText adviesOmschrijving;
		Button btnBevestigen;

		Spinner spinBovenlichaam;
		Spinner spinBenen;
		Spinner spinSchoenen;
		Spinner spinAccessoires;
		Spinner spinGeslacht;
		Spinner spinOogkleur;
		Spinner spinHaarkleur;
		Spinner spinOndertoon;
		Spinner spinKleurtype;
		Spinner spinLichaamstype;

		private string bovenlichaam;
		private string benen;
		private string schoenen;
		private string accessoires;

		private string geslacht;
		private string oogkleur;
		private string haarkleur;
		private string ondertoon;
		private string kleurtype;
		private string lichaamstype;

		private Array geslachtlijst = new string[]{"Man", "Vrouw"};
		private Array oogkleurlijst = new string[]{"Blauw", "Groen", "Bruin"};
		private Array haarkleurlijst = new string[]{"Blond", "Bruin", "Rood", "Zwart"};
		private Array ondertoonlijst = new string[]{"Koel", "Warm"};
		private Array kleurtypelijst = new string[]{"Lente", "Zomer", "Herfst", "Winter"};
		private Array Lichaamstypelijst = new string[]{"Smal", "Gemiddeld", "Fors"};

		private string ingelogdAls;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.AdviesMakenScherm);

			adviesOmschrijving = FindViewById<EditText> (Resource.Id.txt_adviesOmschrijving);

			spinBovenlichaam = FindViewById<Spinner> (Resource.Id.spinner_bovenlichaam);
			spinBenen = FindViewById<Spinner> (Resource.Id.spinner_benen);
			spinSchoenen = FindViewById<Spinner> (Resource.Id.spinner_schoenen);
			spinAccessoires = FindViewById<Spinner> (Resource.Id.spinner_accessoires);

			spinGeslacht = FindViewById<Spinner> (Resource.Id.spinner_geslacht);
			spinOogkleur = FindViewById<Spinner> (Resource.Id.spinner_oog);
			spinHaarkleur = FindViewById<Spinner> (Resource.Id.spinner_haar);
			spinOndertoon = FindViewById<Spinner> (Resource.Id.spinner_huid);
			spinKleurtype = FindViewById<Spinner> (Resource.Id.spinner_kleurType);
			spinLichaamstype = FindViewById<Spinner> (Resource.Id.spinner_lichaam);

			bk.InsertStandaardKledingstukken ();

			Array bovenlichaamlijst = bk.GetKledingstukkenArray ("Bovenlichaam");
			Array benenlijst = bk.GetKledingstukkenArray ("Benen");
			Array schoenenlijst = bk.GetKledingstukkenArray ("Schoenen");
			Array accessoireslijst = bk.GetKledingstukkenArray ("Accessoires");



			zetInSpinner (bovenlichaamlijst, spinBovenlichaam);
			zetInSpinner (benenlijst, spinBenen);
			zetInSpinner (schoenenlijst, spinSchoenen);
			zetInSpinner (accessoireslijst, spinAccessoires);

			zetInSpinner (geslachtlijst, spinGeslacht);
			zetInSpinner (oogkleurlijst, spinOogkleur);
			zetInSpinner (haarkleurlijst, spinHaarkleur);
			zetInSpinner (ondertoonlijst, spinOndertoon);
			zetInSpinner (kleurtypelijst, spinKleurtype);
			zetInSpinner (Lichaamstypelijst, spinLichaamstype);

			spinBovenlichaam.ItemSelected += (sender, e) => {
					bovenlichaam = spinnerWaardeSelectie(sender, e);
					};

			spinBenen.ItemSelected += (sender, e) => {
				benen = spinnerWaardeSelectie(sender, e);
			};

			spinSchoenen.ItemSelected += (sender, e) => {
				schoenen = spinnerWaardeSelectie(sender, e);
			};

			spinAccessoires.ItemSelected += (sender, e) => {
				accessoires = spinnerWaardeSelectie(sender, e);
			};

			spinGeslacht.ItemSelected += (sender, e) => {
				geslacht = spinnerWaardeSelectie(sender, e);
			};

			spinOogkleur.ItemSelected += (sender, e) => {
				oogkleur = spinnerWaardeSelectie(sender, e);
			};

			spinHaarkleur.ItemSelected += (sender, e) => {
				haarkleur = spinnerWaardeSelectie(sender, e);
			};

			spinOndertoon.ItemSelected += (sender, e) => {
				ondertoon = spinnerWaardeSelectie(sender, e);
			};

			spinKleurtype.ItemSelected += (sender, e) => {
				kleurtype = spinnerWaardeSelectie(sender, e);
			};

			spinLichaamstype.ItemSelected += (sender, e) => {
				lichaamstype = spinnerWaardeSelectie(sender, e);
			};
				
			ingelogdAls = bi.GetIngelogd ();

			btnBevestigen = FindViewById<Button> (Resource.Id.btn_adviesMakenBevestigen);
			btnBevestigen.Click += delegate {
				ba.InsertAdvies(adviesOmschrijving.Text, benen, bovenlichaam, schoenen, accessoires, geslacht+oogkleur+haarkleur+ondertoon+kleurtype+lichaamstype, ingelogdAls);
				Toast.MakeText(this, "Advies Succesvol aangemaakt", ToastLength.Short).Show();
				StartActivity(typeof(StylistActivity));
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
			
	}
}
