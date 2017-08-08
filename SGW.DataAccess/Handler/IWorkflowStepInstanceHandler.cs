using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IWorkflowStepInstanceHandler : IBaseHandler<Common.DataContract.WorkflowStepInstanceDataContract, SGW_WorkflowStepInstance>
	{
		IEnumerable<WorkflowStepInstanceDataContract> GetAll(bool active);
		Common.OperationResult AttachFile(System.Data.Linq.Binary file, string fileName, Guid entityInstanceId, Guid stepId);
	}
}
