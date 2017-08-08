using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SGW.BusinessLogic.BusinessObject;
using SGW.Common;
using SGW.Common.DataContract;
using SGW.Portal.Models;

namespace SGW.Portal.Controllers
{
	
	[Authorize]
	public class EntityController : Controller
    {
        //
        // GET: /Entity/

        public ActionResult Index()
        {
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			ViewBag.EntityList = entityBO.GetAll();
            return View();
        }

		//
		// GET: /ResourceConfiguration/
		[HttpGet]
		public ActionResult EditEntity(Guid entityId, bool displayonly = true)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			EntityModel model = new EntityModel();

			model.EditMode = !displayonly;
			model.EntityStatus = new List<EntityStatusModel>();
			model.EntityFields = new List<EntityFieldModel>();
			
			var fieldTypes = new List<EntityFieldTypeModel>();
			fieldTypes.Add(new EntityFieldTypeModel() { Name = "Texto", Id = "T" });
			fieldTypes.Add(new EntityFieldTypeModel() { Name = "Numero", Id = "N" });
			fieldTypes.Add(new EntityFieldTypeModel() { Name = "Data/Hora", Id = "D" });
			fieldTypes.Add(new EntityFieldTypeModel() { Name = "Código", Id = "C" });
			model.EntityFieldTypes = fieldTypes;

			var entityTypes = new List<EntityTypeModel>();
			entityTypes.Add(new EntityTypeModel() { Name = "Manual", Id = "M" });
			entityTypes.Add(new EntityTypeModel() { Name = "Tabela", Id = "T" });
			model.EntityTypes = entityTypes;

			model.SQLTables = entityBO.GetTables().Select(o => new SQLTableModel() { Name = o }).ToList();

			Session["EntityStatusList"] = model.EntityStatus;
			Session["EntityFieldList"] = model.EntityFields;

			if (entityId == null || entityId == Guid.Empty)
			{
				model.EditMode = true;
				return View(model);
			}
			EntityDataContract dt = entityBO.GetById(entityId);
			model.SQLTableName = dt.SQLTableName;
			model.StatusField = dt.StatusField;
			model.Name = dt.Name;
			model.Id = dt.Id;
			model.EntityType = dt.EntityType;
			model.UserDefinedCodeField = dt.UserDefinedCodeField;
			if (dt.EntityStatus != null)
			{
				model.EntityStatus = dt.EntityStatus.Select(o => new EntityStatusModel() { Code = o.UserDefinedCode, Description = o.Description } ).ToList();
				Session["EntityStatusList"] = model.EntityStatus;
			}
			if (dt.EntityFields != null)
			{
				model.EntityFields = dt.EntityFields.Select(o => new EntityFieldModel() { Name = o.Name, Type = o.FieldType, UserDefined = o.UserDefined }).ToList();
				Session["EntityFieldList"] = model.EntityFields;
			}
			return View(model);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditEntity(EntityModel model)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();

			EntityDataContract dataContract;

			bool update = model.Id != null && model.Id != Guid.Empty;
			if (!update)
			{
				dataContract = new EntityDataContract();
				dataContract.Id = Guid.NewGuid();
			}
			else
				dataContract = entityBO.GetById(model.Id);

			dataContract.EntityStatus = ((List<EntityStatusModel>)Session["EntityStatusList"]).Select(o => new EntityStatusDataContract() { Description = o.Description, UserDefinedCode = o.Code });
			dataContract.EntityFields = ((List<EntityFieldModel>)Session["EntityFieldList"]).Select(o => new EntityFieldDataContract() { Name = o.Name, UserDefined = o.UserDefined, FieldType = o.Type });
			dataContract.SQLTableName = model.SQLTableName;
			dataContract.Name = model.Name;
			dataContract.EntityType = model.EntityType;
			dataContract.StatusField = model.StatusField;
			dataContract.UserDefinedCodeField = model.UserDefinedCodeField;

			OperationResult result;
			if (update)
				result = entityBO.Update(dataContract);
			else
				result = entityBO.Add(dataContract);

			if (result.Status != Common.OperationResultStatus.Succesfull)
			{
				ModelState.AddModelError("", result.Message);
				return View(model);
			}
			else
				return RedirectToAction("Index", "Entity");
		}

		[HttpPost]
		public ActionResult AddEntityStatus(string desc, string code)
		{
			var entityStatus = new EntityStatusModel { Description = desc, Code = code, EditMode = true};
			List<EntityStatusModel> entityStatusList = (List<EntityStatusModel>)Session["EntityStatusList"];
			foreach (var item in entityStatusList)
				if (item.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase))
				{
					throw new Exception("Código já adicionado");
				}
			entityStatusList.Add(entityStatus);
			Session["EntityStatusList"] = entityStatusList;
			return View("Partial/NewEntityStatus", entityStatus);
		}

		[HttpPost]
		public JsonResult SetEntityStatus(string statusFieldId, string tableName)
		{
			List<EntityStatusModel> entityStatusList = (List<EntityStatusModel>)Session["EntityStatusList"];
			if (entityStatusList.Count > 0)
				return Json(string.Empty, JsonRequestBehavior.AllowGet);

			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			var valuesList = entityBO.GetColumnValues(statusFieldId, tableName).Take(10);
			entityStatusList.RemoveAll(o=>true);

			StringBuilder sb = new StringBuilder();
			foreach (var item in valuesList)
			{
				var entityStatus = new EntityStatusModel() { Code = item, Description = item, EditMode = true };
				entityStatusList.Add(entityStatus);
				sb.AppendLine(HtmlHelperExtender.RenderPartialViewToString(this, "Partial/NewEntityStatus", entityStatus));
			}
			Session["EntityStatusList"] = entityStatusList;

			return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public ActionResult AddEntityField(string field, string type)
		{
			var model = new EntityFieldModel { Name = field, Type = type, UserDefined = true, EditMode = true };
			List<EntityFieldModel> entityFieldList = (List<EntityFieldModel>)Session["EntityFieldList"];
			foreach (var item in entityFieldList)
				if (item.Name.Equals(field, StringComparison.CurrentCultureIgnoreCase))
				{
					throw new Exception("Campo já adicionado");
				}
			entityFieldList.Add(model);
			Session["EntityFieldList"] = entityFieldList;
			return View("Partial/NewEntityField", model);
		}

		[HttpPost]
		public JsonResult GetEntityTableFields(string table)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			var columns = entityBO.GetColumns(table);

			List<EntityFieldModel> entityFieldList = (List<EntityFieldModel>)Session["EntityFieldList"];
			entityFieldList.RemoveAll(o => !o.UserDefined);

			StringBuilder sb = new StringBuilder();
			foreach (var item in columns)
			{
				EntityFieldModel model = new EntityFieldModel();
				model.UserDefined = false;
				model.Name = item.Key;
				model.Type = entityBO.GetColumnType(item.Value);
				
				if (model.Type == "")
					continue;

				var entity = entityFieldList.Where(o => o.Name.Equals(item.Key, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
				if (entity != null)
				{
					entity.UserDefined = false;
					entity.Name = item.Key;
				}
				else
					entityFieldList.Add(model);

				sb.Append(HtmlHelperExtender.RenderPartialViewToString(this, "Partial/NewEntityField", model));
			}

			Session["EntityFieldList"] = entityFieldList;
			return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
		}
		

		[HttpPost]
		public JsonResult RemoveEntityStatus(string code)
		{
			List<EntityStatusModel> entityStatusList = (List<EntityStatusModel>)Session["EntityStatusList"];
			foreach (var item in entityStatusList)
				if (item.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase))
				{
					entityStatusList.Remove(item);
					Session["EntityStatusList"] = entityStatusList;
					return Json(new { sucess = true });
				}
			return Json(new { sucess = false });
		}

		[HttpPost]
		public JsonResult RemoveEntityField(string field)
		{
			List<EntityFieldModel> entityFieldList = (List<EntityFieldModel>)Session["EntityFieldList"];
			foreach (var item in entityFieldList)
				if (item.Name.Equals(field, StringComparison.CurrentCultureIgnoreCase))
				{
					entityFieldList.Remove(item);
					Session["EntityFieldList"] = entityFieldList;
					return Json(new { sucess = true });
				}
			return Json(new { sucess = false });
		}

		[HttpGet]
		public ActionResult DeleteEntity(Guid entityId)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			var entity = entityBO.GetById(entityId);
			entityBO.Delete(entity);

			return RedirectToAction("Index", "Entity");
		}
}
}
