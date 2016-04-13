
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
using ZXing;
using Android.Graphics;
using CC;

namespace KapApp_evolved
{
	[Activity (Label = "KrijgAdviesActivity")]			
	public class KrijgAdviesActivity : Activity
	{
		BeheerAdvies ba = new BeheerAdvies ();
		BeheerIngelogd bi = new BeheerIngelogd ();
		BeheerBasisinstellingen bb = new BeheerBasisinstellingen ();
		BeheerKledingstukken bk = new BeheerKledingstukken ();
		BeheerFavorieten bf = new BeheerFavorieten ();

		Button btnQR;
		ImageView imgQR;
		string kortingscode;

		TextView txtAdviesOmschrijving;
		TextView txtStylistNaam;
		TextView txtBovenlichaam;
		TextView txtBenen;
		TextView txtSchoenen;
		TextView txtAccessoires;

		Button btnInfoBovenlichaam;
		Button btnInfoBenen;
		Button btnInfoSchoenen;
		Button btnInfoAccessoires;
		Button btnTerug;
		Button btnFavoriet;
		Button btnNietLeuk;


		private string ingelogdAls;
		private string geslachtKleurLichaam;
		private List<string> advies;
		private bool adviesGevonden;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.KrijgAdviesScherm);

			txtAdviesOmschrijving = FindViewById<TextView> (Resource.Id.txt_omschrijvingAdvies);
			txtStylistNaam = FindViewById<TextView> (Resource.Id.txt_stylistOmschrijving);
			txtBovenlichaam = FindViewById<TextView> (Resource.Id.txt_bovenlichaamOmschrijving);
			txtBenen = FindViewById<TextView> (Resource.Id.txt_benenOmschrijving);
			txtSchoenen = FindViewById<TextView> (Resource.Id.txt_schoenenOmschrijving);
			txtAccessoires = FindViewById<TextView> (Resource.Id.txt_accessoiresOmschrijving);

			ingelogdAls = bi.GetIngelogd ();
			geslachtKleurLichaam = bb.GetGeslachtKleurtypeLichaamstype (ingelogdAls);
			adviesGevonden = ba.PassendAdvies (geslachtKleurLichaam);
			if (adviesGevonden) 
			{
				advies = ba.KrijgAdvies (geslachtKleurLichaam);
				txtAdviesOmschrijving.Text = advies [0];
				txtStylistNaam.Text = advies [1];
				txtBovenlichaam.Text = advies [2];
				txtBenen.Text = advies [3];
				txtSchoenen.Text = advies [4];
				txtAccessoires.Text = advies [5];
			}
			else
				txtAdviesOmschrijving.Text = "Helaas, we hebben geen passend advies kunnen vinden";

			btnInfoBovenlichaam = FindViewById<Button> (Resource.Id.btn_infoBoven);
			btnInfoBenen = FindViewById<Button> (Resource.Id.btn_infoBenen);
			btnInfoSchoenen = FindViewById<Button> (Resource.Id.btn_infoSchoenen);
			btnInfoAccessoires = FindViewById<Button> (Resource.Id.btn_infoAccessoires);

			btnInfoBovenlichaam.Click+= delegate {
				SetInfoscherm(txtBovenlichaam.Text);
			};
			btnInfoBenen.Click+= delegate {
				SetInfoscherm(txtBenen.Text);
			};
			btnInfoSchoenen.Click+= delegate {
				SetInfoscherm(txtSchoenen.Text);
			};
			btnInfoAccessoires.Click+= delegate {
				SetInfoscherm(txtAccessoires.Text);
			};

			btnFavoriet = FindViewById<Button> (Resource.Id.btn_addFavoriet);
			btnFavoriet.Click += delegate {
				bf.InsertFavoriet(txtAdviesOmschrijving.Text, ingelogdAls);
				Toast.MakeText(this, "Toegevoegd aan Favorieten", ToastLength.Short).Show();
			};

			btnNietLeuk = FindViewById<Button> (Resource.Id.btn_nietLeuk);
			btnNietLeuk.Click+= delegate {
				//Handle niet leuk event
			};

			btnQR = FindViewById<Button> (Resource.Id.btn_genereerQR);
			kortingscode = "2 halen 1 betalen. Geldt voor alle onderbroeken en sokken.";
			btnQR.Click += delegate {
				SetContentView (Resource.Layout.QRCode);
				imgQR = FindViewById<ImageView>(Resource.Id.img_kortingCodeQR);
				imgQR.SetImageBitmap(GetQRCode(kortingscode));			
			};

			btnTerug = FindViewById<Button> (Resource.Id.btn_krijgAdviesTerug);
			btnTerug.Click += delegate {
				StartActivity(typeof(KlantActivity));
			};
		
		}

		private Bitmap GetQRCode(string tekst)
		{
			var writer = new BarcodeWriter {
				Format = BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions {
					Height = 600,
					Width = 600
				}
			};
			return writer.Write (tekst);
		}
			

		private void SetInfoscherm(string productomschrijving)
		{
			SetContentView(Resource.Layout.ProductInformatieScherm);
			TextView txtOmschrijving = FindViewById<TextView>(Resource.Id.txt_infoOmschrijving);
			TextView txtPrijs = FindViewById<TextView>(Resource.Id.txt_infoPrijs);
			TextView txtType = FindViewById<TextView>(Resource.Id.txt_infoType);
			Button terug = FindViewById<Button> (Resource.Id.btn_infoTerug);
			txtOmschrijving.Text = productomschrijving;
			txtPrijs.Text = "€"+bk.GetProductPrijs(txtOmschrijving.Text)+",00";
			txtType.Text = bk.GetProductType(txtOmschrijving.Text);
			terug.Click += delegate {
				StartActivity(typeof(KrijgAdviesActivity));
			};
		}

	}
}

