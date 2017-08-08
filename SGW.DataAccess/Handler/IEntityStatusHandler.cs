using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IEntityStatusHandler : IBaseHandler<Common.DataContract.EntityStatusDataContract, SGW_EntityStatus>
	{
		IEnumerable<EntityStatusDataContract> GetAll(Guid entityId);
		void DeleteAll(Guid entityId);
	}
}
