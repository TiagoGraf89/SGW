using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.DataAccess.Handler
{
	public interface IBaseHandler<TDataContract, TLinq> where TDataContract : Common.DataContract.BaseDataContract
	{
		Common.OperationResult Add(TDataContract dataContract);
		Common.OperationResult Update(TDataContract dataContract);
		Common.OperationResult Delete(TDataContract dataContract);
		TDataContract GetByDescription(string description);
		TDataContract GetDataContract(TLinq linqObj);
		TLinq GetLinqObj(TDataContract dataContract);
		TLinq GetLinqObj(TDataContract dataContract, TLinq linq);
		TDataContract GetById(Guid id);
		IEnumerable<TDataContract> GetAll();
	}
}
