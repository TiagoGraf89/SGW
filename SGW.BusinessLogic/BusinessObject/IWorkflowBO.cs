using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.Common.DataContract;

namespace SGW.BusinessLogic.BusinessObject
{
	public interface IWorkflowBO
	{
		Common.OperationResult Add(WorkflowDataContract dataContract);
		Common.OperationResult Update(WorkflowDataContract dataContract);
		Common.OperationResult Delete(WorkflowDataContract dataContract);
		WorkflowDataContract GetById(Guid id);
		IEnumerable<Common.DataContract.WorkflowDataContract> GetAll();

		Common.OperationResult AddState(WorkflowStateDataContract dataContract);
		WorkflowStateDataContract GetStateById(Guid stateId);

		IEnumerable<WorkflowStateTransitionDataContract> GetAllWorkflowStateTransition(Guid workflowId);

		IEnumerable<StepTypeDataContract> GetAllStepType();
		IEnumerable<ParticipantDataContract> GetAllParticipant();
		StepTypeDataContract GetStepTypeByCommand(string command);
		StepTypeDataContract GetStepTypeById(Guid id);

		IEnumerable<WorkflowStepDataContract> GetSteps(Guid workflowId, Guid fromStateId);
		IEnumerable<WorkflowStepTransitionDataContract> GetStepsTransitions(Guid workflowId, Guid fromStateId);
		OperationResult SaveTransitionDetail(Guid workflowId, Guid fromStateId, IEnumerable<WorkflowStepDataContract> steps, IEnumerable<WorkflowStepTransitionDataContract> transitions);
		IEnumerable<WorkflowAssignmentDataContract> GetPendingRequestsByResourceId(Guid resourceId);

		WorkflowStepDataContract GetStepById(Guid stepId);
		WorkflowStepTransitionDataContract GetStepTransitionById(Guid transitionId);

		OperationResult ProcessManualStep(Guid workflowStepInstanceId, Guid stepId, string resultCode, byte[] file, string fileName);

		void ProcessWorkflow();

		void AddEntityInstance(Guid entityId, IEnumerable<KeyValuePair<string, object>> data);
	}
}
