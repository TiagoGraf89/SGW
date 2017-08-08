using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IStepTypeHandler : IBaseHandler<Common.DataContract.StepTypeDataContract, SGW_StepType>
	{
		IEnumerable<StepTypeDataContract> GetAll();
	}
}
