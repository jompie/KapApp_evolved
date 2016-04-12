using System;
using SQLite;
using BU;
using System.IO;
using System.Collections.Generic;

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
				conn.CreateTable<Advies>();
				databaseCreated = true;
			}
		}

		public void InsertAdvies(
			string adviesOmschrijving,
			string beenmode,
			string bovenkleding,
			string schoeisel,
			string accessoire,
			string hoortBijBasisinstelling,
			string stylist)
		{
			databaseCreated = CheckIfCreated ();
			if (!databaseCreated) {
				CreateTable ();
			}
			Advies advies = new Advies {
				AdviesOmschrijving = adviesOmschrijving,
				Beenmode = beenmode,
				Bovenkleding = bovenkleding,
				Schoeilsel = schoeisel,
				Accessoire = accessoire,
				HoortBijBasisinstelling = hoortBijBasisinstelling,
				Stylist = stylist};
			using (var db = new SQLiteConnection (GetDatabasePath ())) {
				db.Insert (advies);
			}
		}

		public List<String> KrijgAdvies(string basisinstelling)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Advies> adviezen = db.Query<Advies> ("SELECT * FROM ADVIES WHERE HoortBijBasisinstelling = '" + basisinstelling + "' ORDER BY IDADVIES DESC LIMIT 1");
					if (adviezen.Count > 0) {
						Advies p = adviezen [0];
						List<string> advies = new List<string> (){ p.AdviesOmschrijving, p.Stylist, p.Bovenkleding, p.Beenmode, p.Schoeilsel, p.Accessoire };
						return advies;
					}
				}
			}
			return null;
		}

		public bool PassendAdvies(string basisinstelling)
		{
			databaseCreated = CheckIfCreated ();
			if (databaseCreated) {
				using (var db = new SQLiteConnection (GetDatabasePath ())) {
					List<Advies> adviezen = db.Query<Advies> ("SELECT * FROM ADVIES WHERE HoortBijBasisinstelling = '" + basisinstelling + "' ORDER BY IDADVIES DESC LIMIT 1");
					if (adviezen.Count > 0)
						return true;
				}
			}
			return false;
		}



		public BeheerAdvies ()
		{
		}
	}
}

