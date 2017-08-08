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
	public class ResourceDataContract : BaseDataContract
	{
		[Required]
		public string Password { get; set; }

		[Required]
		[StringLength(200)]
		public string Email { get; set; }

		[Required]
		public bool Active { get; set; }

		[Required]
		public Guid WorkgroupId { get; set; }

		public Guid? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public Guid? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public bool IsAdmin { get; set; }

		public IEnumerable<RoleDataContract> ResourceRoles { get; set; }

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
