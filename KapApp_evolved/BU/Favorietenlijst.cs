using System;
using SQLite;
using System.Collections;

namespace BU
{
	public class Favorietenlijst
	{
		[PrimaryKey, AutoIncrement]
		public int IDFavoriet { get; set; }
		public string Adviesnaam { get; set; }
		public string HoortBijGebruiker { get; set; }
	}
}

