using System;
using SQLite;

namespace BU
{
	public class Basisinstelling
	{
		[PrimaryKey, AutoIncrement]
		public int IDbasisinstelling { get; set; }
		public string Geslacht { get; set; }
		public string Oogkleur { get; set; } 
		public string Haarkleur { get; set; } 
		public string Ondertoon { get; set; } 
		public string Kleurtype { get; set; } 
		public string Lichaamstype { get; set;} 
		public string GebruikersnaamKlant { get; set;} 


		public Basisinstelling ()
		{
		}
	}
}

