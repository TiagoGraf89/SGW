using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.BusinessLogic.BusinessObject
{
	public interface IConditionBO
	{
		Common.OperationResult Add(ConditionDataContract dataContract);
		Common.OperationResult Update(ConditionDataContract dataContract);
		Common.OperationResult Delete(ConditionDataContract dataContract);
		IEnumerable<ConditionDataContract> GetAll();
		ConditionDataContract GetById(Guid id);
		IEnumerable<string> GetProcedures(bool onlyWithOutParam);
		string GetDisplayText(ConditionDataContract dt);
	}
}
