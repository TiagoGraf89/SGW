using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;
using SGW.DataAccess.Configuration;
using SGW.DataAccess.Handler;

namespace SGW.BusinessLogic.BusinessObject
{
    public class EntityBO : IEntityBO
    {

		public Common.OperationResult Add(Common.DataContract.EntityDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			dataContract.CreatedBy = Common.SessionData.ResourceId;
			dataContract.CreatedOn = DateTime.Now;
	
			var val = dataContract.Validate();
			if (!val.IsValid)
				return new Common.OperationResult(val);

			var result = handler.Add(dataContract);
			if (result.Status == Common.OperationResultStatus.Succesfull)
			{
				UpdateEntityStatus(dataContract);
				UpdateEntityFields(dataContract);

				if (dataContract.EntityType.Equals("T"))
					SetTrigger(dataContract.SQLTableName, dataContract.Id, dataContract.StatusField, dataContract.UserDefinedCodeField, dataContract.EntityFields);
			}

			return result;
		}

		public IEnumerable<EntityStatusDataContract> GetAllStatus(Guid entityId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityStatusHandler>();
			var statusList = handler.GetAll(entityId);
			return statusList;
		}

		public IEnumerable<EntityFieldDataContract> GetAllFields(Guid entityId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityFieldHandler>();
			var list = handler.GetAll(entityId);
			return list;
		}

		public void UpdateEntityStatus(EntityDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityStatusHandler>();
			var statusList = handler.GetAll(dataContract.Id);


			//add new statuses
			foreach (var item in dataContract.EntityStatus)
				if (statusList.Where(o => o.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)).Any())
					continue;
				else
				{
					item.EntityId = dataContract.Id;
					item.Id = Guid.NewGuid();
					handler.Add(item);
				}

			statusList = handler.GetAll(dataContract.Id);

			//remove deleted statuses
			foreach (var item in statusList)
				if (dataContract.EntityStatus.Where(o => o.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)).Any())
					continue;
				else
					handler.Delete(item);

			statusList = handler.GetAll(dataContract.Id);

			//remove deleted statuses
			foreach (var item in statusList)
				if (dataContract.EntityStatus.Where(o => o.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)
					&& !o.UserDefinedCode.Equals(item.UserDefinedCode, StringComparison.CurrentCultureIgnoreCase)).Any())
				{
					item.UserDefinedCode = dataContract.EntityStatus.Where(o => o.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)).First().UserDefinedCode;
					handler.Update(item);
				}
				else
					continue;
		}

		public void UpdateEntityFields(EntityDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityFieldHandler>();
			var list = handler.GetAll(dataContract.Id);


			//add new fields
			foreach (var item in dataContract.EntityFields)
				if (list.Where(o => o.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)).Any())
					continue;
				else
				{
					item.EntityId = dataContract.Id;
					item.Id = Guid.NewGuid();
					handler.Add(item);
				}

			list = handler.GetAll(dataContract.Id);

			//remove deleted fields
			foreach (var item in list)
				if (dataContract.EntityFields.Where(o => o.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)).Any())
					continue;
				else
					handler.Delete(item);

			list = handler.GetAll(dataContract.Id);

			//update fields
			foreach (var item in list)
				if (dataContract.EntityFields.Where(o => o.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)
					&& !o.FieldType.Equals(item.FieldType, StringComparison.CurrentCultureIgnoreCase)).Any())
				{
					item.FieldType = dataContract.EntityFields.Where(o => o.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)).First().FieldType;
					handler.Update(item);
				}
				else
					continue;
			
		}

		public Common.OperationResult Update(EntityDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			dataContract.UpdatedBy = Common.SessionData.ResourceId;
			dataContract.UpdatedOn = DateTime.Now;

			var val = dataContract.Validate();
			if (!val.IsValid)
				return new Common.OperationResult(val);

			var result = handler.Update(dataContract);
			if (result.Status == Common.OperationResultStatus.Succesfull)
			{
				UpdateEntityStatus(dataContract);
				UpdateEntityFields(dataContract);

				if (dataContract.EntityType.Equals("T"))
				{
					SetTrigger(dataContract.SQLTableName, dataContract.Id, dataContract.StatusField, dataContract.UserDefinedCodeField, dataContract.EntityFields);
				}
			}

			return result;
		}

		public void SetTrigger(string tableName, Guid entityId, string statusField, string userDefinedCodeField, IEnumerable<EntityFieldDataContract> fields)
		{
			bool statusUD = fields.Where(o => o.Name.Equals(statusField)).Select(o => o.UserDefined).First();
			bool fieldUD = fields.Where(o => o.Name.Equals(userDefinedCodeField)).Select(o => o.UserDefined).First();
			DatabaseHelper.CreateTrigger(tableName, entityId, !fieldUD, userDefinedCodeField, !statusUD, statusField, fields.Where(o => !o.UserDefined).Select(o=> o.Name).ToList());			
		}

		public Common.OperationResult Delete(Common.DataContract.EntityDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			var fieldsHandler = DataAccess.Core.GetFactory().GetInstance<IEntityFieldHandler>();
			var statusHandler = DataAccess.Core.GetFactory().GetInstance<IEntityStatusHandler>();

			fieldsHandler.DeleteAll(dataContract.Id);
			statusHandler.DeleteAll(dataContract.Id);


			return handler.Delete(dataContract);
		}

		public Common.DataContract.EntityDataContract GetById(Guid id)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			var statusHandler = DataAccess.Core.GetFactory().GetInstance<IEntityStatusHandler>();
			var fieldHandler = DataAccess.Core.GetFactory().GetInstance<IEntityFieldHandler>();
			var dt = handler.GetById(id);

			if (dt != null)
			{
				dt.EntityStatus = statusHandler.GetAll(dt.Id);
				dt.EntityFields = fieldHandler.GetAll(dt.Id);
			}
			return dt;
		}

		public Common.DataContract.EntityInstanceDataContract GetEntityInstanceById(Guid id)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityInstanceHandler>();
			var dt = handler.GetById(id);
			dt.Entity = this.GetById(dt.EntityId);
			dt.Attachments = this.GetAttachments(id);
			return dt;
		}

		public IEnumerable<Common.DataContract.EntityInstanceDataContract> GetAllEntityInstance()
		{
			var assignmentHandler = DataAccess.Core.GetFactory().GetInstance<IWorkflowAssignmentHandler>();
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityInstanceHandler>();
			var entityHandler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			var list = handler.GetAll();

			foreach (var item in list)
			{
				item.Entity = entityHandler.GetById(item.EntityId);
				var stepList = handler.GetCurrentStepsDescription(item.Id);
				item.CurrentSteps = "";
				item.Assignee = "";
				var assignmentList = assignmentHandler.GetByEntityInstanceId(item.Id);

				if (stepList != null)
					foreach (var s in stepList)
					{
						item.CurrentSteps += s.Value;
						var assignee = assignmentList.Where(o => o.StepId.Equals(s.Key)).FirstOrDefault();
						if (assignee != null)
							item.CurrentSteps += string.Format("({0})", assignee.ParticipantDescription);
						item.CurrentSteps += "; ";
					}

				
			}
			return list;
		}

		public IEnumerable<EntityDataContract> GetAll()
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			return handler.GetAll();
		}

		public IEnumerable<string> GetColumnValues(string fieldName, string tableName)
		{
			return DatabaseHelper.GetColumnValues(fieldName, tableName);
		}

		public IEnumerable<string> GetTables()
		{
			return DatabaseHelper.GetTables("SGW_");
		}
		public IEnumerable<KeyValuePair<string, string>> GetColumns(string tableName)
		{
			return DatabaseHelper.GetTableColumns(tableName);
		}

		public IEnumerable<KeyValuePair<Guid, string>> GetAttachments(Guid entityInstanceId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			return handler.GetAttachments(entityInstanceId);
		}

		public KeyValuePair<string, byte[]> GetAttachment(Guid attachmentId)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IEntityHandler>();
			return handler.GetAttachment(attachmentId);
		}

		public string GetColumnType(string databaseType)
		{
			
			switch (databaseType)
			{
				case "36":
					return "C";
				case "61":
				case "189":
				case "58":
				case "40":
					return "D";
				case "60": 
				case "127":
				case "56":
				case "52":
				case "59":
				case "122": 
				case "108": 
				case "106": 
				case "104": 
				case "62":
				case "48":
					return "N";
				case "35":
				case "175":
				case "231":
				case "239":
				case "167":
				case "99":
					return "T";
				default:
					return "";
			}
		}
	}
}
