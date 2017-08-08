using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.BusinessLogic.BusinessObject
{
	public interface IWorkgroupBO
	{
		Common.OperationResult Add(WorkgroupDataContract dataContract);
		Common.OperationResult Update(WorkgroupDataContract dataContract);
		Common.OperationResult Delete(WorkgroupDataContract dataContract);
		IEnumerable<WorkgroupDataContract> GetAll();
		WorkgroupDataContract GetById(Guid id);
	}
}
