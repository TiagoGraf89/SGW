using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SGW.Common.DataContract;
using WebMatrix.WebData;

namespace SGW.Portal
{
	public class CustomMembershipProvider : SimpleMembershipProvider
	{
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			return base.GetUser(username, userIsOnline);
		}
		public override int GetUserIdFromOAuth(string provider, string providerUserId)
		{
			return base.GetUserIdFromOAuth(provider, providerUserId);
		}
		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			base.Initialize(name, config);
		}
		public override bool ValidateUser(string username, string password)
		{
			var bo = BusinessLogic.Core.GetFactory().GetInstance<BusinessLogic.BusinessObject.IResourceBO>();
			if (bo.Login(username, password))
			{
				ResourceDataContract dt = bo.GetByEmail(username);
				Common.SessionData.StartSession(dt.Id,dt.Name,dt.Email, dt.IsAdmin);
				return true;
			}


			return false;
		}
	}
}