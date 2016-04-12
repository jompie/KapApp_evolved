using System;
using SQLite;

namespace BU
{
	public class Ingelogd
	{
		[PrimaryKey, AutoIncrement]
		public int IDingelogd { get; set; }
		public string GebruikersnaamIngelogd{ get; set;}

		public Ingelogd ()
		{
			
		}
	}
}

