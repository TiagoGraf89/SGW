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
	public class WorkflowStateDataContract : BaseDataContract
	{
		[Required]
		public Guid WorkflowId { get; set; }

		[Required]
		public Guid EntityStatusId { get; set; }

		[Required]
		public int UILeftPosition  { get; set; }
		
		[Required]
		public int UITopPosition  { get; set; }

		[Required]
		public bool InitialState { get; set; }

		public DateTime? CreatedOn  { get; set; }
		public Guid? CreatedBy  { get; set; }
		public DateTime? UpdatedOn  { get; set; }
		public Guid? UpdatedBy  { get; set; }

		public EntityStatusDataContract EntityStatus { get; set; }
	}
}
