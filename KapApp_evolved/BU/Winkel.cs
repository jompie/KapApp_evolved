using System;
using SQLite;

namespace BU
{
	public class Winkel
	{
		[PrimaryKey, AutoIncrement]
		public int IDwinkel { get; set; }
		public string Plaats { get; set; }
		public string Straat { get; set; } 
		public string Huisnummer { get; set; }


		public Winkel ()
		{
		}
	}
}

