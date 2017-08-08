using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common;
using SGW.DataAccess.Handler;

namespace SGW.DataAccess
{
	public class Factory : BaseFactory
	{
		public Factory()
		{
			this.Add(typeof(IWorkflowHandler), typeof(WorkflowHandler));
			this.Add(typeof(IWorkflowStateHandler), typeof(WorkflowStateHandler));
			this.Add(typeof(IWorkflowStateTransitionHandler), typeof(WorkflowStateTransitionHandler));
			this.Add(typeof(IStepTypeHandler), typeof(StepTypeHandler));
			this.Add(typeof(IWorkflowStepHandler), typeof(WorkflowStepHandler));
			this.Add(typeof(IWorkflowStepTransitionHandler), typeof(WorkflowStepTransitionHandler));
			this.Add(typeof(IWorkflowStepManualActionHandler), typeof(WorkflowStepManualActionHandler));
			this.Add(typeof(IWorkflowStepTransitionDecisionHandler), typeof(WorkflowStepTransitionDecisionHandler));
			this.Add(typeof(IWorkflowStepDecisionConditionHandler), typeof(WorkflowStepDecisionConditionHandler));
			this.Add(typeof(IWorkflowStepInstanceHandler), typeof(WorkflowStepInstanceHandler));
			this.Add(typeof(IWorkflowAssignmentHandler), typeof(WorkflowAssignmentHandler));
			this.Add(typeof(IEntityHandler), typeof(EntityHandler));
			this.Add(typeof(IEntityInstanceHandler), typeof(EntityInstanceHandler));
			this.Add(typeof(IEntityStatusHandler), typeof(EntityStatusHandler));
			this.Add(typeof(IEntityFieldHandler), typeof(EntityFieldHandler));
			this.Add(typeof(IConditionHandler), typeof(ConditionHandler));
			this.Add(typeof(IConditionDetailHandler), typeof(ConditionDetailHandler));
			this.Add(typeof(IParticipantHandler), typeof(ParticipantHandler));
			this.Add(typeof(IResourceHandler), typeof(ResourceHandler));
			this.Add(typeof(IWorkgroupHandler), typeof(WorkgroupHandler));
			this.Add(typeof(IRoleHandler), typeof(RoleHandler));
		}
	}
}
