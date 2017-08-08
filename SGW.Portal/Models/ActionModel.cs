using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.Common.DataContract;

namespace SGW.Portal.Models
{
	public class HomeModel
	{
		public List<SGW.Portal.Models.ActionModel> ActionRequests { get; set; }
	}

	public enum ActionType
	{
		Upload,
		Action
	}
	public class ActionModel
	{
		[Required]
		[Display(Name = "Entidade")]
		public string Entity { get; set; }

		public ActionType ActionType { get; set; }

		[Required]
		[Display(Name = "Workflow")]
		public string Workflow { get; set; }

		[Required]
		[Display(Name = "Identificador")]
		public string UserDefinedId { get; set; }

		[Display(Name = "Etapa")]
		public string Step { get; set; }

		[Display(Name = "Participante")]
		public string Assignee { get; set; }

		[Display(Name = "Descrição/Instruções da Etapa")]
		public string Description { get; set; }

		[Display(Name = "Concluir Etapa")]
		public string Result { get; set; }

		public DateTime Date { get; set; }

		public Guid StepId { get; set; }
		public Guid EntityInstanceId { get; set; }
		public Guid WorkflowStepInstanceId { get; set; }

		public IEnumerable<ManualActionDataContract> Results { get; set; }
		public IEnumerable<WorkflowStepDataContract> Steps { get; set; }
	}

	public class EntityInstanceModel
	{
		[Required]
		[Display(Name = "Identificador")]
		public string UserDefinedId { get; set; }

		[Required]
		public Guid EntityInstanceId { get; set; }

		[Display(Name = "Status Atual")]
		public string CurrentStatus{ get; set; }

		[Display(Name = "Entidade")]
		public string Entity { get; set; }

		public IEnumerable<EntityInstanceValueDataContract> Values { get; set; }

		public IEnumerable<KeyValuePair<Guid, string>> AttachedFiles { get; set; }

	}
}
