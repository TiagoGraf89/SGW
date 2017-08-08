using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.Common.DataContract;
using SGW.DataAccess.Handler;

namespace SGW.BusinessLogic.BusinessObject
{
    public class WorkflowBO : IWorkflowBO
    {

		public Common.OperationResult Add(Common.DataContract.WorkflowDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowHandler>();
			dataContract.CreatedOn = DateTime.Now;
			dataContract.CreatedBy = Common.SessionData.ResourceId;
			var result = handler.Add(dataContract);

			if (result.Status != Common.OperationResultStatus.Succesfull)
				return result;

			foreach (var item in dataContract.WorkflowStateList)
			{
				item.WorkflowId = dataContract.Id;
				result = this.AddState(item);
				if (result.Status != Common.OperationResultStatus.Succesfull)
					return result;
			}

			foreach (var item in dataContract.WorkflowStateTransitionList)
			{
				item.WorkflowId = dataContract.Id;
				result = this.AddStateTransition(item);
				if (result.Status != Common.OperationResultStatus.Succesfull)
					return result;
			}

			return result;
		}

		public Common.OperationResult Update(WorkflowDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowHandler>();
			var handlerTransition = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateTransitionHandler>();
			var handlerState = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateHandler>();

			var result = handler.Update(dataContract);


			if (result.Status != Common.OperationResultStatus.Succesfull)
				return result;

			var stateList = handlerState.GetAll(dataContract.Id);
			foreach (var item in dataContract.WorkflowStateList)
			{
				if (!stateList.Any(o => o.Id.Equals(item.Id)))
				{
					item.WorkflowId = dataContract.Id;
					result = this.AddState(item);
					if (result.Status != Common.OperationResultStatus.Succesfull)
						return result;
				}
				else
				{
					result = handlerState.Update(item);
					if (result.Status != Common.OperationResultStatus.Succesfull)
						return result;

				}
			}

			var tranList = handlerTransition.GetAll(dataContract.Id);
			foreach (var item in dataContract.WorkflowStateTransitionList)
			{
				if (!tranList.Any(o => o.Id.Equals(item.Id)))
				{
					item.WorkflowId = dataContract.Id;
					result = this.AddStateTransition(item);
					if (result.Status != Common.OperationResultStatus.Succesfull)
						return result;
				}
			}


			tranList = handlerTransition.GetAll(dataContract.Id);
			foreach (var item in tranList)
			{
				if (!dataContract.WorkflowStateTransitionList.Any(o => o.Id.Equals(item.Id)))
				{
					result = handlerTransition.Delete(item);
					if (result.Status != Common.OperationResultStatus.Succesfull)
						return result;
				}
			}

			stateList = handlerState.GetAll(dataContract.Id);
			foreach (var item in stateList)
			{
				if (!dataContract.WorkflowStateList.Any(o => o.Id.Equals(item.Id)))
				{
					result = handlerState.Delete(item);
					if (result.Status != Common.OperationResultStatus.Succesfull)
						return result;
				}
			}


			return result;
		}

		public Common.OperationResult Delete(Common.DataContract.WorkflowDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowHandler>();
			return handler.Delete(dataContract);
		}

		public Common.DataContract.WorkflowDataContract GetById(Guid id)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowHandler>();
			var handlerTransition = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateTransitionHandler>();
			var handlerState = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateHandler>();
			var result = handler.GetById(id);

			if (result != null)
			{
				result.WorkflowStateTransitionList = handlerTransition.GetAll(id);
				result.WorkflowStateList = handlerState.GetAll(id);
			}

			return result;
		}

		public IEnumerable<Common.DataContract.WorkflowDataContract> GetAll()
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowHandler>();
			return handler.GetAll();
		}

		public Common.OperationResult SaveStep(Common.DataContract.WorkflowStepDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepHandler>();
			var handlerTranDecision = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepTransitionDecisionHandler>();
			var handlerDecisionCondition = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepDecisionConditionHandler>();
			var handlerManualAction = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepManualActionHandler>();

			dataContract.CreatedOn = DateTime.Now;
			dataContract.CreatedBy = Common.SessionData.ResourceId;


			var step = handler.GetById(dataContract.Id);

			OperationResult result;
			if (step != null)
				result = handler.Update(dataContract);
			else
				result = handler.Add(dataContract);


			if (result.Status != Common.OperationResultStatus.Succesfull)
				return result;


			handlerTranDecision.DeleteAll(dataContract.Id);
			handlerDecisionCondition.DeleteAll(dataContract.Id);
			handlerManualAction.DeleteAll(dataContract.Id);

			if (dataContract.TransitionDecisionList != null)
				foreach (var item in dataContract.TransitionDecisionList)
					handlerTranDecision.Add(item);

			if (dataContract.DecisionConditionList != null)
				foreach (var item in dataContract.DecisionConditionList)
					handlerDecisionCondition.Add(item);

			if (dataContract.ManualActionList != null)
				foreach (var item in dataContract.ManualActionList)
					handlerManualAction.Add(item);
			return result;
		}

		public Common.OperationResult SaveStepTransition(Common.DataContract.WorkflowStepTransitionDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepTransitionHandler>();
			dataContract.CreatedOn = DateTime.Now;
			dataContract.CreatedBy = Common.SessionData.ResourceId;

			var transition = handler.GetById(dataContract.Id);

			OperationResult result;
			if (transition != null)
				result = handler.Update(dataContract);
			else
				result = handler.Add(dataContract);

			return result;
		}

		public Common.OperationResult AddState(Common.DataContract.WorkflowStateDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateHandler>();
			dataContract.CreatedOn = DateTime.Now;
			dataContract.CreatedBy = Common.SessionData.ResourceId;
			return handler.Add(dataContract);
		}

		public Common.OperationResult AddStateTransition(Common.DataContract.WorkflowStateTransitionDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateTransitionHandler>();
			dataContract.CreatedOn = DateTime.Now;
			dataContract.CreatedBy = Common.SessionData.ResourceId;
			return handler.Add(dataContract);
		}

		public WorkflowStateDataContract GetStateById(Guid stateId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateHandler>();
			var entitySatusHandler = DataAccess.Core.GetFactory().GetInstance<IEntityStatusHandler>();
			var state = handler.GetById(stateId);
			state.EntityStatus = entitySatusHandler.GetById(state.EntityStatusId);

			return state;
		}

		public WorkflowStepDataContract GetStepById(Guid stepId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepHandler>();
			var handlerMA = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepManualActionHandler>();
			var step = handler.GetById(stepId);
			step.ManualActionList = handlerMA.GetAll(stepId); 
			return step;
		}

		public WorkflowStepInstanceDataContract GetStepInstanceById(Guid stepInstanceId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepInstanceHandler>();
			return handler.GetById(stepInstanceId);
		}

		public IEnumerable<WorkflowStateTransitionDataContract> GetAllWorkflowStateTransition(Guid workflowId)
		{
			var stateHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateHandler>();
			var tranHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStateTransitionHandler>();

			var transitions = tranHandler.GetAll(workflowId);
			foreach(var item in transitions)
			{
				item.FromState = stateHandler.GetById(item.FromStateId);
				item.ToState = stateHandler.GetById(item.ToStateId);
			}
			return transitions;
		}

		public IEnumerable<StepTypeDataContract> GetAllStepType()
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IStepTypeHandler>();
			return handler.GetAll();
		}
		public IEnumerable<ParticipantDataContract> GetAllParticipant()
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IParticipantHandler>();
			return handler.GetAll();
		}
		public StepTypeDataContract GetStepTypeByCommand(string command)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IStepTypeHandler>();
			return handler.GetByDescription(command);
		}

		public StepTypeDataContract GetStepTypeById(Guid id)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IStepTypeHandler>();
			return handler.GetById(id);
		}

		public IEnumerable<WorkflowStepDataContract> GetSteps(Guid workflowId, Guid fromStateId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepHandler>();
			var handlerMA = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepManualActionHandler>();
			var handlerTD = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepTransitionDecisionHandler>();
			var handlerDC = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepDecisionConditionHandler>();

			var list = handler.GetAll(workflowId, fromStateId);

			foreach (var item in list)
			{
				item.StepType = GetStepTypeById(item.StepTypeId);
				item.ManualActionList = new List<ManualActionDataContract>(handlerMA.GetAll(item.Id));
				item.TransitionDecisionList = new List<TransitionDecisionDataContract>(handlerTD.GetAll(item.Id));
				item.DecisionConditionList = new List<DecisionConditionDataContract>(handlerDC.GetAll(item.Id));
			}

			return list;
		}

		public IEnumerable<WorkflowStepTransitionDataContract> GetStepsTransitions(Guid workflowId, Guid fromStateId)
		{
			var stepHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepHandler>();
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepTransitionHandler>();
			var list = handler.GetAll(workflowId, fromStateId);

			foreach (var item in list)
			{
				item.FromStep = stepHandler.GetById(item.FromStepId);
				item.ToStep = stepHandler.GetById(item.ToStepId);
			}
			return list;
		}

		public OperationResult SaveTransitionDetail(Guid workflowId, Guid fromStateId, IEnumerable<WorkflowStepDataContract> steps, IEnumerable<WorkflowStepTransitionDataContract> transitions)
		{
			var stepHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepHandler>();
			var tranHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepTransitionHandler>();

			var originalStepsList = stepHandler.GetAll(workflowId, fromStateId);
			var originalTranList = tranHandler.GetAll(workflowId, fromStateId);

			foreach (var item in originalTranList)
				if (!transitions.Any(o => o.Id.Equals(item.Id)))
					tranHandler.Delete(item);

			foreach (var item in originalStepsList)
				if (!steps.Any(o => o.Id.Equals(item.Id)))
					stepHandler.Delete(item);


			foreach (var item in steps)
			{
				SaveStep(item);
			}
			foreach (var item in transitions)
			{
				SaveStepTransition(item);
			}

			return new OperationResult();
		}

		public IEnumerable<WorkflowAssignmentDataContract> GetPendingRequestsByResourceId(Guid resourceId)
		{
			var pHandler = DataAccess.Core.GetFactory().GetInstance<IParticipantHandler>();
			var resHandler = DataAccess.Core.GetFactory().GetInstance<IResourceHandler>();
			var stepHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepHandler>();
			var stHandler = DataAccess.Core.GetFactory().GetInstance<IStepTypeHandler>();
			var wfAssignmentHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowAssignmentHandler>();
			var entityInstanceHandler = DataAccess.Core.GetFactory().GetInstance<IEntityInstanceHandler>();
			var assignments = wfAssignmentHandler.GetAll(resourceId);

			foreach (var item in assignments)
			{
				item.Step = stepHandler.GetById(item.StepId);
				item.StepType = stHandler.GetById(item.Step.StepTypeId);
				item.EntityInstance = entityInstanceHandler.GetById(item.EntityInstanceId);
				if (item.ParticipantEntity == "R")
					item.ParticipantDescription = resHandler.GetById(item.ParticipantId).Name;
				else
				{
					var p = pHandler.GetById(item.ParticipantId);
					if (p != null)
						item.ParticipantDescription = p.Description;
					else
						item.ParticipantDescription = "Não encontrado!";
				}
			}

			return assignments;
		}


		public WorkflowStepTransitionDataContract GetStepTransitionById(Guid transitionId)
		{
			var tranHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepTransitionHandler>();
			return tranHandler.GetById(transitionId);
		}

		public IEnumerable<TransitionDecisionDataContract> GetStepTransitionDecisions(Guid stepId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepTransitionDecisionHandler>();
			return handler.GetAll(stepId);
		}
		
		public OperationResult ProcessManualStep(Guid workflowStepInstanceId, Guid stepId, string resultCode, byte[] file, string fileName)
		{
			var assignmentHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowAssignmentHandler>();
			var stepInstanceHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepInstanceHandler>();
			var stepInstance = stepInstanceHandler.GetById(workflowStepInstanceId);

			WorkflowStepDataContract step = this.GetStepById(stepId);
			step.TransitionDecisionList = this.GetStepTransitionDecisions(stepId);
			stepInstance.Completed = true;
			stepInstance.CompletedBy = Common.SessionData.ResourceId;
			stepInstance.CompletedOn = DateTime.Now;

			var assignment = assignmentHandler.GetByStepInstanceId(workflowStepInstanceId);


			if (file != null)
			{
				System.Data.Linq.Binary b = new System.Data.Linq.Binary(file);
				stepInstanceHandler.AttachFile(b, fileName, assignment.EntityInstanceId, stepId);
			}

			stepInstanceHandler.Update(stepInstance);
			assignmentHandler.Delete(assignment);

			
			bool matched = false;
			foreach(var item in step.TransitionDecisionList)
				if (item.Code.Equals(resultCode))
				{
					var transition = this.GetStepTransitionById(item.TransitionId);
					var newStepInstance = new WorkflowStepInstanceDataContract();
					newStepInstance.StepId = transition.ToStepId;
					newStepInstance.Id = Guid.NewGuid();
					newStepInstance.WorkflowId = transition.WorkflowId;
					newStepInstance.WorkflowInstanceId = stepInstance.WorkflowInstanceId;

					stepInstanceHandler.Add(newStepInstance);
					matched = true;
				}

			if (!matched)
			{
				var transitionList = this.GetStepsTransitions(step.WorkflowId, step.FromStateId);
				transitionList = transitionList.Where(o => o.FromStepId.Equals(step.Id)).ToList();
				foreach (var item in transitionList)
				{
					var newStepInstance = new WorkflowStepInstanceDataContract();
					newStepInstance.StepId = item.ToStepId;
					newStepInstance.Id = Guid.NewGuid();
					newStepInstance.WorkflowId = item.WorkflowId;
					newStepInstance.WorkflowInstanceId = stepInstance.WorkflowInstanceId;

					stepInstanceHandler.Add(newStepInstance);
				}

			}

			return new OperationResult();
		}

		public void ProcessWorkflow()
		{
			try
			{
				var handler = DataAccess.Core.GetFactory().GetInstance<IWorkflowStepInstanceHandler>();
				var list = handler.GetAll(true);
				foreach (var item in list)
					DataAccess.Core.MainDataContextInstance().SGW_ProcessWorkflowStep(item.Id);
			}
			catch (Exception ex)
			{
				ex.ToString();
			}
		}

		public void AddEntityInstance(Guid entityId, IEnumerable<KeyValuePair<string, object>> data)
		{
			var entityHandler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			var entityFieldsHandler = DataAccess.Core.GetFactory().GetInstance<IEntityFieldHandler>();
			var entity = entityHandler.GetById(entityId);
			entity.EntityFields = entityFieldsHandler.GetAll(entityId);

			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityInstanceHandler>();
			var entityInstance = new EntityInstanceDataContract();
			entityInstance.EntityId = entityId;
			entityInstance.Id = Guid.NewGuid();

			var list = new List<EntityInstanceValueDataContract>();

			foreach(var item in data)
			{
				object value = null;
				try
				{
					value = item.Value;

					var field = entity.EntityFields.Where(o => o.Name.Equals(item.Key)).FirstOrDefault();

					if (value != null && field != null)
					{
						var entityValue = new EntityInstanceValueDataContract();
						entityValue.Id = Guid.NewGuid();
						entityValue.FieldName = field.Name;
						entityValue.EntityId = entityId;
						entityValue.EntityInstanceId = entityInstance.Id;
						switch(field.FieldType)
						{
							case "C":
								entityValue.IdValue = Guid.Parse(value.ToString());
								break;
							case "N":
								entityValue.NumberValue = decimal.Parse(value.ToString());
								break;
							case "D":
								entityValue.DateValue = DateTime.Parse(value.ToString());
								break;
							case "T":
								entityValue.TextValue = value.ToString();
								break;
							default:
								continue;
						}

						list.Add(entityValue);
					}
				}
				catch
				{
					continue;
				}
			}

			entityInstance.EntityValues = list;
			
			foreach (var item in list)
			{
				if (entity.StatusField.Equals(item.FieldName, StringComparison.CurrentCultureIgnoreCase))
					entityInstance.CurrentStatus = item.Value;

				if (entity.UserDefinedCodeField.Equals(item.FieldName, StringComparison.CurrentCultureIgnoreCase))
					entityInstance.UserDefinedCode = item.Value;
			}

			entityInstance.CreatedOn = DateTime.Now;

			handler.Add(entityInstance);
		}
	}
}
