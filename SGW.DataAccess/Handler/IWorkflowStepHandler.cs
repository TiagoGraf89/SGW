using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowStepHandler : IBaseHandler<Common.DataContract.WorkflowStepDataContract, SGW_WorkflowStep>
	{
		IEnumerable<WorkflowStepDataContract> GetAll(Guid workflowId, Guid fromStateId);
	}
}
