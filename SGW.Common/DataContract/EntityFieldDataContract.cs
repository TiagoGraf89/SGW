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
	public class EntityFieldDataContract : BaseDataContract
	{
		[Required]
		public bool UserDefined { get; set; }
		
		[Required]
		public Guid EntityId { get; set; }

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

		[Required]
		public string FieldType { get; set; }
	}
}
