using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowStepDecisionConditionHandler : IBaseHandler<Common.DataContract.DecisionConditionDataContract, SGW_WorkflowStepDecisionCondition>
	{
		OperationResult DeleteAll(Guid stepId);
		IEnumerable<Common.DataContract.DecisionConditionDataContract> GetAll(Guid stepId);
	}
}
