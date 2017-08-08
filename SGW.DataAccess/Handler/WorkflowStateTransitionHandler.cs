using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowStateTransitionHandler : BaseHandler<WorkflowStateTransitionDataContract, SGW_WorkflowStateTransition>, IWorkflowStateTransitionHandler
    {
		public override Common.OperationResult Add(Common.DataContract.WorkflowStateTransitionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStateTransitions.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.OperationResult Update(WorkflowStateTransitionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowStateTransition workflowState = Core.MainDataContextInstance().SGW_WorkflowStateTransitions.Where(w => w.StateTransitionId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, workflowState);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowStateTransitionDataContract GetDataContract(SGW_WorkflowStateTransition linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.WorkflowStateTransitionDataContract dataContract = new Common.DataContract.WorkflowStateTransitionDataContract();
			dataContract.Id = linqObj.StateTransitionId;
			dataContract.Description = linqObj.Name;
			dataContract.WorkflowId = linqObj.WorkflowId;
			dataContract.FromStateId = linqObj.FromStateId;
			dataContract.ToStateId = linqObj.ToStateId;
			dataContract.AutoTransition = linqObj.AutoTransition;

			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			return dataContract;
		}


		public override SGW_WorkflowStateTransition GetLinqObj(Common.DataContract.WorkflowStateTransitionDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowStateTransition());
		}
		public override SGW_WorkflowStateTransition GetLinqObj(Common.DataContract.WorkflowStateTransitionDataContract dataContract, SGW_WorkflowStateTransition linq)
		{
			if (dataContract == null)
				return null;

			linq.StateTransitionId = dataContract.Id;
			linq.Name = dataContract.Description;
			linq.WorkflowId = dataContract.WorkflowId;
			linq.ToStateId = dataContract.ToStateId;
			linq.FromStateId = dataContract.FromStateId;
			linq.AutoTransition = dataContract.AutoTransition;

			linq.UpdatedBy = dataContract.UpdatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;

			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.WorkflowStateTransitionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStateTransitions.DeleteOnSubmit(
					Core.MainDataContextInstance().SGW_WorkflowStateTransitions.Where(o => o.StateTransitionId.Equals(dataContract.Id)).First());
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowStateTransitionDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowStateTransition wf = Core.MainDataContextInstance().SGW_WorkflowStateTransitions.Where(w => w.StateTransitionId.Equals(id)).FirstOrDefault();
			return GetDataContract(wf);
		}

		public override IEnumerable<Common.DataContract.WorkflowStateTransitionDataContract> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Common.DataContract.WorkflowStateTransitionDataContract> GetAll(Guid workflowId)
		{
			return Core.MainDataContextInstance().SGW_WorkflowStateTransitions.Where(w => w.WorkflowId.Equals(workflowId)).Select(o => GetDataContract(o)).ToList();			
		}
		
		public override WorkflowStateTransitionDataContract GetByDescription(string description)
		{
			throw new NotImplementedException();
		}

	}
}
