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
	[Activity (Label = "CatalogusToevoegenActivity")]			
	public class CatalogusToevoegenActivity : Activity
	{
		BeheerKledingstukken bk = new BeheerKledingstukken();

		Spinner spinKledingstype;
		EditText txtOmschrijving;
		EditText txtPrijs;
		EditText txtKorting;
		ImageView imgPlaatje;
		Button btnToevoegen;
		Button btnAddPlaatje;
		Button btnTerug;

		private string kledingtype;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.CatalogusToevoegen);

			txtPrijs = FindViewById<EditText> (Resource.Id.txt_prijsKledingstuk);
			txtOmschrijving = FindViewById<EditText> (Resource.Id.txt_omschrijvingKledingstuk);
			txtKorting = FindViewById<EditText> (Resource.Id.txt_kortingKledingstuk);

			spinKledingstype = FindViewById<Spinner> (Resource.Id.spinner_kledingstype);
			Array soortenLijst = new string[]{"Bovenlichaam", "Benen", "Schoenen", "Accesoires"};
			zetInSpinner (soortenLijst,spinKledingstype);
			spinKledingstype.ItemSelected += (sender, e) =>
			{
				kledingtype = spinnerWaardeSelectie(sender, e);
			};

			btnToevoegen = FindViewById<Button> (Resource.Id.btn_toevoegenKledingstuk);
			btnToevoegen.Click += delegate {
				bool bestaatAl = bk.KledingstukBestaat(txtOmschrijving.Text);
				if(!bestaatAl)
				{
					int prijs = Convert.ToInt32(txtPrijs.Text);
					int korting = Convert.ToInt32(txtKorting.Text);
					bk.InsertKledingstuk(txtOmschrijving.Text, prijs, korting , kledingtype);
					Toast.MakeText(this, "Kledingstuk toegevoegd aan catalogus", ToastLength.Short).Show();
					StartActivity(typeof(WinkeleigenaarActivity));
				}
				else
					Toast.MakeText(this, "Er bestaat al een kledingstuk met de naam: "+txtOmschrijving.Text, ToastLength.Short).Show();
			};

			btnAddPlaatje = FindViewById<Button> (Resource.Id.btn_plaatjeToevoegen);


			btnTerug = FindViewById<Button> (Resource.Id.btn_terug);




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

