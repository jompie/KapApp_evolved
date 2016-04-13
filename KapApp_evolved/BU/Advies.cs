using System;
using SQLite;

namespace BU
{
	public class Advies
	{
		[PrimaryKey, AutoIncrement]
		public int IDadvies { get; set; }
		public string AdviesOmschrijving { get; set; }
		public string Beenmode { get; set; }		//Jeans, pantalons, rokken, etc.
		public string Bovenkleding { get; set; }   //T-shirts, truien, jassen, etc.
		public string Schoeilsel { get; set; }		//Sneakers, laarzen, slippers, etc.
		public string Accessoire { get; set; } 	//Horloge, ketting, oorbellen, etc.
		public string HoortBij { get; set; } //bijv. GeslachtKleurtypeLichaamstype
		public string Stylist { get; set; }
	}
}

