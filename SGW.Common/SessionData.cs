using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.Common
{
	public static class SessionData
	{
		public static void StartSession(Guid resourceId, string name, string email, bool isAdmin)
		{
			SessionData.ResourceId = resourceId;
 			SessionData.Name = name;
			SessionData.Email = email;
			SessionData.LoginDate = DateTime.Now;
			SessionData.IsAdmin = isAdmin;
		}

		public static Guid ResourceId { get; internal set; }
		public static DateTime LoginDate { get; internal set; }
		public static string Name { get; internal set; }
		public static string Email { get; internal set; }
		public static bool IsAdmin { get; internal set; }
	}
}
