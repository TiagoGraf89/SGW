using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class ConditionDetailHandler : BaseHandler<ConditionDetailDataContract, SGW_ConditionDetail>, IConditionDetailHandler
    {
		public override Common.OperationResult Add(Common.DataContract.ConditionDetailDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_ConditionDetails.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(ConditionDetailDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_ConditionDetail obj = Core.MainDataContextInstance().SGW_ConditionDetails.Where(w => w.ConditionDetailId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ConditionDetailDataContract GetDataContract(SGW_ConditionDetail linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.ConditionDetailDataContract dataContract = new Common.DataContract.ConditionDetailDataContract();
			dataContract.Id = linqObj.ConditionDetailId;
			dataContract.ConditionId = linqObj.ConditionId;
			dataContract.GroupIdentifier = linqObj.GroupIdentifier;
			dataContract.Field = linqObj.Field;
			dataContract.Operator = linqObj.Operator;
			dataContract.Value1 = linqObj.Value1;
			dataContract.Value2 = linqObj.Value2;

			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			return dataContract;
		}


		public override SGW_ConditionDetail GetLinqObj(Common.DataContract.ConditionDetailDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_ConditionDetail());
		}

		public override SGW_ConditionDetail GetLinqObj(Common.DataContract.ConditionDetailDataContract dataContract, SGW_ConditionDetail linq)
		{
			if (dataContract == null)
				return null;

			linq.ConditionDetailId = dataContract.Id;
			linq.ConditionId = dataContract.ConditionId;
			linq.GroupIdentifier = dataContract.GroupIdentifier;
			linq.Field = dataContract.Field;
			linq.Operator = dataContract.Operator;
			linq.Value1 = dataContract.Value1;
			linq.Value2 = dataContract.Value2;

			linq.CreatedOn = dataContract.CreatedOn;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.ConditionDetailDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_ConditionDetail obj = Core.MainDataContextInstance().SGW_ConditionDetails.Where(o => o.ConditionDetailId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Condição não encontrada.");

				Core.MainDataContextInstance().SGW_ConditionDetails.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ConditionDetailDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_ConditionDetail obj = Core.MainDataContextInstance().SGW_ConditionDetails.Where(o => o.ConditionDetailId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.ConditionDetailDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_ConditionDetails.Select(o => GetDataContract(o)).ToList();
		}

		public IEnumerable<ConditionDetailDataContract> GetAll(Guid conditionId)
		{
			return Core.MainDataContextInstance().SGW_ConditionDetails.Where(o => o.ConditionId.Equals(conditionId)).Select(o => GetDataContract(o)).ToList();
		}

		public override Common.DataContract.ConditionDetailDataContract GetByDescription(string desc)
		{
			throw new NotImplementedException();
		}
	}
}
