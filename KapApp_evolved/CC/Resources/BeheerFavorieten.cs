using System;
using SQLite;
using System.IO;
using BU;
using System.Collections.Generic;
using System.Collections;

namespace CC
{
	public class BeheerFavorieten
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
			string filename = System.IO.Path.Combine (path, "Favorieten.db");
			return filename;
		}

		private void CreateTable()
		{
			using (var conn = new SQLiteConnection(GetDatabasePath()))
			{
				conn.CreateTable<Favorietenlijst>();
				databaseCreated = true;
			}
		}
		public void InsertFavoriet(string omschrijving, string gebruiker)
		{
			databaseCreated = CheckIfCreated ();
			if (!databaseCreated) {
				CreateTable ();
			}
			Favorietenlijst favorietenlijst = new Favorietenlijst { HoortBijGebruiker = gebruiker, Adviesnaam = omschrijving };
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					db.Insert (favorietenlijst);
				}
			}


		public List<string> GetFavorieten(string gebruikersnaam)
		{
			int count = 0;
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Favorietenlijst> favorietenlijst = db.Query<Favorietenlijst> ("SELECT * FROM FAVORIETENLIJST WHERE HOORTBIJGEBRUIKER = '" + gebruikersnaam + "' ORDER BY IDFAVORIET DESC");
					List<string> returnlist = new List<string>(){};
					while (favorietenlijst.Count > count) {
						Favorietenlijst p = favorietenlijst [count];
						returnlist.Add (p.Adviesnaam);
						count++;
					}
					return returnlist;
				}
			}
			return new List<string>(){};
		}

	}
}

