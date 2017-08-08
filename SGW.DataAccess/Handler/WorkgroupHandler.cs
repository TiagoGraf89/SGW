using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkgroupHandler : BaseHandler<WorkgroupDataContract, SGW_Workgroup>, IWorkgroupHandler
    {
		public string GetWorkgroupPath(WorkgroupDataContract dataContract)
		{
			if (dataContract.ParentWorkgroupId == Guid.Empty)
				return dataContract.Id.ToString();
			else
			{
				var parent = GetById(dataContract.ParentWorkgroupId);
				return string.Format("{0}.{1}", GetWorkgroupPath(parent), dataContract.Id.ToString());
			}
		}
		public override Common.OperationResult Add(Common.DataContract.WorkgroupDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				var wg = GetLinqObj(dataContract);
				wg.WorkgroupPath = GetWorkgroupPath(dataContract);
				Core.MainDataContextInstance().SGW_Workgroups.InsertOnSubmit(wg);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.OperationResult Update(WorkgroupDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_Workgroup workgroup = Core.MainDataContextInstance().SGW_Workgroups.Where(w => w.WorkgroupId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, workgroup);
				workgroup.WorkgroupPath = GetWorkgroupPath(dataContract);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkgroupDataContract GetDataContract(SGW_Workgroup linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.WorkgroupDataContract dataContract = new Common.DataContract.WorkgroupDataContract();
			dataContract.Id = linqObj.WorkgroupId;
			dataContract.Description = linqObj.Name;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			dataContract.ParentWorkgroupId = linqObj.ParentWorkgroupId;
			dataContract.Path = linqObj.WorkgroupPath;
			return dataContract;
		}


		public override SGW_Workgroup GetLinqObj(Common.DataContract.WorkgroupDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_Workgroup());
		}
		public override SGW_Workgroup GetLinqObj(Common.DataContract.WorkgroupDataContract dataContract, SGW_Workgroup linq)
		{
			if (dataContract == null)
				return null;

			linq.Name = dataContract.Description;
			linq.WorkgroupId = dataContract.Id;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			linq.ParentWorkgroupId = dataContract.ParentWorkgroupId;
			linq.WorkgroupPath = dataContract.Path;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.WorkgroupDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_Workgroup wg = Core.MainDataContextInstance().SGW_Workgroups.Where(w => w.WorkgroupId.Equals(dataContract.Id)).FirstOrDefault();
				
				if (wg == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Grupo não encontrado.");

				Core.MainDataContextInstance().SGW_Workgroups.DeleteOnSubmit(wg);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkgroupDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_Workgroup wf = Core.MainDataContextInstance().SGW_Workgroups.Where(w => w.WorkgroupId.Equals(id)).FirstOrDefault();
			return GetDataContract(wf);
		}

		public override IEnumerable<Common.DataContract.WorkgroupDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_Workgroups.OrderBy(w => w.WorkgroupPath).ThenBy(w => w.Name).Select(w => GetDataContract(w)).ToList();
		}

		public override WorkgroupDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_Workgroup Workgroup = Core.MainDataContextInstance().SGW_Workgroups.Where(w => w.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(Workgroup);			
		}

		public WorkgroupDataContract GetByResourceId(Guid id)
		{
			SGW_Workgroup Workgroup = (from w in Core.MainDataContextInstance().SGW_Workgroups
										join wm in Core.MainDataContextInstance().SGW_WorkgroupMembers on w.WorkgroupId equals wm.WorkgroupId
										where wm.ResourceId.Equals(id)
										select w).FirstOrDefault();
			return GetDataContract(Workgroup);			
		}
	}
}
