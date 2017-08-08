using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class EntityHandler : BaseHandler<EntityDataContract, SGW_Entity>, IEntityHandler
    {
		public override Common.OperationResult Add(Common.DataContract.EntityDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_Entities.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(EntityDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_Entity obj = Core.MainDataContextInstance().SGW_Entities.Where(w => w.EntityId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.EntityDataContract GetDataContract(SGW_Entity linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.EntityDataContract dataContract = new Common.DataContract.EntityDataContract();
			dataContract.Id = linqObj.EntityId;
			dataContract.Description = linqObj.Name;
			dataContract.EntityType = linqObj.EntityType;
			dataContract.StatusField = linqObj.StatusFieldName;
			dataContract.SQLTableName = linqObj.SQLTableName;
			dataContract.UserDefinedCodeField = linqObj.UserDefinedCodeField;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			return dataContract;
		}


		public override SGW_Entity GetLinqObj(Common.DataContract.EntityDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_Entity());
		}

		public override SGW_Entity GetLinqObj(Common.DataContract.EntityDataContract dataContract, SGW_Entity linq)
		{
			if (dataContract == null)
				return null;

			linq.Name = dataContract.Description;
			linq.EntityId = dataContract.Id;
			linq.StatusFieldName = dataContract.StatusField;
			linq.SQLTableName = dataContract.SQLTableName;
			linq.EntityType = dataContract.EntityType;
			linq.UserDefinedCodeField = dataContract.UserDefinedCodeField;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.EntityDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_Entity obj = Core.MainDataContextInstance().SGW_Entities.Where(o => o.EntityId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Entidade não encontrada.");

				Core.MainDataContextInstance().SGW_Entities.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.EntityDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_Entity obj = Core.MainDataContextInstance().SGW_Entities.Where(o => o.EntityId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.EntityDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_Entities.Select(o => GetDataContract(o)).ToList();
		}

		public override EntityDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_Entity obj = Core.MainDataContextInstance().SGW_Entities.Where(o => o.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);			
		}

		public IEnumerable<KeyValuePair<Guid, string>> GetAttachments(Guid entityInstanceId)
		{
			var list = Core.MainDataContextInstance().SGW_Attachments.Where(o => o.EntityInstanceId.Equals(entityInstanceId)).ToList();
			return list.Select(o => new KeyValuePair<Guid, string>(o.AttachmentId, o.Name)).ToList();
		}

		public KeyValuePair<string, byte[]> GetAttachment(Guid attachmentId)
		{
			var obj = Core.MainDataContextInstance().SGW_Attachments.Where(o => o.AttachmentId.Equals(attachmentId)).FirstOrDefault();
			return new KeyValuePair<string,byte[]>(obj.Name, obj.AttachedFile.ToArray());
		}
	}
}
