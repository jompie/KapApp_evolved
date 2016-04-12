using System;


using BU;
using System.IO;
using SQLite;
using System.Collections.Generic;


namespace CC
{
	public class BeheerIngelogd
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
			string filename = System.IO.Path.Combine (path, "IngelogdAls.db");
			return filename;
		}

		private void CreateTable()
		{
			using (var conn = new SQLiteConnection(GetDatabasePath()))
			{
				conn.CreateTable<Ingelogd>();
				databaseCreated = true;
			}
		}

		public void InsertIngelogd(string gebruikersnaam)
		{
				
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) 
			{
				using (var db = new SQLiteConnection (GetDatabasePath ())) 
				{
					db.DeleteAll<Ingelogd> ();
				}
				CreateTable ();
			} 
			else 
				if (!databaseCreated) 
				{
					CreateTable ();
				}
			{
				Ingelogd ingelogd = new Ingelogd {GebruikersnaamIngelogd = gebruikersnaam};
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					db.DeleteAll<Ingelogd> ();
					db.Insert (ingelogd);
				}
			}
			
		}

		public string GetIngelogd()
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Ingelogd> ingelogd = db.Query<Ingelogd> ("SELECT * FROM INGELOGD ORDER BY IDIngelogd DESC");
					if (ingelogd.Count > 0) {
						Ingelogd p = ingelogd [0];
						string gebruikersnaam = p.GebruikersnaamIngelogd;
						return gebruikersnaam;
					}
				}
			}
			return "";
		}

		public void Uitloggen()
		{
			using (var db = new SQLiteConnection (GetDatabasePath ())) {
				db.DeleteAll<Ingelogd>();
			}
		}


		//SimpleStorage storage = SimpleStorage.EditGroup("ingelogd als");


			
	}
}

