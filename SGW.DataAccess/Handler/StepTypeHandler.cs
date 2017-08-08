using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class StepTypeHandler : BaseHandler<StepTypeDataContract, SGW_StepType>, IStepTypeHandler
    {
		public override Common.OperationResult Add(Common.DataContract.StepTypeDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_StepTypes.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(StepTypeDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_StepType obj = Core.MainDataContextInstance().SGW_StepTypes.Where(w => w.StepTypeId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.StepTypeDataContract GetDataContract(SGW_StepType linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.StepTypeDataContract dataContract = new Common.DataContract.StepTypeDataContract();
			dataContract.Id = linqObj.StepTypeId;
			dataContract.Description = linqObj.Name;
			dataContract.CommandFile = linqObj.CommandFile;
			dataContract.CommandType = linqObj.CommandType;
			dataContract.StepCommand = linqObj.StepCommand;
			dataContract.SystemDefined = linqObj.SystemDefined;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			return dataContract;
		}


		public override SGW_StepType GetLinqObj(Common.DataContract.StepTypeDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_StepType());
		}

		public override SGW_StepType GetLinqObj(Common.DataContract.StepTypeDataContract dataContract, SGW_StepType linq)
		{
			if (dataContract == null)
				return null;

			linq.StepTypeId = dataContract.Id;
			linq.Name = dataContract.Description;
			linq.CommandFile = dataContract.CommandFile;
			linq.CommandType = dataContract.CommandType;
			linq.StepCommand = dataContract.StepCommand;
			linq.SystemDefined = dataContract.SystemDefined;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.StepTypeDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_StepType obj = Core.MainDataContextInstance().SGW_StepTypes.Where(o => o.StepTypeId.Equals(dataContract.Id) && !o.SystemDefined).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Status da Entidade não encontrado.");

				Core.MainDataContextInstance().SGW_StepTypes.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.StepTypeDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_StepType obj = Core.MainDataContextInstance().SGW_StepTypes.Where(o => o.StepTypeId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.StepTypeDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_StepTypes.Select(o => GetDataContract(o)).ToList();
		}

		public override StepTypeDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_StepType obj = Core.MainDataContextInstance().SGW_StepTypes.Where(o => o.StepCommand.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);			
		}

	}
}
