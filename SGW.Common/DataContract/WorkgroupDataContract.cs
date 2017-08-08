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
	public class WorkgroupDataContract : BaseDataContract
	{
		public Guid? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public Guid? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public Guid ParentWorkgroupId { get; set; }

		public string Path { get; set; }

		[Required]
		public string Name
		{
			get 
			{
				return this.Description;
			}
			set
			{
				this.Description = value;
			}

		}
	}
}
