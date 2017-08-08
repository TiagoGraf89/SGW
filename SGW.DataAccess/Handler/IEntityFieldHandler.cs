using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IEntityFieldHandler : IBaseHandler<Common.DataContract.EntityFieldDataContract, SGW_EntityField>
	{
		IEnumerable<EntityFieldDataContract> GetAll(Guid entityId);
		void DeleteAll(Guid entityId);
	}
}
