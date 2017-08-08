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
	public class WorkflowStateTransitionDataContract : BaseDataContract
	{
		[Required]
		public Guid WorkflowId { get; set; }

		[Required]
		public Guid FromStateId { get; set; }

		[Required]
		public Guid ToStateId { get; set; }

		[Required]
		public bool AutoTransition { get; set; }

		public DateTime? CreatedOn  { get; set; }
		public Guid? CreatedBy  { get; set; }
		public DateTime? UpdatedOn  { get; set; }
		public Guid? UpdatedBy  { get; set; }

		public WorkflowStateDataContract ToState { get; set; }
		public WorkflowStateDataContract FromState { get; set; }

	}
}
