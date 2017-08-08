using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IConditionDetailHandler : IBaseHandler<Common.DataContract.ConditionDetailDataContract, SGW_ConditionDetail>
	{
		IEnumerable<ConditionDetailDataContract> GetAll(Guid conditionId);
	}
}
