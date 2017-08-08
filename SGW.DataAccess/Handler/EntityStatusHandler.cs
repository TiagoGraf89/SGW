using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class EntityStatusHandler : BaseHandler<EntityStatusDataContract, SGW_EntityStatus>, IEntityStatusHandler
    {
		public override Common.OperationResult Add(Common.DataContract.EntityStatusDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_EntityStatus.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(EntityStatusDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_EntityStatus obj = Core.MainDataContextInstance().SGW_EntityStatus.Where(w => w.EntityStatusId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.EntityStatusDataContract GetDataContract(SGW_EntityStatus linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.EntityStatusDataContract dataContract = new Common.DataContract.EntityStatusDataContract();
			dataContract.Id = linqObj.EntityStatusId;
			dataContract.Description = linqObj.Name;
			dataContract.UserDefinedCode = linqObj.UserDefinedCode;
			dataContract.EntityId = linqObj.EntityId;
			return dataContract;
		}


		public override SGW_EntityStatus GetLinqObj(Common.DataContract.EntityStatusDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_EntityStatus());
		}

		public override SGW_EntityStatus GetLinqObj(Common.DataContract.EntityStatusDataContract dataContract, SGW_EntityStatus linq)
		{
			if (dataContract == null)
				return null;

			linq.Name = dataContract.Description;
			linq.EntityStatusId = dataContract.Id;
			linq.EntityId = dataContract.EntityId;
			linq.UserDefinedCode = dataContract.UserDefinedCode;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.EntityStatusDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_EntityStatus obj = Core.MainDataContextInstance().SGW_EntityStatus.Where(o => o.EntityStatusId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Status da Entidade não encontrado.");

				Core.MainDataContextInstance().SGW_EntityStatus.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.EntityStatusDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_EntityStatus obj = Core.MainDataContextInstance().SGW_EntityStatus.Where(o => o.EntityStatusId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.EntityStatusDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_EntityStatus.Select(o => GetDataContract(o)).ToList();
		}

		public IEnumerable<Common.DataContract.EntityStatusDataContract> GetAll(Guid entityId)
		{
			return Core.MainDataContextInstance().SGW_EntityStatus.Where(o => o.EntityId.Equals(entityId)).OrderBy(o => o.UserDefinedCode).Select(o => GetDataContract(o)).ToList();
		}

		public void DeleteAll(Guid entityId)
		{
			var list = Core.MainDataContextInstance().SGW_EntityStatus.Where(o => o.EntityId.Equals(entityId)).ToList();
			foreach (var obj in list)
			{
				Core.MainDataContextInstance().SGW_EntityStatus.DeleteOnSubmit(obj);
			}
			Core.MainDataContextInstance().SubmitChanges();
		}

		public override EntityStatusDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_EntityStatus obj = Core.MainDataContextInstance().SGW_EntityStatus.Where(o => o.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);			
		}

	}
}
