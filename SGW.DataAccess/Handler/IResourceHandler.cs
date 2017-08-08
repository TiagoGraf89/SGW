using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IResourceHandler : IBaseHandler<ResourceDataContract, SGW_Resource>
	{
		ResourceDataContract GetByEmail(string email);
		Common.OperationResult UpdateResourceRoles(ResourceDataContract updatedDataContract);
		Common.OperationResult UpdateResourceWorkgroup(Common.DataContract.ResourceDataContract updatedDataContract);
	}
}
