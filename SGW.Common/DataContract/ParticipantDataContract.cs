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
	public class ParticipantDataContract : BaseDataContract
	{
		public Guid? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public Guid? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public bool	HasResources { get; set; }
	
		public bool	HasWorkgroups { get; set; }

		public bool	HasRoles { get; set; } 

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

		public IEnumerable<ParticipantDataContract> ParticipantMemberList { get; set; }
	}

	[DataContract]
	public class ParticipantMemberDataContract : BaseDataContract
	{
			public Guid ParticipantMemberId {get; set;}
			public Guid ParticipantId {get;set;}
			public Guid ResourceId {get;set;}
			public Guid WorkgroupId {get;set;}
			public Guid RoleId {get; set;}

	}
}
