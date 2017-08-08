using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;
using SGW.DataAccess.Handler;

namespace SGW.BusinessLogic.BusinessObject
{
    public class RoleBO : IRoleBO
    {

		public Common.OperationResult Add(Common.DataContract.RoleDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IRoleHandler>();
			dataContract.CreatedBy = Common.SessionData.ResourceId;
			dataContract.CreatedOn = DateTime.Now;
	
			var val = dataContract.Validate();
			if (!val.IsValid)
				return new Common.OperationResult(val);

			return handler.Add(dataContract);
		}

		public Common.OperationResult Update(RoleDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IRoleHandler>();
			dataContract.UpdatedBy = Common.SessionData.ResourceId;
			dataContract.UpdatedOn = DateTime.Now;

			var val = dataContract.Validate();
			if (!val.IsValid)
				return new Common.OperationResult(val);

			return handler.Update(dataContract);
		}

		public Common.OperationResult Delete(Common.DataContract.RoleDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IRoleHandler>();
			return handler.Delete(dataContract);
		}

		public Common.DataContract.RoleDataContract GetById(Guid id)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IRoleHandler>();
			return handler.GetById(id);
		}

		public IEnumerable<RoleDataContract> GetAll()
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IRoleHandler>();
			return handler.GetAll();
		}
		public IEnumerable<RoleDataContract> GetByResourceId(Guid resourceId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IRoleHandler>();
			return handler.GetByResourceId(resourceId);
		}
	}
}
