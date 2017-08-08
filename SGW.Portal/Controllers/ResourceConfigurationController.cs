using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.BusinessLogic.BusinessObject;
using SGW.Common;
using SGW.Common.DataContract;
using SGW.Portal.Models;

namespace SGW.Portal.Controllers
{

	[Authorize]
	public class ResourceConfigurationController : Controller
    {
        //
        // GET: /ResourceConfiguration/
		[HttpGet]
        public ActionResult EditResource(Guid resourceId, bool displayonly = true)
        {			
			var resourceBO = BusinessLogic.Core.GetFactory().GetInstance<IResourceBO>();
			var roleBO = BusinessLogic.Core.GetFactory().GetInstance<IRoleBO>();
			var wgBO = BusinessLogic.Core.GetFactory().GetInstance<IWorkgroupBO>();

			ResourceConfigurationModel model = new ResourceConfigurationModel();
			ResourceDataContract res;
			model.EditMode = !displayonly;

			if (Guid.Empty.Equals(resourceId))
			{
				model.EditMode = true;
				res = new ResourceDataContract();
				res.ResourceRoles = new List<RoleDataContract>();
			}
			else
				res = resourceBO.GetById(resourceId, true);

			model.Active = res.Active;
			model.Email = res.Email;
			model.Name = res.Name;
			model.Id = res.Id;
			
			var roles = roleBO.GetAll();
			model.Roles = roles.ToList();

			var wgs = wgBO.GetAll();
			model.WorkgroupList = wgs.ToList();

			model.Workgroup = res.WorkgroupId;

			foreach (var item in model.Roles)
				item.Selected = res.ResourceRoles.Any(o => o.Id.Equals(item.Id));
			return View(model);
        }

		[HttpGet]
		public ActionResult WorkgroupRoleList(WorkgroupRoleListModel model)
		{
			var roleBO = BusinessLogic.Core.GetFactory().GetInstance<IRoleBO>();
			var workgroupBO = BusinessLogic.Core.GetFactory().GetInstance<IWorkgroupBO>();
			if (model == null)
				model = new WorkgroupRoleListModel();
			else if (!string.IsNullOrEmpty(model.Message))
				ModelState.AddModelError("", model.Message);
			model.Roles = roleBO.GetAll();
			model.Workgroups = workgroupBO.GetAll();
			return View(model);
		}

		[HttpGet]
		public ActionResult ResourceList()
		{
			var resBO = BusinessLogic.Core.GetFactory().GetInstance<IResourceBO>();
			var list = resBO.GetAll();

			ViewBag.ResourceList = list;

			return View();
		}

		[HttpGet]
		public ActionResult WorkgroupRoleEdit(WorkgroupRoleOperation op, Guid entityId)
		{
			WorkgroupRoleListModel model = new WorkgroupRoleListModel();
			if (op == WorkgroupRoleOperation.Role)
			{
				var roleBO = BusinessLogic.Core.GetFactory().GetInstance<IRoleBO>();
				RoleDataContract role = roleBO.GetById(entityId);
				model.RoleId = role.Id;
				model.RoleDescription = role.Name;
			}
			else if (op == WorkgroupRoleOperation.Workgroup)
			{
				var wgBO = BusinessLogic.Core.GetFactory().GetInstance<IWorkgroupBO>();
				WorkgroupDataContract wg = wgBO.GetById(entityId);
				model.WorkgroupId = wg.Id;
				model.WorkgroupDescription = wg.Name;
				model.ParentWorkgroupId = wg.ParentWorkgroupId;
			}

			return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration", model);
		}

		[HttpGet]
		public ActionResult WorkgroupRoleDelete(WorkgroupRoleOperation op, Guid entityId)
		{
			WorkgroupRoleListModel model = new WorkgroupRoleListModel();
			OperationResult result;
			if (op == WorkgroupRoleOperation.Role)
			{
				var roleBO = BusinessLogic.Core.GetFactory().GetInstance<IRoleBO>();
				RoleDataContract role = roleBO.GetById(entityId);
				result = roleBO.Delete(role);
			}
			else 
			{
				var wgBO = BusinessLogic.Core.GetFactory().GetInstance<IWorkgroupBO>();
				WorkgroupDataContract wg = wgBO.GetById(entityId);
				result = wgBO.Delete(wg);
			}

			if (result.Status == OperationResultStatus.Succesfull)
				return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration", model);
			else 
				model.Message = result.Message;
			
			return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration", model);
		}

		[HttpPost]
		public ActionResult AddRole(WorkgroupRoleListModel model)
		{
			if (model == null)
				throw new ArgumentNullException("model");

			var roleBO = BusinessLogic.Core.GetFactory().GetInstance<IRoleBO>();
			RoleDataContract role;

			bool update = model.RoleId != null && model.RoleId != Guid.Empty;
			if (!update)
			{
				role = new RoleDataContract();
				role.Id = Guid.NewGuid();
			}
			else
				role = roleBO.GetById(model.RoleId);


			role.Name = model.RoleDescription;
			if (string.IsNullOrEmpty(role.Name))
			{
				model.Message = "Campo Descrição do Cargo é obrigatório.";
				return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration", model);
			}

			OperationResult result;
			if (update)
				result = roleBO.Update(role);
			else
				result = roleBO.Add(role);
			

			if (result.Status == OperationResultStatus.ValidationFailure)
				model.Message = result.Message;
			else if (result.Status == OperationResultStatus.UnexpectedError)
				model.Message = result.Exception.ToString();
			else
				return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration");

			return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration", model);
		}

		[HttpPost]
		public ActionResult AddWorkgroup(WorkgroupRoleListModel model)
		{
			if (model == null)
				throw new ArgumentNullException("model");

			var wgBO = BusinessLogic.Core.GetFactory().GetInstance<IWorkgroupBO>();
			WorkgroupDataContract wg;

			bool update = model.WorkgroupId != null && model.WorkgroupId != Guid.Empty;
			if (!update) 
			{
				wg = new WorkgroupDataContract();
				wg.Id = Guid.NewGuid(); 
			}
			else
				wg = wgBO.GetById(model.WorkgroupId); 

			wg.Name = model.WorkgroupDescription;
			wg.ParentWorkgroupId = model.ParentWorkgroupId;
			if (string.IsNullOrEmpty(wg.Name))
			{
				model.Message = "Campo Descrição do Grupo é obrigatório.";
				return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration", model);
			}

			OperationResult result;
			if (update)
				result = wgBO.Update(wg);
			else
				result = wgBO.Add(wg);

			if (result.Status == OperationResultStatus.ValidationFailure)
				model.Message = result.Message;
			else if (result.Status == OperationResultStatus.UnexpectedError)
				model.Message = result.Exception.ToString();
			else
				return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration");

			return RedirectToAction("WorkgroupRoleList", "ResourceConfiguration", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditResource(ResourceConfigurationModel model)
		{
			if (string.IsNullOrEmpty(model.Password) ||
				string.IsNullOrEmpty(model.ConfirmPassword) ||
				!model.Password.Equals(model.ConfirmPassword))
			{
				ModelState.AddModelError("", "Senha e/ou Confirmação Inválido(s).");
				return View(model);
			}

			var resourceBO = BusinessLogic.Core.GetFactory().GetInstance<IResourceBO>();
			var roleBO = BusinessLogic.Core.GetFactory().GetInstance<IRoleBO>();
			
			ResourceDataContract dataContract = null;

			if (!Guid.Empty.Equals(model.Id))
				dataContract = resourceBO.GetById(model.Id);
			
			if (dataContract == null)
				dataContract = new ResourceDataContract();

			dataContract.Password = model.Password;
			dataContract.Name = model.Name;
			dataContract.Email = model.Email;
			dataContract.Active = model.Active;
			dataContract.Id = model.Id;
			dataContract.ResourceRoles = model.Roles.Where(o => o.Selected);
			dataContract.WorkgroupId = model.Workgroup;

			OperationResult result;
			if (Guid.Empty.Equals(model.Id))
			{
				dataContract.Id = Guid.NewGuid();
				result = resourceBO.Add(dataContract);
			}
			else
				result = resourceBO.Update(dataContract);

			if (result.Status == Common.OperationResultStatus.ValidationFailure)
			{
				ModelState.AddModelError("", result.Message);
				return View(model);
			}
			else
				return RedirectToAction("ResourceList", "ResourceConfiguration");
		}

		public enum WorkgroupRoleOperation
		{
			Workgroup,
			Role
		}
	}
}
