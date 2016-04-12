using System;
using SQLite;

namespace BU
{
	public class Gebruiker
	{
		[PrimaryKey, AutoIncrement]
		public int Idgebruiker{ get; set;}
		public string Naam{ get; set;}
		public string Gebruikersnaam{ get; set;}
		public string Wachtwoord{ get; set;}
		public string Gebruikerstype{ get; set;}

		public Gebruiker ()
		{
		}
	}
}

