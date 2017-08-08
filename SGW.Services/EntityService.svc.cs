using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SGW.BusinessLogic.BusinessObject;

namespace SGWServices
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
	public class EntityService : IEntity
	{
		public bool NewManualEntityInstance(EntityDataContract entity)
		{
			var wfBO = SGW.BusinessLogic.Core.GetFactory().GetInstance<IWorkflowBO>();

			wfBO.AddEntityInstance(entity.EntityId, entity.Data.Select(o=> new KeyValuePair<string, object>(o.Field, o.Value)));
			return true;
		}

		public void ProcessWorkflows()
		{
			var wfBO = SGW.BusinessLogic.Core.GetFactory().GetInstance<IWorkflowBO>();
			wfBO.ProcessWorkflow();


		}

		public void ProcessEmails()
		{


		}
	}
}
