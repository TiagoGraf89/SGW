using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.BusinessLogic.BusinessObject
{
	public interface IRoleBO
	{
		Common.OperationResult Add(RoleDataContract dataContract);
		Common.OperationResult Update(RoleDataContract dataContract);
		Common.OperationResult Delete(RoleDataContract dataContract);
		IEnumerable<RoleDataContract> GetByResourceId(Guid resourceId);
		IEnumerable<RoleDataContract> GetAll();
		RoleDataContract GetById(Guid id);
	}
}
