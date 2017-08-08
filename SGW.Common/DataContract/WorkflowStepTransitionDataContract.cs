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
	public class WorkflowStepTransitionDataContract : BaseDataContract
	{
		[Required]
		public Guid WorkflowId { get; set; }

		[Required]
		public Guid FromStepId { get; set; }

		[Required]
		public Guid ToStepId { get; set; }

		[Required]
		public Guid FromStateId { get; set; }

		public bool AutoTransition { get; set; }

		public DateTime? CreatedOn  { get; set; }
		public Guid? CreatedBy  { get; set; }

		public WorkflowStepDataContract ToStep { get; set; }
		public WorkflowStepDataContract FromStep { get; set; }

	}
}
