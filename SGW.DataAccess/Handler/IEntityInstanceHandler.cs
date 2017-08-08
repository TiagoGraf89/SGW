using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IEntityInstanceHandler : IBaseHandler<EntityInstanceDataContract, SGW_EntityInstance>
	{
		IEnumerable<KeyValuePair<Guid, string>> GetCurrentStepsDescription(Guid entityInstanceId);
	}
}
