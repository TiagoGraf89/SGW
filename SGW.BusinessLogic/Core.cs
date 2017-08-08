using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.BusinessLogic
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
	}
}
