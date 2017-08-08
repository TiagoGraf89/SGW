using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowStepManualActionHandler : BaseHandler<ManualActionDataContract, SGW_WorkflowStepManualAction>, IWorkflowStepManualActionHandler
    {
		public override Common.OperationResult Add(Common.DataContract.ManualActionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowStepManualActions.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(ManualActionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowStepManualAction obj = Core.MainDataContextInstance().SGW_WorkflowStepManualActions.Where(w => w.ManualActionId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ManualActionDataContract GetDataContract(SGW_WorkflowStepManualAction linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.ManualActionDataContract dataContract = new Common.DataContract.ManualActionDataContract();
			dataContract.Id = linqObj.ManualActionId;
			dataContract.Description = linqObj.Name;
			dataContract.Code = linqObj.Code;
			dataContract.StepId = linqObj.StepId;
			return dataContract;
		}


		public override SGW_WorkflowStepManualAction GetLinqObj(Common.DataContract.ManualActionDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowStepManualAction());
		}

		public override SGW_WorkflowStepManualAction GetLinqObj(Common.DataContract.ManualActionDataContract dataContract, SGW_WorkflowStepManualAction linq)
		{
			if (dataContract == null)
				return null;

			linq.ManualActionId = dataContract.Id;
			linq.Name = dataContract.Description;
			linq.Code = dataContract.Code;
			linq.StepId = dataContract.StepId;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.ManualActionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_WorkflowStepManualAction obj = Core.MainDataContextInstance().SGW_WorkflowStepManualActions.Where(o => o.ManualActionId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Status da Entidade não encontrado.");

				Core.MainDataContextInstance().SGW_WorkflowStepManualActions.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ManualActionDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowStepManualAction obj = Core.MainDataContextInstance().SGW_WorkflowStepManualActions.Where(o => o.ManualActionId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.ManualActionDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepManualActions.Select(o => GetDataContract(o)).ToList();
		}

		public override ManualActionDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_WorkflowStepManualAction obj = Core.MainDataContextInstance().SGW_WorkflowStepManualActions.Where(o => o.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);			
		}

		public OperationResult DeleteAll(Guid stepId)
		{
			var list = Core.MainDataContextInstance().SGW_WorkflowStepManualActions.Where(o => o.StepId.Equals(stepId)).ToList();
			Core.MainDataContextInstance().SGW_WorkflowStepManualActions.DeleteAllOnSubmit(list);
			Core.MainDataContextInstance().SubmitChanges();
			return new OperationResult();
		}

		public IEnumerable<Common.DataContract.ManualActionDataContract> GetAll(Guid stepId)
		{
			return Core.MainDataContextInstance().SGW_WorkflowStepManualActions.Where(o => o.StepId.Equals(stepId)).Select(o=>GetDataContract(o)).ToList();
		}
	}
}
