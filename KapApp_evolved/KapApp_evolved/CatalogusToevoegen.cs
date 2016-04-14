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
using Android.Graphics;
using Android.Provider;
using Android.Content.PM;

namespace KapApp_evolved
{
	[Activity (Label = "CatalogusToevoegenActivity")]			
	public class CatalogusToevoegenActivity : Activity
	{
		public static class App {
			public static Java.IO.File _file;
			public static Java.IO.File _dir;
			public static Bitmap bitmap;
		}

		BeheerKledingstukken bk = new BeheerKledingstukken();

		Spinner spinKledingstype;
		Spinner spingeslacht;
		EditText txtOmschrijving;
		EditText txtPrijs;
		EditText txtKorting;
		ImageView imgVwPlaatje;
		Button plaatjeToevoegen;
		Button opslaan;
		Button btnAddPlaatje;
		Button btnTerug;

		private string kledingtype;
		string geslacht;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.CatalogusToevoegen);

			txtPrijs = FindViewById<EditText> (Resource.Id.editTextPrijs);
			txtOmschrijving = FindViewById<EditText> (Resource.Id.editTextOmschrijving);
			txtKorting = FindViewById<EditText> (Resource.Id.editTextKorting);

			spinKledingstype = FindViewById<Spinner> (Resource.Id.spinner_kledingstype);
			Array soortenLijst = new string[]{"Bovenlichaam", "Benen", "Schoenen", "Accesoires"};
			zetInSpinner (soortenLijst,spinKledingstype);
			spinKledingstype.ItemSelected += (sender, e) =>
			{
				kledingtype = spinnerWaardeSelectie(sender, e);
			};
			spingeslacht = FindViewById<Spinner> (Resource.Id.spinner_geslacht);
			Array geslachtLijst = new string[]{"Man", "Vrouw"};
			zetInSpinner (geslachtLijst,spingeslacht);

			plaatjeToevoegen = FindViewById<Button> (Resource.Id.btn_plaatjeToevoegen);
			plaatjeToevoegen.Click += delegate {
				//StartActivity(typeof(PlaatjeToevoegenKeuze));
				Intent imageIntent = new Intent ();
				imageIntent.SetType ("image/*");
				imageIntent.SetAction (Intent.ActionGetContent);
				StartActivityForResult (
					Intent.CreateChooser (imageIntent, "Select photo"), 0);
			};
			if (IsThereAnAppToTakePictures ())
			{
				CreateDirectoryForPictures ();

				Button camera = FindViewById<Button>(Resource.Id.btn_cameraToevoegen);
				camera.Click += TakeAPicture;
			}

			opslaan = FindViewById<Button> (Resource.Id.btn_productOpslaan);
			opslaan.Click += delegate {
				bool volledingIngevuld = controleerIngevuld(txtOmschrijving.Text, txtPrijs.Text, txtKorting.Text);
				if (volledingIngevuld) 
				{
					bool bestaatAl = bk.KledingstukBestaat (txtOmschrijving.Text);

					if (!bestaatAl) {
						string omschrijving;
						int prijs;
						int korting;
						omschrijving = txtOmschrijving.Text;
						prijs = Convert.ToInt32 (txtPrijs.Text);
						korting = Convert.ToInt32 (txtKorting.Text);
						bk.InsertKledingstuk (omschrijving, prijs, korting, kledingtype);
						Toast.MakeText (this, "Kledingstuk toegevoegd aan catalogus", ToastLength.Short).Show ();
						StartActivity (typeof(CatalogusToevoegenActivity));
					} 
					else
						Toast.MakeText (this, "Er bestaat al een kledingstuk met de naam: " + txtOmschrijving, ToastLength.Short).Show ();
					
				} else
					Toast.MakeText (this, "Gegevens zijn niet volledig ingevuld", ToastLength.Short).Show ();
			};

			btnAddPlaatje = FindViewById<Button> (Resource.Id.btn_plaatjeToevoegen);


			btnTerug = FindViewById<Button> (Resource.Id.btn_terug_WE);
			btnTerug.Click += delegate {
				StartActivity(typeof(WinkeleigenaarActivity));
			};



		}

		private bool controleerIngevuld(string omschrijving, string prijs, string korting)
		{
			if (omschrijving == "" | prijs == "" | korting == "")
				return false;
			else
				return true;
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

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == 0) {
				base.OnActivityResult (requestCode, resultCode, data);

				if (resultCode == Result.Ok) {
					ImageView imageView =
						FindViewById<ImageView> (Resource.Id.imgVwPlaatje);
					imageView.SetImageURI (data.Data);
				}
			}
			if (requestCode == 1){

				base.OnActivityResult (requestCode, resultCode, data);

				// Make it available in the gallery

				Intent mediaScanIntent = new Intent (Intent.ActionMediaScannerScanFile);
				Android.Net.Uri contentUri = Android.Net.Uri.FromFile (App._file);
				mediaScanIntent.SetData (contentUri);
				SendBroadcast (mediaScanIntent);

				// Display in ImageView. We will resize the bitmap to fit the display.
				// Loading the full sized image will consume to much memory
				// and cause the application to crash.
				imgVwPlaatje = FindViewById<ImageView>(Resource.Id.imgVwPlaatje);

				//				int height = Resources.DisplayMetrics.HeightPixels;
				//				int width = imgVwPlaatje.Height ;

				int height = 200;
				int width = 400 ;
				App.bitmap = App._file.Path.LoadAndResizeBitmap (width, height);
				if (App.bitmap != null) {
					imgVwPlaatje.SetImageBitmap (App.bitmap);
					App.bitmap = null;
				}

				// Dispose of the Java side bitmap.
				GC.Collect();

			}
		}

		private void CreateDirectoryForPictures ()
		{
			App._dir = new Java.IO.File (
				Android.OS.Environment.GetExternalStoragePublicDirectory (
					Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
			if (!App._dir.Exists ())
			{
				App._dir.Mkdirs( );
			}
		}
			

		private bool IsThereAnAppToTakePictures ()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				PackageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakeAPicture (object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			App._file = new Java.IO.File (App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
			intent.PutExtra (MediaStore.ExtraOutput, Android.Net.Uri.FromFile (App._file));
			StartActivityForResult (intent, 1);
		}
	}
}

