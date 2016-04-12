using System;
using System.IO;
using System.Collections.Generic;
using SQLite;

using BU;

namespace CC
{
	public class BeheerGebruikers
	{
		private bool databaseCreated;
		private bool CheckIfCreated()
		{
			if (File.Exists (GetDatabasePath ()))
				return true;
			else
				return false;
		}

		private string GetDatabasePath()
		{
			string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			string filename = System.IO.Path.Combine (path, "Gebruikers.db");
			return filename;
		}

		public bool databaseStatus()
		{
			return databaseCreated;
		}

		private void CreateTable()
		{
			using (var conn = new SQLiteConnection(GetDatabasePath()))
			{
				conn.CreateTable<Gebruiker>();
				databaseCreated = true;
			}
		}

		public void InsertGebruiker(
			string naam, 
			string gebruikersnaam, 
			string wachtwoord, 
			string gebruikerstype  )
		{
			databaseCreated = CheckIfCreated ();
			if (!databaseCreated) {
				if (File.Exists (GetDatabasePath ()))
					File.Delete (GetDatabasePath ());
				CreateTable ();
				databaseCreated = true;
			}
			var gebruiker = new Gebruiker {
				Naam = naam,
				Gebruikersnaam = gebruikersnaam,
				Wachtwoord = wachtwoord,
				Gebruikerstype = gebruikerstype};
			using (var db = new SQLiteConnection (GetDatabasePath ())) {
				db.Insert (gebruiker);
			}
		}

		public string GetGebruikerWachtwoord (string gebruikersnaam)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {

				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Gebruiker> gebruikers = db.Query<Gebruiker> ("SELECT * FROM GEBRUIKER WHERE GEBRUIKERSNAAM = '" + gebruikersnaam + "' ORDER BY IDGEBRUIKER DESC LIMIT 1");
					if (gebruikers.Count > 0) {
						Gebruiker p = gebruikers [0];
						string wachtwoord = p.Wachtwoord;
						return wachtwoord;
					}
				}
			}
			return "";
		}

		public string GetGebruikersType (string gebruikersnaam)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Gebruiker> gebruikers = db.Query<Gebruiker> ("SELECT * FROM GEBRUIKER WHERE GEBRUIKERSNAAM = '" + gebruikersnaam + "' ORDER BY IDGEBRUIKER DESC LIMIT 1");
					if (gebruikers.Count > 0) {
						Gebruiker p = gebruikers [0];
						string type = p.Gebruikerstype;
						return type;
					}
				}
			}
			return "";
		}
		public bool GebruikerBestaat(string gebruikersnaam)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Gebruiker> gebruikers = db.Query<Gebruiker> ("SELECT * FROM GEBRUIKER WHERE GEBRUIKERSNAAM = '" + gebruikersnaam + "' ORDER BY IDGEBRUIKER DESC LIMIT 1");
					if (gebruikers.Count > 0) {
						return true;
					}
				}
			}
			return false;
		}



	}
}

