using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.Common.DataContract
{
	public class WorkflowDataContract : BaseDataContract
	{
		[Required]
		public bool Active { get; set; }

		[Required]
		public Guid EntityId { get; set; }

		[Required]
		public Guid StartConditionId { get; set; }

		public IEnumerable<WorkflowStateDataContract> WorkflowStateList { get; set; }
		public IEnumerable<WorkflowStateTransitionDataContract> WorkflowStateTransitionList { get; set; }

		public DateTime? CreatedOn { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public Guid? UpdatedBy { get; set; }

		public string EntityDescription { get; set; }
	}
}
