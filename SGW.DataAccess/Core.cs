using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.DataAccess
{
	public static class Core
	{
		private static Factory instance;
		public static Factory GetFactory()
		{
			if (instance == null)
				instance = new Factory();

			return instance;
		}

		private static SGWDataContext main;
		public static SGWDataContext MainDataContextInstance()
		{
			if (main == null)
				main = new SGWDataContext(ConfigurationManager.ConnectionStrings["DataAccess.Properties.Settings.SGWConnectionString"].ConnectionString);

			if (main.Connection.State != System.Data.ConnectionState.Open)
				main.Connection.Open();

			return main;
		}
	}
}
