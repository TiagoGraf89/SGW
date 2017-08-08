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
	public class StepTypeDataContract : BaseDataContract
	{
		[Required]
		public string Name { get; set; }
		public string CommandType { get; set; }
		public string StepCommand { get; set; }
		public string CommandFile { get; set; }
		public bool SystemDefined { get; set; }
		public DateTime? CreatedOn  { get; set; }
		public Guid? CreatedBy  { get; set; }
		public DateTime? UpdatedOn  { get; set; }
		public Guid? UpdatedBy  { get; set; }
	}
}
