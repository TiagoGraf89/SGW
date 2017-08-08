using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowStepDecisionConditionHandler : BaseHandler<DecisionConditionDataContract, SGW_WorkflowStepDecisionCondition>, IWorkflowStepDecisionConditionHandler
    {
		public override Common.OperationResult Add(Common.DataContract.DecisionConditionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(DecisionConditionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowStepDecisionCondition obj = Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.Where(w => w.DecisionConditionId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.DecisionConditionDataContract GetDataContract(SGW_WorkflowStepDecisionCondition linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.DecisionConditionDataContract dataContract = new Common.DataContract.DecisionConditionDataContract();
			dataContract.Id = linqObj.DecisionConditionId;
			dataContract.ConditionId = linqObj.ConditionId;
			dataContract.StepId = linqObj.StepId;
			dataContract.Code = linqObj.Code;
			return dataContract;
		}


		public override SGW_WorkflowStepDecisionCondition GetLinqObj(Common.DataContract.DecisionConditionDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowStepDecisionCondition());
		}

		public override SGW_WorkflowStepDecisionCondition GetLinqObj(Common.DataContract.DecisionConditionDataContract dataContract, SGW_WorkflowStepDecisionCondition linq)
		{
			if (dataContract == null)
				return null;

			linq.DecisionConditionId = dataContract.Id;
			linq.Code = dataContract.Code;
			linq.ConditionId = dataContract.ConditionId;
			linq.StepId = dataContract.StepId;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.DecisionConditionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_WorkflowStepDecisionCondition obj = Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.Where(o => o.DecisionConditionId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Status da Entidade não encontrado.");

				Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.DecisionConditionDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowStepDecisionCondition obj = Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.Where(o => o.DecisionConditionId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.DecisionConditionDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.Select(o => GetDataContract(o)).ToList();
		}

		public override DecisionConditionDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_WorkflowStepDecisionCondition obj = Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.Where(o => o.Code.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public OperationResult DeleteAll(Guid stepId)
		{
			var list = Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.Where(o => o.StepId.Equals(stepId)).ToList();
			Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.DeleteAllOnSubmit(list);
			Core.MainDataContextInstance().SubmitChanges();
			return new OperationResult();
		}

		public IEnumerable<Common.DataContract.DecisionConditionDataContract> GetAll(Guid stepId)
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepDecisionConditions.Where(o => o.StepId.Equals(stepId)).Select(o => GetDataContract(o)).ToList();
		}
	}
}
