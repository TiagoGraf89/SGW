using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IRoleHandler : IBaseHandler<Common.DataContract.RoleDataContract, SGW_Role>
	{
		IEnumerable<RoleDataContract> GetByResourceId(Guid resourceId);
	}
}
