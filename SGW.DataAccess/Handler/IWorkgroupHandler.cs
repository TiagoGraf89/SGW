using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkgroupHandler : IBaseHandler<Common.DataContract.WorkgroupDataContract, SGW_Workgroup>
	{
		WorkgroupDataContract GetByResourceId(Guid id);
	}
}
