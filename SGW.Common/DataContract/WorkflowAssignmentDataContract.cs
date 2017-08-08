using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SGW.Common.DataContract
{
	[DataContract]
	public class WorkflowAssignmentDataContract : BaseDataContract
	{

		[Required]
		public Guid WorkflowStepInstanceId { get; set; }

		[Required]
		public Guid WorkflowInstanceId {get; set;}
		
		[Required]
		public Guid ParticipantId {get;set;}

		[Required]
		public Guid StepId { get; set; }

		[Required]
		public Guid EntityInstanceId { get; set; }

		[Required]
		public string ParticipantEntity {get;set;}

		public string ParticipantDescription { get; set; }

		[Required]
		public bool Escalated { get; set; }

		public DateTime? CreatedOn  { get; set; }
		public Guid? CreatedBy  { get; set; }

		public StepTypeDataContract StepType { get; set; }
		public WorkflowStepDataContract Step { get; set; }
		public EntityInstanceDataContract EntityInstance { get; set; }


		public List<ManualActionDataContract> ManualActionList { get; set; }
	
	}

}
