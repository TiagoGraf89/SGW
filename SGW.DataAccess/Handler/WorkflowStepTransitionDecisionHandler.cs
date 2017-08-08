using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowStepTransitionDecisionHandler : BaseHandler<TransitionDecisionDataContract, SGW_WorkflowStepTransitionDecision>, IWorkflowStepTransitionDecisionHandler
    {
		public override Common.OperationResult Add(Common.DataContract.TransitionDecisionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(TransitionDecisionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowStepTransitionDecision obj = Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.Where(w => w.TransitionDecisionId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.TransitionDecisionDataContract GetDataContract(SGW_WorkflowStepTransitionDecision linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.TransitionDecisionDataContract dataContract = new Common.DataContract.TransitionDecisionDataContract();
			dataContract.Id = linqObj.TransitionDecisionId;
			dataContract.Code = linqObj.Code;
			dataContract.StepId = linqObj.FromStepId;
			dataContract.TransitionId = linqObj.TransitionId;
			return dataContract;
		}


		public override SGW_WorkflowStepTransitionDecision GetLinqObj(Common.DataContract.TransitionDecisionDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowStepTransitionDecision());
		}

		public override SGW_WorkflowStepTransitionDecision GetLinqObj(Common.DataContract.TransitionDecisionDataContract dataContract, SGW_WorkflowStepTransitionDecision linq)
		{
			if (dataContract == null)
				return null;

			linq.TransitionDecisionId = dataContract.Id;
			linq.Code = dataContract.Code;
			linq.FromStepId = dataContract.StepId;
			linq.TransitionId = dataContract.TransitionId;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.TransitionDecisionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_WorkflowStepTransitionDecision obj = Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.Where(o => o.TransitionDecisionId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Status da Entidade não encontrado.");

				Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.TransitionDecisionDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowStepTransitionDecision obj = Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.Where(o => o.TransitionDecisionId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.TransitionDecisionDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.Select(o => GetDataContract(o)).ToList();
		}

		public override TransitionDecisionDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_WorkflowStepTransitionDecision obj = Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.Where(o => o.Code.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);			
		}

		public OperationResult DeleteAll(Guid stepId)
		{
			var list = Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.Where(o => o.FromStepId.Equals(stepId)).ToList();
			Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.DeleteAllOnSubmit(list);
			Core.MainDataContextInstance().SubmitChanges();
			return new OperationResult();
		}

		public IEnumerable<Common.DataContract.TransitionDecisionDataContract> GetAll(Guid stepId)
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepTransitionDecisions.Where(o => o.FromStepId.Equals(stepId)).Select(o => GetDataContract(o)).ToList();
		}
	}
}
