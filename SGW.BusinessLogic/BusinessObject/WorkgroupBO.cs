using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;
using SGW.DataAccess.Handler;

namespace SGW.BusinessLogic.BusinessObject
{
    public class WorkgroupBO : IWorkgroupBO
    {

		public Common.OperationResult Add(Common.DataContract.WorkgroupDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkgroupHandler>();
			dataContract.CreatedBy = Common.SessionData.ResourceId;
			dataContract.CreatedOn = DateTime.Now;
			return handler.Add(dataContract);
		}

		public Common.OperationResult Update(WorkgroupDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkgroupHandler>();
			dataContract.UpdatedBy = Common.SessionData.ResourceId;
			dataContract.UpdatedOn = DateTime.Now;
			return handler.Update(dataContract);
		}

		public Common.OperationResult Delete(Common.DataContract.WorkgroupDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkgroupHandler>();
			return handler.Delete(dataContract);
		}

		public Common.DataContract.WorkgroupDataContract GetById(Guid id)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkgroupHandler>();
			return handler.GetById(id);
		}

		public IEnumerable<WorkgroupDataContract> GetAll()
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkgroupHandler>();
			return handler.GetAll();
		}

	}
}
