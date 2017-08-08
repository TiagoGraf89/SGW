using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowStateTransitionHandler : IBaseHandler<WorkflowStateTransitionDataContract, SGW_WorkflowStateTransition>
	{
		IEnumerable<WorkflowStateTransitionDataContract> GetAll(Guid workflowId);
	}
}
