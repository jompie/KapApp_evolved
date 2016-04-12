using System;
using SQLite;

namespace BU
{
	public class Kledingstuk
	{
		[PrimaryKey, AutoIncrement]
		public int IDkledingstuk { get; set; }
		public string Omschrijving { get; set; }
		public int Prijs { get; set; }
		public int Korting { get; set; }
		public string Kledingtype { get; set; } 	//Bovenlichaam, Benen, Schoenen, Accessoires
		//public string Sexe { get; set; }			//man, vrouw of unisex

		public Kledingstuk ()
		{
		}
	}
}

