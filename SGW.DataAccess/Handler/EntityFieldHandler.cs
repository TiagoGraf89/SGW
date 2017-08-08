using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class EntityFieldHandler : BaseHandler<EntityFieldDataContract, SGW_EntityField>, IEntityFieldHandler
    {
		public override Common.OperationResult Add(Common.DataContract.EntityFieldDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_EntityFields.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(EntityFieldDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_EntityField obj = Core.MainDataContextInstance().SGW_EntityFields.Where(w => w.EntityFieldId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.EntityFieldDataContract GetDataContract(SGW_EntityField linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.EntityFieldDataContract dataContract = new Common.DataContract.EntityFieldDataContract();
			dataContract.Id = linqObj.EntityFieldId;
			dataContract.Description = linqObj.Name;
			dataContract.FieldType = linqObj.FieldType;
			dataContract.UserDefined = linqObj.UserDefined;
			dataContract.EntityId = linqObj.EntityId;
			return dataContract;
		}


		public override SGW_EntityField GetLinqObj(Common.DataContract.EntityFieldDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_EntityField());
		}

		public override SGW_EntityField GetLinqObj(Common.DataContract.EntityFieldDataContract dataContract, SGW_EntityField linq)
		{
			if (dataContract == null)
				return null;

			linq.Name = dataContract.Description;
			linq.EntityFieldId = dataContract.Id;
			linq.EntityId = dataContract.EntityId;
			linq.UserDefined = dataContract.UserDefined;
			linq.FieldType = dataContract.FieldType;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.EntityFieldDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_EntityField obj = Core.MainDataContextInstance().SGW_EntityFields.Where(o => o.EntityFieldId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Field not found.");

				Core.MainDataContextInstance().SGW_EntityFields.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.EntityFieldDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_EntityField obj = Core.MainDataContextInstance().SGW_EntityFields.Where(o => o.EntityFieldId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.EntityFieldDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_EntityFields.Select(o => GetDataContract(o)).ToList();
		}

		public IEnumerable<Common.DataContract.EntityFieldDataContract> GetAll(Guid entityId)
		{
			return Core.MainDataContextInstance().SGW_EntityFields.Where(o => o.EntityId.Equals(entityId)).Select(o => GetDataContract(o)).ToList();
		}

		public void DeleteAll(Guid entityId)
		{
			var list = Core.MainDataContextInstance().SGW_EntityFields.Where(o => o.EntityId.Equals(entityId)).ToList();
			foreach (var obj in list)
			{
				Core.MainDataContextInstance().SGW_EntityFields.DeleteOnSubmit(obj);
			}
			Core.MainDataContextInstance().SubmitChanges();
		}

		public override EntityFieldDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_EntityField obj = Core.MainDataContextInstance().SGW_EntityFields.Where(o => o.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);			
		}

	}
}
