using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowHandler : BaseHandler<WorkflowDataContract, SGW_Workflow>, IWorkflowHandler
    {
		public override Common.OperationResult Add(Common.DataContract.WorkflowDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_Workflows.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.OperationResult Update(WorkflowDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_Workflow workflow = Core.MainDataContextInstance().SGW_Workflows.Where(w => w.WorkflowId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, workflow);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowDataContract GetDataContract(SGW_Workflow linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.WorkflowDataContract dataContract = new Common.DataContract.WorkflowDataContract();
			dataContract.Id = linqObj.WorkflowId;
			dataContract.Description = linqObj.Name;
			dataContract.Active = linqObj.Active;
			dataContract.EntityId = linqObj.EntityId;
			dataContract.StartConditionId = linqObj.TriggerConditionId;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			return dataContract;
		}


		public override SGW_Workflow GetLinqObj(Common.DataContract.WorkflowDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_Workflow());
		}
		public override SGW_Workflow GetLinqObj(Common.DataContract.WorkflowDataContract dataContract, SGW_Workflow linq)
		{
			if (dataContract == null)
				return null;

			linq.Name = dataContract.Description;
			linq.WorkflowId = dataContract.Id;
			linq.Active = dataContract.Active;
			linq.EntityId = dataContract.EntityId;
			linq.TriggerConditionId = dataContract.StartConditionId ;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.WorkflowDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				var wf = Core.MainDataContextInstance().SGW_Workflows.Where(o => o.WorkflowId.Equals(dataContract.Id)).First();
				Core.MainDataContextInstance().SGW_Workflows.DeleteOnSubmit(wf);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_Workflow wf = Core.MainDataContextInstance().SGW_Workflows.Where(w => w.WorkflowId.Equals(id)).FirstOrDefault();
			return GetDataContract(wf);
		}

		public override IEnumerable<Common.DataContract.WorkflowDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_Workflows.Select(o => GetDataContract(o)).ToList();			
		}

		public override WorkflowDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_Workflow wf = Core.MainDataContextInstance().SGW_Workflows.Where(w => w.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(wf);			
		}
	}
}
