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
	public class ConditionDetailDataContract : BaseDataContract
	{
		public Guid? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public Guid? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public Guid ConditionId { get; set; }
		public string GroupIdentifier { get; set; }
		public string Field { get; set; }
		public string Operator { get; set; }
		public string Value1 { get; set; }
		public string Value2 { get; set; }

	}
}
