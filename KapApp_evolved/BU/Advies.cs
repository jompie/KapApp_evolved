using System;
using SQLite;

namespace BU
{
	public class Advies
	{
		[PrimaryKey, AutoIncrement]
		public int IDadvies { get; set; }
		public int Beenmode { get; set; }		//Jeans, pantalons, rokken, etc.
		public int Bovenkleding { get; set; }   //T-shirts, truien, jassen, etc.
		public int Schoeilsel { get; set; }		//Sneakers, laarzen, slippers, etc.
		public int Accessoire { get; set; } 	//Horloge, ketting, oorbellen, etc.
		public string HoortBijBasisinstelling { get; set; }
	}
}

