using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGW.BusinessLogic.BusinessObject;
using SGW.Common.DataContract;
using Microsoft.Web.WebPages.OAuth;
using SGW.Portal.Models;

namespace SGW.Portal
{
	public static class AuthConfig
	{
		public static void StartLoginSession(string email)
		{
			var bo = BusinessLogic.Core.GetFactory().GetInstance<IResourceBO>();
			ResourceDataContract resource = bo.GetByEmail(email);
			if (resource == null)
				throw new Exception(string.Format("Resource {0} Not Found.", email));
			else
				Common.SessionData.StartSession(resource.Id, resource.Name, resource.Email, resource.IsAdmin);
		}

		public static void RegisterAuth()
		{
			// To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
			// you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166
			
			//OAuthWebSecurity.RegisterMicrosoftClient(
			//    clientId: "",
			//    clientSecret: "");

			//OAuthWebSecurity.RegisterTwitterClient(
			//    consumerKey: "",
			//    consumerSecret: "");

			//OAuthWebSecurity.RegisterFacebookClient(
			//    appId: "",
			//    appSecret: "");

			//OAuthWebSecurity.RegisterGoogleClient();
		}
	}
}
