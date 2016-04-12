using System;
using System.IO;
using System.Collections.Generic;
using SQLite;

using BU;

namespace CC
{
	public class BeheerBasisinstellingen
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
			string filename = System.IO.Path.Combine (path, "Basisinstellingen.db");
			return filename;
		}

		private void CreateTable()
		{
			using (var conn = new SQLiteConnection(GetDatabasePath()))
			{
				conn.CreateTable<Basisinstelling>();
				databaseCreated = true;
			}
		}

		public void InsertBasisinstelling(
			string geslacht, 
			string oogkleur, 
			string haarkleur, 
			string ondertoon,
			string kleurtype,
			string lichaamstype,
			string gebruikersnaamKlant)
		{
			databaseCreated = CheckIfCreated ();
			if (!databaseCreated) {
				CreateTable ();
			}
			Basisinstelling basisinstelling = new Basisinstelling {
				Geslacht = geslacht,
				Oogkleur = oogkleur,
				Haarkleur = haarkleur,
				Ondertoon = ondertoon,
				Kleurtype = kleurtype,
				Lichaamstype = lichaamstype,
				GebruikersnaamKlant = gebruikersnaamKlant};
			using (var db = new SQLiteConnection (GetDatabasePath ())) {
				db.Insert (basisinstelling);
			}
		}

		public string GetBasisInstellingen(string gebruikersnaamKlant)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Basisinstelling> basis = db.Query<Basisinstelling> ("SELECT * FROM BASISINSTELLING WHERE GEBRUIKERSNAAMKLANT = '" + gebruikersnaamKlant + "' ORDER BY IDBASISINSTELLING DESC LIMIT 1");
					if (basis.Count > 0) {
						Basisinstelling p = basis [0];
						string geslacht = p.Geslacht;
						return geslacht;
					}
				}
			}
			return "";
		}


			
			



	}
}

