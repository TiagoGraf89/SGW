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
    public class ConditionBO : IConditionBO
    {

		public Common.OperationResult Add(Common.DataContract.ConditionDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IConditionHandler>();
			dataContract.CreatedBy = Common.SessionData.ResourceId;
			dataContract.CreatedOn = DateTime.Now;
	
			var val = dataContract.Validate();
			if (!val.IsValid)
				return new Common.OperationResult(val);

			if (dataContract.ConditionDetails != null && dataContract.ConditionDetails.Count() > 0)
				dataContract.GeneratedCommand = GetCommandText(dataContract.ConditionDetails, dataContract.EntityId);

			var result = handler.Add(dataContract);
			if (result.Status == Common.OperationResultStatus.Succesfull)
				UpdateConditionDetails(dataContract);

			return result;
		}

		public Common.OperationResult Update(ConditionDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IConditionHandler>();
			dataContract.UpdatedBy = Common.SessionData.ResourceId;
			dataContract.UpdatedOn = DateTime.Now;

			var val = dataContract.Validate();
			if (!val.IsValid)
				return new Common.OperationResult(val);

			if (dataContract.ConditionDetails != null && dataContract.ConditionDetails.Count() > 0)
				dataContract.GeneratedCommand = GetCommandText(dataContract.ConditionDetails, dataContract.EntityId);

			var result = handler.Update(dataContract);

			if (result.Status == Common.OperationResultStatus.Succesfull)
				UpdateConditionDetails(dataContract);

			return result;
		}

		private void UpdateConditionDetails(ConditionDataContract dt)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IConditionDetailHandler>();
			var list = handler.GetAll(dt.Id);


			//add new fields
			foreach (ConditionDetailDataContract item in dt.ConditionDetails)
				if (list.Where(o => o.Id.Equals(item.Id)).Any())
					continue;
				else
				{
					item.ConditionId = dt.Id;
					handler.Add(item);
				}

			list = handler.GetAll(dt.Id);

			//remove deleted fields
			foreach (var item in list)
				if (dt.ConditionDetails.Where(o => o.Id.Equals(item.Id)).Any())
					continue;
				else
					handler.Delete(item);

		}
		public Common.OperationResult Delete(Common.DataContract.ConditionDataContract dataContract)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IConditionHandler>();
			return handler.Delete(dataContract);
		}

		public Common.DataContract.ConditionDataContract GetById(Guid id)
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IConditionHandler>();
			var detailHandler = DataAccess.Core.GetFactory().GetInstance<IConditionDetailHandler>();
			var dt = handler.GetById(id);

			dt.ConditionDetails = detailHandler.GetAll(id);

			return dt;
		}

		public IEnumerable<ConditionDataContract> GetAll()
		{
			var handler = DataAccess.Core.GetFactory().GetInstance<IConditionHandler>();
			var detailHandler = DataAccess.Core.GetFactory().GetInstance<IConditionDetailHandler>();
			var result = handler.GetAll();
			foreach (var item in result)
				item.ConditionDetails = detailHandler.GetAll(item.Id);

			return result;
		}

		public IEnumerable<string> GetProcedures(bool onlyWithOutParam)
		{
			return DatabaseHelper.GetProcedures("SGW_", onlyWithOutParam);
		}

		public string GetDisplayText(ConditionDataContract dt)
		{
			switch (dt.ConditionType)
			{
				case "P":
					return dt.StoredProcedure;
				case "S":
					return dt.SQLCommand;
				default:
					return GetDisplayText(dt.ConditionDetails, dt.EntityId);
			}
		}
		
		public string GetDisplayText(IEnumerable<ConditionDetailDataContract> list, Guid entityId)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			var entity = entityBO.GetById(entityId);
			var entityName = entity.EntityType == "T" ? entity.SQLTableName : "@" + entity.Name;

			StringBuilder sb = new StringBuilder();
			string lastGroup = "";
			foreach (var item in list.OrderBy(o => o.GroupIdentifier))
			{
				if (!string.IsNullOrWhiteSpace(sb.ToString()))
					if (lastGroup.Equals(item.GroupIdentifier, StringComparison.CurrentCultureIgnoreCase))
						sb.AppendLine(" AND ");
					else
						sb.AppendLine(" OR ");

				sb.Append(string.Format("{0}.",entityName));
				if (entity.EntityFields.Where(o => o.Name.Equals(item.Field, StringComparison.CurrentCultureIgnoreCase)).First().UserDefined)
					sb.Append("@");

				sb.Append(item.Field);
				sb.Append(string.Format(" {0} ", item.Operator));

				var valueFormat = "{0}";
				if (!entity.EntityFields.Where(o => o.Name.Equals(item.Field, StringComparison.CurrentCultureIgnoreCase)).First().FieldType.Equals('N'))
					valueFormat = "'{0}'";

				sb.Append(string.Format(valueFormat, item.Value1.Replace("'","''")));

				if (item.Operator.Equals("BETWEEN"))
				{
					sb.Append(" AND ");
					sb.Append(string.Format(valueFormat, item.Value2));
				}
			}

			return sb.ToString();
		}

		public string GetCommandText(IEnumerable<ConditionDetailDataContract> list, Guid entityId)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			var entity = entityBO.GetById(entityId);
			var entityName = entity.Name;

			StringBuilder sb = new StringBuilder();
			string lastGroup = "";
			foreach (var item in list.OrderBy(o => o.GroupIdentifier))
			{
				string fieldType = entity.EntityFields.Where(o => o.Name.Equals(item.Field, StringComparison.CurrentCultureIgnoreCase)).First().FieldType;

				if (!string.IsNullOrWhiteSpace(sb.ToString()))
					if (lastGroup.Equals(item.GroupIdentifier, StringComparison.CurrentCultureIgnoreCase))
						sb.AppendLine(" AND ");
					else
						sb.AppendLine(" OR ");

				sb.Append(string.Format("(FieldName = '{0}' AND ", item.Field));
				

				switch (fieldType)
				{
					case "D":
						sb.Append("DateValue");
						break;
					case "T":
						sb.Append("TextValue");
						break;
					case "N":
						sb.Append("NumberValue");
						break;
					case "C":
						sb.Append("IdValue");
						break;
				}

				sb.Append(string.Format(" {0} ", item.Operator));

				var valueFormat = "{0}";
				if (fieldType != "N")
					valueFormat = "'{0}'";


				sb.Append(string.Format(valueFormat, item.Value1.Replace("'", "''")));
				if (item.Operator.Equals("BETWEEN"))
				{
					sb.Append(" AND ");
					sb.Append(string.Format(valueFormat, item.Value2));
				}
				sb.Append(")");
			}

			return sb.ToString();
		}
	}
}
