using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.BusinessLogic.BusinessObject;
using SGW.Common.DataContract;
using SGW.Portal.Models;

namespace SGW.Portal.Controllers
{
    public class ActionController : Controller
    {
        //
        // GET: /Action/

        public ActionResult Index(Guid workflowStepInstanceId, Guid entityInstanceId, Guid stepId, string entity, string assignee, string userDefinedCode)
        {
			var wfBO = BusinessLogic.Core.GetFactory().GetInstance<IWorkflowBO>();
			var step = wfBO.GetStepById(stepId);
			step.StepType = wfBO.GetStepTypeById(step.StepTypeId);

			ViewBag.Message = string.Format("Etapa Manual ({0})", step.Description);
			ActionModel act = new ActionModel();
			act.UserDefinedId = userDefinedCode;
			act.Step = step.Description;
			act.Assignee = assignee;
			act.Entity = entity;
			act.Description = step.Comments;
			act.Results = step.ManualActionList;
			act.EntityInstanceId = entityInstanceId;
			act.Date = step.CreatedOn.Value;
			act.Workflow = wfBO.GetById(step.WorkflowId).Description;

			if (step.StepType.StepCommand.Equals("upload"))
				act.ActionType = ActionType.Upload;
			else
				act.ActionType = ActionType.Action;

			var list = wfBO.GetStepsTransitions(step.WorkflowId, step.FromStateId).Where(o => o.FromStepId.Equals(stepId)).Select(o => o.ToStep).ToList();
			foreach(var item in list)
				item.StepType = wfBO.GetStepTypeById(item.StepTypeId);
			list.Insert(0, step);
			
			act.Steps = list;

			return View(act);
        }

		public byte[] ReadToEnd(System.IO.Stream stream)
		{
			long originalPosition = 0;

			if (stream.CanSeek)
			{
				originalPosition = stream.Position;
				stream.Position = 0;
			}

			try
			{
				byte[] readBuffer = new byte[4096];

				int totalBytesRead = 0;
				int bytesRead;

				while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
				{
					totalBytesRead += bytesRead;

					if (totalBytesRead == readBuffer.Length)
					{
						int nextByte = stream.ReadByte();
						if (nextByte != -1)
						{
							byte[] temp = new byte[readBuffer.Length * 2];
							Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
							Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
							readBuffer = temp;
							totalBytesRead++;
						}
					}
				}

				byte[] buffer = readBuffer;
				if (readBuffer.Length != totalBytesRead)
				{
					buffer = new byte[totalBytesRead];
					Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
				}
				return buffer;
			}
			finally
			{
				if (stream.CanSeek)
				{
					stream.Position = originalPosition;
				}
			}
		}

		[HttpPost]
		public ActionResult Index(ActionModel model)
		{
			var wfBO = BusinessLogic.Core.GetFactory().GetInstance<IWorkflowBO>();

			if (Request.Files.Count == 0 && model.ActionType == ActionType.Upload)
			{
				ModelState.AddModelError("", "Nenhum Arquivo Anexado!");
				model.Steps = new List<WorkflowStepDataContract>();
				return View(model);
			}

			string fileName = "";
			byte[] bytesFile = null;

			if (model.ActionType == ActionType.Upload)
			{
				var file = Request.Files[0];
				if (file != null && file.ContentLength > 0)
				{
					fileName = Path.GetFileName(file.FileName);
					bytesFile = ReadToEnd(file.InputStream);
				}
			}

			wfBO.ProcessManualStep(model.WorkflowStepInstanceId, model.StepId , model.Result, bytesFile, fileName);

			return RedirectToAction("Index","Home");
		}

		[HttpGet]
		public ActionResult EntityInstance(Guid entityInstanceId)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			var entityInstance = entityBO.GetEntityInstanceById(entityInstanceId);
			var model = new EntityInstanceModel();
			model.CurrentStatus = entityInstance.CurrentStatus;
			model.Entity = entityInstance.Entity.Description;
			model.EntityInstanceId = entityInstanceId;
			model.UserDefinedId = entityInstance.UserDefinedCode;
			model.Values = entityInstance.EntityValues;

			model.AttachedFiles = entityInstance.Attachments;

			return View(model);

		}

		public FileResult DownloadAttachment(Guid attachmentId)
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			KeyValuePair<string, byte[]> file = entityBO.GetAttachment(attachmentId);
			byte[] fileBytes = file.Value;
			string fileName = file.Key;
			return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
		}

		[HttpGet]
		public ActionResult EntityInstanceList()
		{
			var entityBO = BusinessLogic.Core.GetFactory().GetInstance<IEntityBO>();
			var list = entityBO.GetAllEntityInstance().Take(20);
			return View(list.OrderByDescending(o => o.CreatedOn));
		}
    }
}
