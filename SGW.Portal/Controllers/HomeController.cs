using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.BusinessLogic.BusinessObject;
using SGW.Portal.Models;

namespace SGW.Portal.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		[Authorize]
		public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated && Common.SessionData.ResourceId.Equals(Guid.Empty))
				AuthConfig.StartLoginSession(User.Identity.Name);


			if (Common.SessionData.IsAdmin)
				return RedirectToAction("Index", "Configuration");

			ViewBag.Message = "Lista de Atividades";
			var wfBO = BusinessLogic.Core.GetFactory().GetInstance<IWorkflowBO>();
			var assignmentList = wfBO.GetPendingRequestsByResourceId(Common.SessionData.ResourceId);
			var list = new List<ActionModel>();
			var model = new HomeModel();

			if (assignmentList != null)
				list = assignmentList.Select(o => new ActionModel() 
				{
					Date = o.CreatedOn.Value,
 					StepId = o.StepId,
					Assignee = o.ParticipantDescription, 
					Description = o.Step.Comments, 
					Step = o.Step.Description, 
					Workflow = wfBO.GetById(o.Step.WorkflowId).Description,
					Entity = o.EntityInstance.Entity.Description, 
					UserDefinedId = o.EntityInstance.UserDefinedCode,
					WorkflowStepInstanceId = o.WorkflowStepInstanceId,
					EntityInstanceId = o.EntityInstanceId
				}).OrderByDescending(o => o.Date).ToList();


			model.ActionRequests = list;

			return View(model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Sistema Gerenciador de Workflows";

			return View();
		}

		public ActionResult Configuration()
		{
			ViewBag.Message = "Workflows, preferências e outros";

			return View();
		}
	}
}
