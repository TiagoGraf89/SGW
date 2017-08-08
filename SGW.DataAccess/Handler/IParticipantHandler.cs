using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public interface IParticipantHandler : IBaseHandler<Common.DataContract.ParticipantDataContract, SGW_WorkflowParticipant>
	{
		IEnumerable<ParticipantDataContract> GetAll(Guid resourceId);
	}
}
