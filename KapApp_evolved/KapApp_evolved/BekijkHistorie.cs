
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
		BeheerAdvies ba = new BeheerAdvies ();
		BeheerKledingstukken bk = new BeheerKledingstukken ();
		
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
			lstFavorieten = FindViewById<ListView> (Resource.Id.list_favorieten);

			ArrayAdapter adapter = new ArrayAdapter<String> (this,Android.Resource.Layout.SimpleListItem1, favorieten);
			lstFavorieten.Adapter = adapter;
			lstFavorieten.ItemClick += MListView_ItemClick;

			btnTerug = FindViewById<Button> (Resource.Id.btn_favorietenTerug);
			btnTerug.Click += delegate {
				StartActivity(typeof(KlantActivity));
			};
		}
		void MListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			SetContentView(Resource.Layout.KrijgAdviesScherm);
			Button terug = FindViewById<Button> (Resource.Id.btn_krijgAdviesTerug);
			TextView omschrijving = FindViewById<TextView> (Resource.Id.txt_omschrijvingAdvies);
			TextView stylist = FindViewById<TextView> (Resource.Id.txt_stylistOmschrijving);
			TextView bovenLichaam = FindViewById<TextView> (Resource.Id.txt_bovenlichaamOmschrijving);
			TextView benen = FindViewById<TextView> (Resource.Id.txt_benenOmschrijving);
			TextView schoenen = FindViewById<TextView> (Resource.Id.txt_schoenenOmschrijving);
			TextView accessoires = FindViewById<TextView> (Resource.Id.txt_accessoiresOmschrijving);
			List<string> advies = ba.VindAdviesOpOmschrijving (favorieten [e.Position]);
			omschrijving.Text = advies [0];
			stylist.Text = advies [1];
			bovenLichaam.Text = advies [2];
			benen.Text = advies [3];
			schoenen.Text = advies [4];
			accessoires.Text = advies [5];
			terug.Click += delegate {
				StartActivity (typeof(BekijkHistorie));
			};

			Button btnInfoBovenlichaam = FindViewById<Button> (Resource.Id.btn_infoBoven);
			Button btnInfoBenen = FindViewById<Button> (Resource.Id.btn_infoBenen);
			Button btnInfoSchoenen = FindViewById<Button> (Resource.Id.btn_infoSchoenen);
			Button btnInfoAccessoires = FindViewById<Button> (Resource.Id.btn_infoAccessoires);

			btnInfoBovenlichaam.Click+= delegate {
				SetInfoscherm(bovenLichaam.Text);
			};
			btnInfoBenen.Click+= delegate {
				SetInfoscherm(benen.Text);
			};
			btnInfoSchoenen.Click+= delegate {
				SetInfoscherm(schoenen.Text);
			};
			btnInfoAccessoires.Click+= delegate {
				SetInfoscherm(accessoires.Text);
			};


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
				StartActivity(typeof(BekijkHistorie));
			};
		}
	}
}


