using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IEntityHandler : IBaseHandler<Common.DataContract.EntityDataContract, SGW_Entity>
	{
		KeyValuePair<string, byte[]> GetAttachment(Guid attachmentId);
		IEnumerable<KeyValuePair<Guid, string>> GetAttachments(Guid entityInstanceId);
	}
}
