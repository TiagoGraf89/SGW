using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SGWServices
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
	[ServiceContract]
	public interface IEntity
	{

		[OperationContract]
		bool NewManualEntityInstance(EntityDataContract entity);

		[OperationContract]
		void ProcessWorkflows();

		[OperationContract]
		void ProcessEmails();
	}


	// Use a data contract as illustrated in the sample below to add composite types to service operations.
	[DataContract]
	public class EntityDataContract
	{
		[DataMember]
		public Guid EntityId { get; set; }

		[DataMember]
		public List<EntityValueDataContract> Data { get; set; }
	}

	[DataContract]
	public class EntityValueDataContract
	{
		[DataMember]
		public string Field { get; set; }

		[DataMember]
		public object Value { get; set; }
	}
}
