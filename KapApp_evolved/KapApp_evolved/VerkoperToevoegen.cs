
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
	[Activity (Label = "VerkoperToevoegen")]			
	public class VerkoperToevoegen : Activity
	{
		BeheerGebruikers bg = new BeheerGebruikers ();

		EditText txtNaam;
		EditText txtGebruikersnaam;
		EditText txtWachtwoord;
		EditText txtHerhaalWachtwoord;
		Button btnBevestigen;
		Button terug;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.VerkoperToevoegen);

			txtNaam = FindViewById<EditText> (Resource.Id.txt_regNaamVerkoper);
			txtGebruikersnaam = FindViewById<EditText> (Resource.Id.txt_regGebruikersnaamVerkoper);
			txtWachtwoord = FindViewById<EditText> (Resource.Id.txt_regWachtwoordVerkoper);
			txtHerhaalWachtwoord = FindViewById <EditText> (Resource.Id.txt_regHerhaalWachtwoordVerkoper);

			btnBevestigen = FindViewById <Button> (Resource.Id.btn_regRegistreerVerkoper);
			btnBevestigen.Click += delegate {
				string type = "Verkoper";
				bool volledigIngevuld = checkVolledigIngevuld();
				if (volledigIngevuld){
					bool gebruikerBestaatAl = bg.GebruikerBestaat(txtGebruikersnaam.Text);
					if(!gebruikerBestaatAl){
						if(txtWachtwoord.Text == txtHerhaalWachtwoord.Text){
							bg.InsertGebruiker(txtNaam.Text, 
								txtGebruikersnaam.Text, 
								txtWachtwoord.Text, 
								type);
							Toast.MakeText (this.BaseContext, "Account aangemaakt", ToastLength.Short).Show ();
							StartActivity(typeof(WinkeleigenaarActivity));
						}
						else
							Toast.MakeText (this.BaseContext, "Wachtwoord komt niet overeen", ToastLength.Short).Show ();
					}
					else
						Toast.MakeText (this.BaseContext, "Gebruikersnaam is reeds in gebruik", ToastLength.Short).Show ();
				}
				else 
					if(!volledigIngevuld)
						Toast.MakeText (this.BaseContext, "Formulier is niet volledig ingevuld", ToastLength.Short).Show ();
			};

			terug = FindViewById<Button> (Resource.Id.btn_verkoperToevoegenTerug);
			terug.Click += delegate {
				StartActivity(typeof(WinkeleigenaarActivity));
			};
		}
		private bool checkVolledigIngevuld()
		{
			if (txtNaam.Text == "" | 
				txtGebruikersnaam.Text == "" | 
				txtWachtwoord.Text == "")
				return false;
			else
				return true;
		}
	}

}

