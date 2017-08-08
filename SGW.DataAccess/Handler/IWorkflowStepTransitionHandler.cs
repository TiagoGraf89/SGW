using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowStepTransitionHandler : IBaseHandler<Common.DataContract.WorkflowStepTransitionDataContract, SGW_WorkflowStepTransition>
	{
		IEnumerable<WorkflowStepTransitionDataContract> GetAll(Guid workflowId, Guid fromStateId);
	}
}
