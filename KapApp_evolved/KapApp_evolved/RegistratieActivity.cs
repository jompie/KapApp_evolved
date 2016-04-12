
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
	[Activity (Label = "RegistratieActivity")]			
	public class RegistratieActivity : Activity
	{
		BeheerGebruikers bg = new BeheerGebruikers ();

		EditText txtNaam;
		EditText txtGebruikersnaam;
		EditText txtWachtwoord;
		EditText txtHerhaalWachtwoord;
		EditText txtGebruikersType;
		Button btnRegistreer;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.RegistratieScherm);

			txtNaam = FindViewById<EditText> (Resource.Id.txt_regNaam);
			txtGebruikersnaam = FindViewById<EditText> (Resource.Id.txt_regGebruikersnaam);
			txtWachtwoord = FindViewById<EditText> (Resource.Id.txt_regWachtwoord);
			txtHerhaalWachtwoord = FindViewById<EditText> (Resource.Id.txt_regHerhaalWachtwoord);
			txtGebruikersType = FindViewById<EditText> (Resource.Id.txt_regGebruikersType);

			btnRegistreer = FindViewById<Button> (Resource.Id.btn_regRegistreer);
			btnRegistreer.Click += delegate {
				string type = txtGebruikersType.Text;
				bool volledigIngevuld = checkVolledigIngevuld();
				if (volledigIngevuld){
					if (type == "Klant" | type == "Stylist" | type == "Winkeleigenaar"){
						bool gebruikerBestaatAl = bg.GebruikerBestaat(txtGebruikersnaam.Text);
						if(!gebruikerBestaatAl){
							if(txtWachtwoord.Text == txtHerhaalWachtwoord.Text){
								bg.InsertGebruiker(txtNaam.Text, 
									txtGebruikersnaam.Text, 
									txtWachtwoord.Text, 
									txtGebruikersType.Text);
								Toast.MakeText (this.BaseContext, "Account aangemaakt", ToastLength.Short).Show ();
								StartActivity(typeof(MainActivity));
							}
							else
								Toast.MakeText (this.BaseContext, "Wachtwoord komt niet overeen", ToastLength.Short).Show ();
						}
						else
							Toast.MakeText (this.BaseContext, "Gebruikersnaam is reeds in gebruik", ToastLength.Short).Show ();
					}
					else
						Toast.MakeText (this.BaseContext, "Ongeldig gebruikerstype", ToastLength.Short).Show ();
				}
				else 
					if(!volledigIngevuld)
						Toast.MakeText (this.BaseContext, "Formulier is niet volledig ingevuld", ToastLength.Short).Show ();
			};

		}
		private bool checkVolledigIngevuld()
		{
			if (txtNaam.Text == null | 
				txtGebruikersnaam.Text == null | 
				txtWachtwoord.Text == null | 
				txtGebruikersType.Text == null)
				return false;
			else
				return true;
		}
						

	}
}

