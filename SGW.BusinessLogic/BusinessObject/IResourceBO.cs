using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.BusinessLogic.BusinessObject
{
	public interface IResourceBO
	{
		Common.OperationResult Add(ResourceDataContract dataContract);
		Common.OperationResult Update(ResourceDataContract dataContract);
		Common.OperationResult Delete(ResourceDataContract dataContract);
		ResourceDataContract GetById(Guid id);
		ResourceDataContract GetById(Guid id, bool loadRelatedEntities);
		bool Login(string username, string password);
		ResourceDataContract GetByEmail(string email);
		IEnumerable<ResourceDataContract> GetAll();
	}
}
