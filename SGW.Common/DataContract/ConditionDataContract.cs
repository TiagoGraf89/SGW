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
	public class ConditionDataContract : BaseDataContract
	{
		public Guid? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public Guid? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		[Required]
		public Guid EntityId { get; set; }

		public string ConditionType { get; set; }

		public string SQLCommand { get; set; }

		public string GeneratedCommand { get; set; }

		public string StoredProcedure { get; set; }

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

		public IEnumerable<ConditionDetailDataContract> ConditionDetails { get; set; }
	}
}
