using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.Common.Extender
{
	public static class StringHelper
	{
		public static string FirstUpperCase(this string value)
		{
			return string.Format("{0}{1}", value.Substring(0, 1).ToUpper(), value.Remove(0, 1));
		}

		public static int GetCharCount(string value, char c)
		{
			int result = 0;
			foreach (var a in value.ToCharArray())
				if (a == c)
					result++;
			return result;
		}
	}
}