using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.DataAccess.Handler
{
	public abstract class BaseHandler<TDataContract, TLinq> : IBaseHandler<TDataContract, TLinq> where TDataContract : Common.DataContract.BaseDataContract
	{
		public abstract Common.OperationResult Add(TDataContract dataContract);

		public abstract Common.OperationResult Delete(TDataContract dataContract);

		public abstract TDataContract GetDataContract(TLinq linqObj);

		public abstract TLinq GetLinqObj(TDataContract dataContract);

		public abstract TLinq GetLinqObj(TDataContract dataContract, TLinq linq);

		public abstract TDataContract GetById(Guid id);

		public abstract TDataContract GetByDescription(string description);

		public abstract IEnumerable<TDataContract> GetAll();

		public abstract Common.OperationResult Update(TDataContract dataContract);

	}
}
