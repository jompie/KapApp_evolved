using Android.App;
using Android.Widget;
using Android.OS;

using CC;

namespace KapApp_evolved
{
	[Activity (Label = "KapApp_evolved", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		BeheerGebruikers bg = new BeheerGebruikers ();
		BeheerIngelogd bi = new BeheerIngelogd ();
	
		TextView txtGebruikersnaam;
		TextView txtWachtwoord;
		Button btnLogin;
		Button btnRegistreer;
		Button btnInfo;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.LoginScherm);

			txtGebruikersnaam = FindViewById<TextView> (Resource.Id.txt_loginGebruikersnaam);
			txtWachtwoord = FindViewById<TextView> (Resource.Id.txt_loginWachtwoord);


			//Login knop
			//Er wordt gekeken of er een account bestaat dat overeen komt met de opgegeven gegevens
			//Als er een account is gevonden wordt een activity gestart die hoort bij het gebruikerstype
			btnLogin = FindViewById<Button> (Resource.Id.btn_loginLogin);
			btnLogin.Click += delegate {
				bool bestaatGebruiker = bg.GebruikerBestaat(txtGebruikersnaam.Text);
				if (!bestaatGebruiker)
					Toast.MakeText(this,"Gebruiker bestaat niet", ToastLength.Short).Show();
				else
					if (bestaatGebruiker)
					{
						string ww = bg.GetGebruikerWachtwoord(txtGebruikersnaam.Text);
						if (txtWachtwoord.Text == ww)
						{
							//bi.SetIngelogdAls(txtGebruikersnaam.Text);
							bi.InsertIngelogd(txtGebruikersnaam.Text);
							string type = bg.GetGebruikersType(txtGebruikersnaam.Text);
							if (type == "Klant"){
								StartActivity(typeof(KlantActivity));
							}
							else
								if(type == "Stylist"){
									StartActivity(typeof(StylistActivity));
								}
								else
									if(type == "Winkeleigenaar"){
										StartActivity(typeof(WinkeleigenaarActivity));
									}
									else
										if(type == "Verkoper"){
											StartActivity(typeof(VerkoperActivity));
										}
						}
						if (!(txtWachtwoord.Text == ww))
							Toast.MakeText(this,"Ongeldige combinatie gebruikersnaam en wachtwoord", ToastLength.Short).Show();
					}
				
			};

			// Registratie knop
			// Als de knop wordt ingedrukt, start de registratieactivity
			btnRegistreer = FindViewById<Button> (Resource.Id.btn_loginRegistreer);
			btnRegistreer.Click += delegate {
				StartActivity(typeof(RegistratieActivity));
			};

			// Info knop
			btnInfo = FindViewById<Button> (Resource.Id.btn_loginAlgemeneInfo);
			btnInfo.Click += delegate {
				//StartActivity(typeof(
			};
				
		}
			
	}
}


