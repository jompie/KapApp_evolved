using System;
using SQLite;

namespace BU
{
	public class Kledingstuk
	{
		[PrimaryKey, AutoIncrement]
		public int IDkledingstuk { get; set; }
		public int Prijs { get; set; }
		public int Korting { get; set; }
		public string Kledingtype { get; set; } 	//Beenmode, bovenkleding, schoeisel of accessoire
		public string Sexe { get; set; }			//man, vrouw of unisex

		public Kledingstuk ()
		{
		}
	}
}

