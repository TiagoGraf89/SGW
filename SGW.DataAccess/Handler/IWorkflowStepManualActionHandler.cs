using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowStepManualActionHandler : IBaseHandler<Common.DataContract.ManualActionDataContract, SGW_WorkflowStepManualAction>
	{
		OperationResult DeleteAll(Guid stepId);
		IEnumerable<Common.DataContract.ManualActionDataContract> GetAll(Guid stepId);
	}
}
