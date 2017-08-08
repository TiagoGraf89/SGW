using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SGW.Common.DataContract
{
	[DataContract]
	public class WorkflowStepInstanceDataContract : BaseDataContract
	{
		public Guid? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public Guid? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public Guid WorkflowInstanceId {get;set;}
		public Guid WorkflowId  {get;set;}
		public Guid StepId {get;set;}
	
		public Guid? CompletedBy {get;set;}
		public DateTime? CompletedOn {get;set;}
		public bool Completed {get;set;}

	}
}
