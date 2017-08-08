using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowStepTransitionHandler : BaseHandler<WorkflowStepTransitionDataContract, SGW_WorkflowStepTransition>, IWorkflowStepTransitionHandler
    {
		public override Common.OperationResult Add(Common.DataContract.WorkflowStepTransitionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStepTransitions.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(WorkflowStepTransitionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowStepTransition obj = Core.MainDataContextInstance().SGW_WorkflowStepTransitions.Where(w => w.TransitionId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowStepTransitionDataContract GetDataContract(SGW_WorkflowStepTransition linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.WorkflowStepTransitionDataContract dataContract = new Common.DataContract.WorkflowStepTransitionDataContract();
			dataContract.Id = linqObj.TransitionId;
			dataContract.Description = linqObj.Name;
			dataContract.FromStepId = linqObj.FromStepId;
			dataContract.ToStepId = linqObj.ToStepId;
			dataContract.WorkflowId = linqObj.WorkflowId;
			dataContract.FromStateId = linqObj.FromStateId;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			return dataContract;
		}


		public override SGW_WorkflowStepTransition GetLinqObj(Common.DataContract.WorkflowStepTransitionDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowStepTransition());
		}

		public override SGW_WorkflowStepTransition GetLinqObj(Common.DataContract.WorkflowStepTransitionDataContract dataContract, SGW_WorkflowStepTransition linq)
		{
			if (dataContract == null)
				return null;

			linq.TransitionId = dataContract.Id;
			linq.Name = dataContract.Description;
			linq.FromStepId = dataContract.FromStepId;
			linq.ToStepId = dataContract.ToStepId;
			linq.WorkflowId = dataContract.WorkflowId;
			linq.FromStateId = dataContract.FromStateId;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.WorkflowStepTransitionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_WorkflowStepTransition obj = Core.MainDataContextInstance().SGW_WorkflowStepTransitions.Where(o => o.TransitionId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Status da Entidade não encontrado.");

				Core.MainDataContextInstance().SGW_WorkflowStepTransitions.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowStepTransitionDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowStepTransition obj = Core.MainDataContextInstance().SGW_WorkflowStepTransitions.Where(o => o.TransitionId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.WorkflowStepTransitionDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepTransitions.Select(o => GetDataContract(o)).ToList();
		}

		public override WorkflowStepTransitionDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_WorkflowStepTransition obj = Core.MainDataContextInstance().SGW_WorkflowStepTransitions.Where(o => o.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);			
		}

		public IEnumerable<WorkflowStepTransitionDataContract> GetAll(Guid workflowId, Guid fromStateId)
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepTransitions.Where(o => o.WorkflowId.Equals(workflowId) && o.FromStateId.Equals(fromStateId)).Select(o => GetDataContract(o)).ToList();
		}
	}
}
