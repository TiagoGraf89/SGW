using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowStepInstanceHandler : BaseHandler<WorkflowStepInstanceDataContract, SGW_WorkflowStepInstance>, IWorkflowStepInstanceHandler
    {
		public override Common.OperationResult Add(Common.DataContract.WorkflowStepInstanceDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			dataContract.CreatedBy = Common.SessionData.ResourceId;
			dataContract.CreatedOn = DateTime.Now;

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStepInstances.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public Common.OperationResult AttachFile(System.Data.Linq.Binary file, string fileName, Guid entityInstanceId, Guid stepId)
		{

			try
			{
				Core.MainDataContextInstance().SGW_Attachments.InsertOnSubmit(new SGW_Attachment() {
					AttachedFile = file,
					AttachmentId = Guid.NewGuid(),
					Comments = "",
					CreatedBy = SGW.Common.SessionData.ResourceId,
					CreatedOn = DateTime.Now,
					EntityInstanceId = entityInstanceId,
					Name = fileName,
					StepId = stepId				
				});
				Core.MainDataContextInstance().SubmitChanges();

				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		
		public override Common.OperationResult Update(WorkflowStepInstanceDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowStepInstance obj = Core.MainDataContextInstance().SGW_WorkflowStepInstances.Where(w => w.WorkflowStepInstanceId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowStepInstanceDataContract GetDataContract(SGW_WorkflowStepInstance linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.WorkflowStepInstanceDataContract dataContract = new Common.DataContract.WorkflowStepInstanceDataContract();
			dataContract.Id = linqObj.WorkflowStepInstanceId;
			dataContract.WorkflowId = linqObj.WorkflowId;
			dataContract.StepId = linqObj.StepId;
			dataContract.WorkflowInstanceId = linqObj.WorkflowInstanceId;
			dataContract.Completed = linqObj.Completed;
			dataContract.CompletedOn = linqObj.CompletedOn;
			dataContract.CompletedBy = linqObj.CompletedBy;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			return dataContract;
		}


		public override SGW_WorkflowStepInstance GetLinqObj(Common.DataContract.WorkflowStepInstanceDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowStepInstance());
		}

		public override SGW_WorkflowStepInstance GetLinqObj(Common.DataContract.WorkflowStepInstanceDataContract dataContract, SGW_WorkflowStepInstance linq)
		{
			if (dataContract == null)
				return null;

			linq.WorkflowStepInstanceId = dataContract.Id;
			linq.WorkflowInstanceId = dataContract.WorkflowInstanceId;
			linq.WorkflowId = dataContract.WorkflowId;
			linq.StepId = dataContract.StepId;
			linq.Completed = dataContract.Completed;
			linq.CompletedOn = dataContract.CompletedOn;
			linq.CompletedBy = dataContract.CompletedBy;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.WorkflowStepInstanceDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_WorkflowStepInstance obj = Core.MainDataContextInstance().SGW_WorkflowStepInstances.Where(o => o.WorkflowStepInstanceId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Entidade não encontrada.");

				Core.MainDataContextInstance().SGW_WorkflowStepInstances.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowStepInstanceDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowStepInstance obj = Core.MainDataContextInstance().SGW_WorkflowStepInstances.Where(o => o.WorkflowStepInstanceId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public IEnumerable<WorkflowStepInstanceDataContract> GetAll(bool active)
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepInstances.Where( o => o.Completed != active).Select(o => GetDataContract(o)).ToList();
		}
		public override IEnumerable<Common.DataContract.WorkflowStepInstanceDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepInstances.Select(o => GetDataContract(o)).ToList();
		}

		public override WorkflowStepInstanceDataContract GetByDescription(string description)
		{
			throw new NotImplementedException();
		}

	}
}
