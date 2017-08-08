using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowStateHandler : BaseHandler<WorkflowStateDataContract, SGW_WorkflowState>, IWorkflowStateHandler
    {
		public override Common.OperationResult Add(Common.DataContract.WorkflowStateDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStates.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.OperationResult Update(WorkflowStateDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowState workflowState = Core.MainDataContextInstance().SGW_WorkflowStates.Where(w => w.StateId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, workflowState);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowStateDataContract GetDataContract(SGW_WorkflowState linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.WorkflowStateDataContract dataContract = new Common.DataContract.WorkflowStateDataContract();
			dataContract.Id = linqObj.StateId;
			dataContract.Description = linqObj.Name;
			dataContract.WorkflowId = linqObj.WorkflowId;
			dataContract.UILeftPosition = linqObj.UILeftPosition;
			dataContract.UITopPosition = linqObj.UITopPosition;
			dataContract.EntityStatusId = linqObj.EntityStatusId;
			dataContract.InitialState = linqObj.InitialState;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			return dataContract;
		}


		public override SGW_WorkflowState GetLinqObj(Common.DataContract.WorkflowStateDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowState());
		}
		public override SGW_WorkflowState GetLinqObj(Common.DataContract.WorkflowStateDataContract dataContract, SGW_WorkflowState linq)
		{
			if (dataContract == null)
				return null;

			linq.StateId = dataContract.Id;
			linq.Name = dataContract.Description;
			linq.WorkflowId = dataContract.WorkflowId;
			linq.UILeftPosition = dataContract.UILeftPosition;
			linq.UITopPosition = dataContract.UITopPosition;
			linq.EntityStatusId = dataContract.EntityStatusId;
			linq.InitialState = dataContract.InitialState;
			linq.UpdatedBy = dataContract.UpdatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;

			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.WorkflowStateDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStates.DeleteOnSubmit(
					Core.MainDataContextInstance().SGW_WorkflowStates.Where(o => o.StateId.Equals(dataContract.Id)).First());
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowStateDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowState wf = Core.MainDataContextInstance().SGW_WorkflowStates.Where(w => w.StateId.Equals(id)).FirstOrDefault();

			return GetDataContract(wf);
		}

		public override IEnumerable<Common.DataContract.WorkflowStateDataContract> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Common.DataContract.WorkflowStateDataContract> GetAll(Guid workflowId)
		{
			return Core.MainDataContextInstance().SGW_WorkflowStates.Where(w => w.WorkflowId.Equals(workflowId)).Select(o => GetDataContract(o)).ToList();
		}

		public override WorkflowStateDataContract GetByDescription(string description)
		{
			throw new NotImplementedException();
		}

		public WorkflowStateDataContract GetByDescription(string description, Guid workflowId)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			if (workflowId == null || workflowId == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "workflowId");

			SGW_WorkflowState wf = Core.MainDataContextInstance().SGW_WorkflowStates.Where(w => w.Name.Equals(description) && w.WorkflowId.Equals(workflowId)).FirstOrDefault();
			return GetDataContract(wf);			
		}
	}
}
