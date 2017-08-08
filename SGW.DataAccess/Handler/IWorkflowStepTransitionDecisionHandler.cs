using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowStepTransitionDecisionHandler : IBaseHandler<Common.DataContract.TransitionDecisionDataContract, SGW_WorkflowStepTransitionDecision>
	{
		OperationResult DeleteAll(Guid stepId);
		IEnumerable<Common.DataContract.TransitionDecisionDataContract> GetAll(Guid stepId);
	}
}
