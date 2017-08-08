using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowAssignmentHandler : IBaseHandler<WorkflowAssignmentDataContract, SGW_WorkflowInstanceAssignment>
	{
		IEnumerable<WorkflowAssignmentDataContract> GetAll(Guid ResourceId);
		WorkflowAssignmentDataContract GetByStepInstanceId(Guid stepInstanceId);
		IEnumerable<WorkflowAssignmentDataContract> GetByEntityInstanceId(Guid entityInstanceId);
	}
}
