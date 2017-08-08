using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SGW.Common;
using SGW.Common.DataContract;
using SGW.Portal.Models;

namespace SGW.Portal.Controllers
{
    public class ConditionController : Controller
    {
        //
        // GET: /Condition/

		[HttpGet]
        public ActionResult ConditionList()
        {
			var conditionBO = BusinessLogic.Core.GetFactory().GetInstance<BusinessLogic.BusinessObject.IConditionBO>();
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<BusinessLogic.BusinessObject.IEntityBO>();

			ViewBag.ConditionList = conditionBO.GetAll().OrderBy(o => o.EntityId).Select(o => new ConditionModel()
			{ 
				Id = o.Id, 
				Name = o.Name, 
				ConditionType = o.ConditionType == "S" ? 
					"Comando SQL" : o.ConditionType == "P" ? 
						"Procedure" : "Manual", 
				EntityDesciption = entityBO.GetById(o.EntityId).Description });

            return View();
        }

		[HttpPost]
		public JsonResult GetConditions(Guid entityId)
		{
			var conditionBO = BusinessLogic.Core.GetFactory().GetInstance<BusinessLogic.BusinessObject.IConditionBO>();
			var conditionList = conditionBO.GetAll().Where(o => o.EntityId.Equals(entityId)).Select(o => new ConditionModel()
			{
				Id = o.Id,
				Name = o.Name,
				EntityId = o.EntityId,
				ConditionType = o.ConditionType == "S" ?
					"Comando SQL" : o.ConditionType == "P" ?
						"Procedure" : "Manual",
				ConditionDisplayText = conditionBO.GetDisplayText(o)
			}).ToList();

			return Json(SGW.Portal.HtmlHelperExtender.RenderPartialViewToString(this, "Partial/SelectCondition", conditionList));
		}
		[HttpGet]
		public ActionResult EditCondition(Guid conditionId, bool displayonly = true)
		{
			var conditionBO = BusinessLogic.Core.GetFactory().GetInstance<BusinessLogic.BusinessObject.IConditionBO>();
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<BusinessLogic.BusinessObject.IEntityBO>();

			ConditionModel model = new ConditionModel();

			model.EditMode = !displayonly;

			List<SelectListItem> types = new List<SelectListItem>();
			types.Add(new SelectListItem(){ Text = "Manual", Selected = true, Value = "M"});
			types.Add(new SelectListItem(){ Text = "SQL Procedure", Selected = false, Value = "P"});
			types.Add(new SelectListItem(){ Text = "Comando SQL", Selected = false, Value = "S"});

			model.ConditionTypes = types;
			model.ConditionDetails = new List<ConditionDetailModel>();
			model.Entities = entityBO.GetAll().Select(o => new SelectListItem() { Text = o.Description, Value = o.Id.ToString(), Selected = false }).ToList();
			model.Procedures = conditionBO.GetProcedures(true).Select(o => new SelectListItem() { Text = o, Selected = false, Value = o }).ToList();
	
			model.Operators = new List<SelectListItem>();
			model.Operators.Add(new SelectListItem() { Selected = true, Value = "=", Text = "=" });
			model.Operators.Add(new SelectListItem() { Selected = false, Value = "<>", Text = "<>" });
			model.Operators.Add(new SelectListItem() { Selected = false, Value = ">", Text = ">" });
			model.Operators.Add(new SelectListItem() { Selected = false, Value = ">=", Text = ">=" });
			model.Operators.Add(new SelectListItem() { Selected = false, Value = "<", Text = "<" });
			model.Operators.Add(new SelectListItem() { Selected = false, Value = "<=", Text = "<=" });
			model.Operators.Add(new SelectListItem() { Selected = false, Value = "BETWEEN", Text = "Entre" });

			model.ConditionDetails = new List<ConditionDetailModel>();
			if (conditionId.Equals(Guid.Empty))
			{
				model.Fields = new List<SelectListItem>();
				model.EditMode = true;
				ViewBag.Message = "Nova Condição.";
			}
			else
			{
				var dt = conditionBO.GetById(conditionId);
				model.Fields = entityBO.GetById(dt.EntityId).EntityFields.Select(o => new SelectListItem() { Text = o.Name, Value = o.Name, Selected = false }).ToList();
				model.ConditionType = dt.ConditionType;
				model.EntityId = dt.EntityId;
				model.Name = dt.Name;
				model.Id = dt.Id;
				model.SQLCommand = dt.SQLCommand;
				model.SQLProcedure = dt.StoredProcedure;
				model.ConditionDetails = dt.ConditionDetails.Select(o => new ConditionDetailModel() { 
					ConditionDetailId = o.Id, 
					EditMode = true, 
					Field = o.Field, 
					GroupIdentifier = o.GroupIdentifier, 
					Operator = o.Operator, 
					Value1 = o.Value1, 
					Value2 = o.Value2 
				}).ToList();
				ViewBag.Message = "Editar Condição.";
			}

			Session["ConditionDetailList"] = model.ConditionDetails;

			return View(model);
		}

		[HttpPost]
		public ActionResult EditCondition(ConditionModel model)
		{
			var conditionBO = BusinessLogic.Core.GetFactory().GetInstance<BusinessLogic.BusinessObject.IConditionBO>();

			ConditionDataContract dt = new ConditionDataContract();

			bool update;

			if (model.Id.Equals(Guid.Empty))
			{
				update = false;
				dt.Id = Guid.NewGuid();
			}
			else
			{
				update = true;
				dt = conditionBO.GetById(model.Id);
			}

			dt.Name = model.Name;
			dt.ConditionType = model.ConditionType;
			dt.EntityId = model.EntityId;

			var list = (List<ConditionDetailModel>)Session["ConditionDetailList"];
			if (model.ConditionType == "M")
			{
				dt.ConditionDetails = list.Select(o => new ConditionDetailDataContract() { Id = o.ConditionDetailId, Field = o.Field, GroupIdentifier = o.GroupIdentifier, Operator = o.Operator, Value1 = o.Value1, Value2 = o.Value2 });
				dt.SQLCommand = string.Empty;
				dt.StoredProcedure = string.Empty;
			}
			else if (model.ConditionType == "P")
			{
				dt.ConditionDetails = new List<ConditionDetailDataContract>();
				dt.SQLCommand = string.Empty;
				dt.StoredProcedure =model.SQLProcedure;
			}
			else if (model.ConditionType == "S")
			{
				dt.ConditionDetails = new List<ConditionDetailDataContract>();
				dt.SQLCommand = model.SQLCommand;
				dt.StoredProcedure = string.Empty; 
			}

			OperationResult result;
			if (update)
				result = conditionBO.Update(dt);
			else
				result = conditionBO.Add(dt);

			if (result.Status != OperationResultStatus.Succesfull)
			{
				ModelState.AddModelError("", result.Message);
				return View(model);
			}
			else
				return RedirectToAction("ConditionList", "Condition");
		}

		[HttpPost]
		public ActionResult AddConditionDetail(string group, string field, string op, string value, string value2)
		{
			var detail = new ConditionDetailModel() { ConditionDetailId = Guid.NewGuid(), EditMode = true, Field = field, GroupIdentifier = group, Operator = op, Value1 = value, Value2 = value2 };
			List<ConditionDetailModel> detailList = (List<ConditionDetailModel>)Session["ConditionDetailList"];
			detailList.Add(detail);
			Session["ConditionDetailList"] = detailList;
			return View("Partial/NewConditionDetail", detail);
		}

		[HttpPost]
		public JsonResult GetEntityFields(Guid entityId)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<BusinessLogic.BusinessObject.IEntityBO>();
			var dt = entityBO.GetById(entityId);
			StringBuilder sb = new StringBuilder();
			foreach (var item in dt.EntityFields)
			{
				sb.Append(string.Format("<option value='{0}'>{1}</option>", item.Name, item.Name));
			}
			return Json(new { list = sb.ToString() });
		}
		

		[HttpPost]
		public JsonResult RemoveConditionDetail(Guid code)
		{
			List<ConditionDetailModel> list = (List<ConditionDetailModel>)Session["ConditionDetailList"];
			foreach (var item in list)
				if (item.ConditionDetailId.Equals(code))
				{
					list.Remove(item);
					Session["ConditionDetailList"] = list;
					return Json(new { sucess = true });
				}
			return Json(new { sucess = false });
		}
	}
}
