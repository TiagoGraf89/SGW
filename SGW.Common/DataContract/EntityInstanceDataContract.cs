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
	public class EntityInstanceDataContract : BaseDataContract
	{

		public Guid? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public Guid? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public string UserDefinedCode  { get; set; }

		public string CurrentStatus { get; set; }

		public string CurrentSteps { get; set; }
		public string Assignee { get; set; }

		public Guid EntityId { get; set; }

		public IEnumerable<EntityInstanceValueDataContract> EntityValues { get; set; }
		public EntityDataContract Entity { get; set; }
		public IEnumerable<KeyValuePair<Guid, string>> Attachments { get; set; }
	}
}
