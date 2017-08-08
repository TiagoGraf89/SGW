using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class ConditionHandler : BaseHandler<ConditionDataContract, SGW_Condition>, IConditionHandler
    {
		public override Common.OperationResult Add(Common.DataContract.ConditionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_Conditions.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(ConditionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_Condition obj = Core.MainDataContextInstance().SGW_Conditions.Where(w => w.ConditionId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ConditionDataContract GetDataContract(SGW_Condition linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.ConditionDataContract dataContract = new Common.DataContract.ConditionDataContract();
			dataContract.Id = linqObj.ConditionId;
			dataContract.Description = linqObj.Name;
			dataContract.ConditionType = linqObj.ConditionType;
			dataContract.SQLCommand= linqObj.SQLCommand;
			dataContract.StoredProcedure = linqObj.StoredProcedure;
			dataContract.EntityId = linqObj.EntityId;
			dataContract.GeneratedCommand = linqObj.GeneratedCommand;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			return dataContract;
		}


		public override SGW_Condition GetLinqObj(Common.DataContract.ConditionDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_Condition());
		}

		public override SGW_Condition GetLinqObj(Common.DataContract.ConditionDataContract dataContract, SGW_Condition linq)
		{
			if (dataContract == null)
				return null;
			
			linq.Name = dataContract.Description;
			linq.ConditionId = dataContract.Id;
			linq.ConditionType = dataContract.ConditionType;
			linq.EntityId = dataContract.EntityId;
			linq.SQLCommand = dataContract.SQLCommand;
			linq.StoredProcedure = dataContract.StoredProcedure;
			linq.GeneratedCommand = dataContract.GeneratedCommand;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.ConditionDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_Condition obj = Core.MainDataContextInstance().SGW_Conditions.Where(o => o.ConditionId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Condição não encontrada.");

				Core.MainDataContextInstance().SGW_Conditions.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ConditionDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_Condition obj = Core.MainDataContextInstance().SGW_Conditions.Where(o => o.ConditionId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.ConditionDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_Conditions.Select(o => GetDataContract(o)).ToList();
		}

		public override Common.DataContract.ConditionDataContract GetByDescription(string desc)
		{
			throw new NotImplementedException();
		}
	}
}
