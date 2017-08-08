using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class EntityInstanceHandler : BaseHandler<EntityInstanceDataContract, SGW_EntityInstance>, IEntityInstanceHandler
    {
		public override Common.OperationResult Add(Common.DataContract.EntityInstanceDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_EntityInstances.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();

				Core.MainDataContextInstance().SGW_EntityInstanceValues.InsertAllOnSubmit(dataContract.EntityValues.Select(o => new SGW_EntityInstanceValue()
					{
						DataValue = o.DateValue,
						EntityId = o.EntityId,
						EntityInstanceId = o.EntityInstanceId,
						FieldName = o.FieldName,
						IdValue = o.IdValue,
						NumberValue = o.NumberValue,
						TextValue = o.TextValue,
						ValueId = o.Id
					}));
				Core.MainDataContextInstance().SubmitChanges();

				Core.MainDataContextInstance().SGW_StartWorkflows(dataContract.Id, dataContract.EntityId, dataContract.UserDefinedCode);

				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.OperationResult Update(EntityInstanceDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_EntityInstance workflowState = Core.MainDataContextInstance().SGW_EntityInstances.Where(w => w.EntityInstanceId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, workflowState);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.EntityInstanceDataContract GetDataContract(SGW_EntityInstance linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.EntityInstanceDataContract dataContract = new Common.DataContract.EntityInstanceDataContract();
			dataContract.Id = linqObj.EntityInstanceId;
			dataContract.CurrentStatus = linqObj.CurrentStatus;
			dataContract.EntityId = linqObj.EntityId;
			dataContract.EntityValues = linqObj.SGW_EntityInstanceValues.Select(o=> GetEntityInstanceValueDataContract(o)).ToList();
			dataContract.UserDefinedCode = linqObj.UserDefinedCode;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			return dataContract;
		}

		public EntityInstanceValueDataContract GetEntityInstanceValueDataContract(SGW_EntityInstanceValue linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.EntityInstanceValueDataContract dataContract = new Common.DataContract.EntityInstanceValueDataContract();
			dataContract.Id = linqObj.ValueId;
			dataContract.DateValue = linqObj.DataValue;
			dataContract.EntityId = linqObj.EntityId;
			dataContract.EntityInstanceId = linqObj.EntityInstanceId;
			dataContract.FieldName = linqObj.FieldName;
			dataContract.IdValue = linqObj.IdValue;
			dataContract.NumberValue = linqObj.NumberValue;
			dataContract.TextValue = linqObj.TextValue;
			return dataContract;

		}

		public override SGW_EntityInstance GetLinqObj(Common.DataContract.EntityInstanceDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_EntityInstance());
		}
		public override SGW_EntityInstance GetLinqObj(Common.DataContract.EntityInstanceDataContract dataContract, SGW_EntityInstance linq)
		{
			if (dataContract == null)
				return null;

			linq.EntityInstanceId = dataContract.Id;
			linq.EntityId = dataContract.EntityId;
			linq.CurrentStatus = dataContract.CurrentStatus;
			linq.UserDefinedCode = dataContract.UserDefinedCode;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;

			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.EntityInstanceDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				Core.MainDataContextInstance().SGW_EntityInstances.DeleteOnSubmit(
					Core.MainDataContextInstance().SGW_EntityInstances.Where(o => o.EntityInstanceId.Equals(dataContract.Id)).First());
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.EntityInstanceDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_EntityInstance wf = Core.MainDataContextInstance().SGW_EntityInstances.Where(w => w.EntityInstanceId.Equals(id)).FirstOrDefault();

			var dt = GetDataContract(wf);

			var eHandler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			if (dt != null)
				dt.Entity = eHandler.GetById(dt.EntityId);

			return dt;

		}

		public override IEnumerable<Common.DataContract.EntityInstanceDataContract> GetAll()
		{			
			return Core.MainDataContextInstance().GetTable<SGW_EntityInstance>().OrderByDescending(o=>o.CreatedOn).Select(o => GetDataContract(o)).ToList();
		}

		public override EntityInstanceDataContract GetByDescription(string description)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<KeyValuePair<Guid, string>> GetCurrentStepsDescription(Guid entityInstanceId)
		{
			return (from wi in Core.MainDataContextInstance().SGW_WorkflowInstances
					join wsi in Core.MainDataContextInstance().SGW_WorkflowStepInstances on wi.WorkflowInstanceId equals wsi.WorkflowInstanceId
					join step in Core.MainDataContextInstance().SGW_WorkflowSteps on wsi.StepId equals step.StepId
					where wi.EntityInstanceId.Equals(entityInstanceId)
					where wsi.Completed.Equals(false)
					select new KeyValuePair<Guid,string>(step.StepId, step.Name)).ToList();
		}
	}
}
