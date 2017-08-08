using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SGW.BusinessLogic.BusinessObject;

namespace SGW.Portal
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();

			var th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(backgroundProcess));
			th.IsBackground = true;
			th.Start();
		}

		void backgroundProcess(object obj)
		{Error processing message in WCF: System.ApplicationException
   at Dell.Changepoint.Db.CLR.Integration.StoredProcedures._ProcessSoapMessage(String serviceConfigEndpointUriName, String serviceConfigSoapAction, XElement queueMessage)
   at Dell.Changepoint.Db.CLR.Integration.StoredProcedures._ProcessMessage(String queueName, Guid handle, Int32 messageType, Int64 order, SqlXml message)
   at Dell.Changepoint.Db.CLR.Integration.StoredProcedures.ProcessMessage(String queueName, Guid handle, Int32 messageType, Int64 order, SqlXml messageXml, SqlString& errorMessage)
			var date = DateTime.Now;
			while (true)
			{
				if (DateTime.Now > date.AddSeconds(15))
				{
					try
					{
						ServiceReference1.EntityClient client = new ServiceReference1.EntityClient();
						client.ProcessWorkflows(new ServiceReference1.ProcessWorkflowsRequest());
						client.ProcessEmails(new ServiceReference1.ProcessEmailsRequest());
						date = DateTime.Now;
					}
					catch
					{

					}
				}
			}

		}
	}
}