using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.Common.DataContract;

namespace SGW.Portal.Models
{

	public class WorkflowStateModel
	{
		public bool EditMode { get; set; }

		public Guid WorkflowId { get; set; }

		[Required]
		[Display(Name = "Entidade")]
		public Guid EntityId { get; set; }

		[Required]
		[Display(Name = "Ativo")]
		public bool Active { get; set; }

		[Required]
		[Display(Name = "Nome")]
		public string Name { get; set; }

		[Required]
		public Guid ConditionId { get; set; }

		[Display(Name = "Critérios de Inicialização")]
		public string TriggerDecisionText { get; set; }

		public IEnumerable<SelectListItem> EntityList { get; set; }

		public IEnumerable<ConditionModel> ConditionList { get; set; }

		public IEnumerable<WorkflowStateDataContract> WorkflowStateList { get; set; }
	}

	public class WorkflowStateDefinitionModel
	{
		public Guid StateId { get; set; }

		public Guid WorkflowId { get; set; }

		[Required]
		[Display(Name = "Descrição")]
		public string Description { get; set; }

		[Required]
		[Display(Name = "Código")]
		public Guid EntityStatusId { get; set; }

		[Required]
		[Display(Name = "Estado Inicial?")]
		public bool InitialState { get; set; }

		public int Left { get; set; }
		public int Top { get; set; }
		public string StateName { get; set; }

		public IEnumerable<SelectListItem> EntityStatusList { get; set; }
	}


	public class WorkflowStepDefinitionModel
	{
		public bool EditMode { get; set; }

		public Guid StepId { get; set; }

		public Guid StepTypeId { get; set; }

		public Guid WorkflowId { get; set; }

		[Required]
		[Display(Name = "Descrição")]
		public string Description { get; set; }

		[Required]
		public Guid EntityId { get; set; }

		[Required]
		[Display(Name = "Etapa Inicial?")]
		public bool InitialState { get; set; }

		public int Left { get; set; }
		public int Top { get; set; }
		public string StepName { get; set; }
		public string StepTypeCommand { get; set; }

		[Display(Name = "Email Para")]
		public string EmailTo { get; set; }
		[Display(Name = "Assunto")]
		public string EmailSubject { get; set; }
		[Display(Name = "Texto")]
		public string EmailBody { get; set; }
		[Display(Name = "Participante")]
		public Guid? EmailParticipantId { get; set; }

		public IEnumerable<ManualActionDataContract> ManualActionList { get; set; }


		[Display(Name = "Arquivos Permitidos")]
		public string UploadFileType { get; set; }

		[Display(Name = "Procedure SQL")]
		public string ActionSQLProcedure { get; set; }

		public List<SelectListItem> ActionSQLProcedures { get; set; }


		[Display(Name = "Comando SQL")]
		public string ActionSQLCommand { get; set; }

		[Display(Name = "Comentários/Detalhes da Etapa")]
		public string Comments { get; set; }

		[Display(Name = "Participante")]
		public Guid? ParticipantId { get; set; }


		public Guid FromStateId {get;set;}

		[Display(Name = "Iniciar Etapa Quando")]
		public string JoinDecisionId { get; set; }

		public List<SelectListItem> JoinDecisionList { get; set; }

		public IEnumerable<ParticipantDataContract> ParticipantList { get; set; }

		public IEnumerable<StepTypeDataContract> StepTypeList { get; set; }

		public IEnumerable<ConditionModel> ConditionList { get; set; }

		public IEnumerable<DecisionConditionDataContract> DecisionConditionList { get; set; }
		public IEnumerable<TransitionDecisionDataContract> TransitionDecisionList { get; set; }
		

		public IEnumerable<SelectListItem> TransitionList { get; set; }

		public IEnumerable<SelectListItem> TransitionCodeList
		{
			get
			{
				var result = new List<SelectListItem>();

				if (ManualActionList != null && ManualActionList.Count() > 0)
					result.AddRange(ManualActionList.Select(o => new SelectListItem(){ Text = o.Description, Value = o.Code }).ToArray());

				if (DecisionConditionList != null && DecisionConditionList.Count() > 0)
				{
					result.AddRange(DecisionConditionList.Select(o => new SelectListItem() { Text = o.Description + "Verdadeiro", Value = o.Code + "Verdadeiro" }).ToArray());
					result.AddRange(DecisionConditionList.Select(o => new SelectListItem() { Text = o.Description + "Falso", Value = o.Code + "Falso" }).ToArray());
				}

				return result;
			}
		}
	}

	public class WorkflowTransitionDefinitionModel
	{
		public bool EditMode { get; set; }

		public Guid EntityId { get; set; }

		public Guid WorkflowId { get; set; }

		[Display(Name = "Nome do Workflow")]
		public string WorkflowName { get; set; }

		[Display(Name = "Transição de Estados")]
		public Guid FromStateId { get; set; }

		public List<SelectListItem> StateTransitionList { get; set; }

		public List<WorkflowStepDataContract> StepList { get; set; }

		public IEnumerable<StepTypeDataContract> StepTypes { get; set; }
	}

	public class WorkflowStateTransitionDefinitionModel
	{
		public Guid Id { get; set; }

		public Guid WorkflowId { get; set; }

		[Display(Name = "Automática")]
		public bool AutoTransition { get; set; }

		[Required]
		[Display(Name = "Estado de Inicio")]
		public Guid FromState { get; set; }

		[Required]
		[Display(Name = "Estado de Transição")]
		public Guid ToState { get; set; }

		[Required]
		[Display(Name = "Nome")]
		public string Description { get; set; }

		public string FromStateDescription { get; set; }
		public string ToStateDescription { get; set; }

		public IEnumerable<WorkflowStateDataContract> StateList { get; set; }

		public IEnumerable<WorkflowStateTransitionDefinitionModel> TransitionList { get; set; }
	}

	public class WorkflowStepTransitionDefinitionModel
	{
		public Guid Id { get; set; }

		public Guid WorkflowId { get; set; }

		[Required]
		[Display(Name = "Etapa de Inicio")]
		public Guid FromStepId { get; set; }

		[Required]
		[Display(Name = "Etapa de Transição")]
		public Guid ToStepId { get; set; }

		[Required]
		[Display(Name = "Nome")]
		public string Description { get; set; }

		[Display(Name = "Etapa Inicio")]
		public string FromStepDescription { get; set; }

		[Display(Name = "Etapa Final")]
		public string ToStepDescription { get; set; }

		public IEnumerable<WorkflowStepDataContract> StepList { get; set; }

		public IEnumerable<WorkflowStepTransitionDefinitionModel> TransitionList { get; set; }
	}
}
