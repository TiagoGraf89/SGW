using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.BusinessLogic.BusinessObject;
using SGW.Common;

namespace SGW.BusinessLogic
{
	public class Factory : BaseFactory
	{
		public Factory()
		{
			this.Add(typeof(IWorkflowBO), typeof(WorkflowBO));
			this.Add(typeof(IEntityBO), typeof(EntityBO));
			this.Add(typeof(IConditionBO), typeof(ConditionBO));
			this.Add(typeof(IResourceBO), typeof(ResourceBO));
			this.Add(typeof(IWorkgroupBO), typeof(WorkgroupBO));
			this.Add(typeof(IRoleBO), typeof(RoleBO));
		}
	}
}
