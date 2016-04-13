using System;
using System.IO;
using SQLite;
using BU;
using System.Collections.Generic;


namespace CC
{
	public class BeheerKledingstukken
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
			string filename = System.IO.Path.Combine (path, "Kledingstukken.db");
			return filename;
		}

		private void CreateTable()
		{
			using (var conn = new SQLiteConnection(GetDatabasePath()))
			{
				conn.CreateTable<Kledingstuk>();
				InsertStandaardKledingstukken ();
				databaseCreated = true;
			}
		}


		public void InsertKledingstuk(
			string omschrijving,
			int prijs,
			int korting, 
			string kledingType)
			//string sexe
		{
			databaseCreated = CheckIfCreated ();
			if (!databaseCreated) {
				CreateTable ();
			}
			Kledingstuk kledingstuk = new Kledingstuk {
				Omschrijving = omschrijving,
				Prijs = prijs,
				Korting = korting,
				Kledingtype = kledingType
			};
				//Sexe = sexe}
			using (var db = new SQLiteConnection (GetDatabasePath ())) 
				{
				db.Insert (kledingstuk);
				}
			}

		public void InsertStandaardKledingstukken()
		{
			databaseCreated = CheckIfCreated ();
			if (!databaseCreated) {
				InsertKledingstuk ("Blauwe spijkerbroek", 80, 10, "Benen");
				InsertKledingstuk ("Witte rok", 50, 15, "Benen");
				InsertKledingstuk ("Zwart overhemd", 35, 15, "Bovenlichaam");
				InsertKledingstuk ("Gebreide trui", 60, 10, "Bovenlichaam");
				InsertKledingstuk ("Authentieke Cowboy laarzen", 220, 20, "Schoenen");
				InsertKledingstuk ("Neon-groene Sneakers", 70, 5, "Schoenen");
				InsertKledingstuk ("Pilotenbril", 15, 0, "Accessoires");
				InsertKledingstuk ("Luchador Masker", 25, 5, "Accessoires");
			}

		}



		public Array GetKledingstukkenArray(string kledingType)
		{
			int count = 0;
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Kledingstuk> kledingstuk = db.Query<Kledingstuk> ("SELECT * FROM KLEDINGSTUK WHERE KLEDINGTYPE = '" + kledingType + "' ORDER BY IDKLEDINGSTUK DESC");
					List<string> kledingstukken = new List<string> (){ };
					while (kledingstuk.Count > count) {
						Kledingstuk p = kledingstuk [count];
						kledingstukken.Add (p.Omschrijving);
						count++;
					}
					return kledingstukken.ToArray ();
				}
			}
			return null;
		}

		public string GetProductPrijs(string omschrijving)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Kledingstuk> kledingstuk = db.Query<Kledingstuk> ("SELECT * FROM KLEDINGSTUK WHERE OMSCHRIJVING ='" + omschrijving + "' ORDER BY IDKLEDINGSTUK DESC");
					if (kledingstuk.Count > 0) {
						Kledingstuk p = kledingstuk [0];
						string prijs = p.Prijs.ToString();
						return prijs;
					}
				}
			}
			return null;
		}

		public string GetProductType(string omschrijving)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Kledingstuk> kledingstuk = db.Query<Kledingstuk> ("SELECT * FROM KLEDINGSTUK WHERE OMSCHRIJVING ='" + omschrijving + "' ORDER BY IDKLEDINGSTUK DESC");
					if (kledingstuk.Count > 0) {
						Kledingstuk p = kledingstuk [0];
						string type = p.Kledingtype;
						return type;
					}
				}
			}
			return null;
		}
		public bool KledingstukBestaat(string omschrijving)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Kledingstuk> kleding = db.Query<Kledingstuk> ("SELECT * FROM KLEDINGSTUK WHERE GEBRUIKERSNAAM = '" + omschrijving + "' ORDER BY IDkledingstuk DESC LIMIT 1");
					if (kleding.Count > 0) {
						return true;
					}
				}
			}
			return false;
		}
	}
}


