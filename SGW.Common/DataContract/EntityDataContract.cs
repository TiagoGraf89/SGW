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
	public class EntityDataContract : BaseDataContract
	{
		public Guid? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public Guid? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public string EntityType { get; set; }

		public string StatusField { get; set; }

		public string UserDefinedCodeField { get; set; }

		public string SQLTableName { get; set; }

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

		public IEnumerable<EntityStatusDataContract> EntityStatus { get; set; }
		public IEnumerable<EntityFieldDataContract> EntityFields { get; set; }
	}
}
