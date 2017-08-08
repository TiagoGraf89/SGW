using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowStateHandler : IBaseHandler<WorkflowStateDataContract, SGW_WorkflowState>
	{
		WorkflowStateDataContract GetByDescription(string description, Guid workflowId);
		IEnumerable<WorkflowStateDataContract> GetAll(Guid workflowId);
	}
}
