using System;
using SQLite;

namespace BU
{
	public class Klant : Gebruiker
	{
		[PrimaryKey, AutoIncrement]
		public int IDklant { get; set; } 
		public int Basisinstelling { get; set;} 

		public Klant ()
		{
		}
	}
}

