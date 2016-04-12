using System;
using SQLite;
using BU;
using System.IO;

namespace CC
{
	public class BeheerAdvies
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
			string filename = System.IO.Path.Combine (path, "Adviezen.db");
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

		public void InsertAdvies(
			int beenmode,
			int bovenkleding,
			int schoeisel,
			int accessoire,
			string hoortBijBasisinstelling)
		{
			databaseCreated = CheckIfCreated ();
			if (!databaseCreated) {
				CreateTable ();
			}
			Advies advies = new Advies {
				Beenmode = beenmode,
				Bovenkleding = bovenkleding,
				Schoeilsel = schoeisel,
				Accessoire = accessoire,
				HoortBijBasisinstelling = hoortBijBasisinstelling};
			using (var db = new SQLiteConnection (GetDatabasePath ())) {
				db.Insert (advies);
			}
		}

		public void krijgAdvies(string hoortbijBasisinstellingen)
		{
			// zoek passende adviezen
			// kies eerste matchende advies
			// indien dit advies al in de historie van de gebruiker staat, moet het volgende matchende advies worden gekozen
		}

		public BeheerAdvies ()
		{
		}
	}
}

