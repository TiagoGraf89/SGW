using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class RoleHandler : BaseHandler<RoleDataContract, SGW_Role>, IRoleHandler
    {
		public override Common.OperationResult Add(Common.DataContract.RoleDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_Roles.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(RoleDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_Role role = Core.MainDataContextInstance().SGW_Roles.Where(w => w.RoleId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, role);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.RoleDataContract GetDataContract(SGW_Role linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.RoleDataContract dataContract = new Common.DataContract.RoleDataContract();
			dataContract.Id = linqObj.RoleId;
			dataContract.Description = linqObj.Name;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			return dataContract;
		}


		public override SGW_Role GetLinqObj(Common.DataContract.RoleDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_Role());
		}

		public override SGW_Role GetLinqObj(Common.DataContract.RoleDataContract dataContract, SGW_Role linq)
		{
			if (dataContract == null)
				return null;

			linq.Name = dataContract.Description;
			linq.RoleId = dataContract.Id;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.RoleDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_Role role = Core.MainDataContextInstance().SGW_Roles.Where(r => r.RoleId.Equals(dataContract.Id)).FirstOrDefault();
				if (role == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Cargo não encontrado.");

				Core.MainDataContextInstance().SGW_Roles.DeleteOnSubmit(role);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.RoleDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_Role wf = Core.MainDataContextInstance().SGW_Roles.Where(w => w.RoleId.Equals(id)).FirstOrDefault();
			return GetDataContract(wf);
		}

		public override IEnumerable<Common.DataContract.RoleDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_Roles.Select(r => GetDataContract(r)).ToList();
		}

		public override RoleDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_Role role = Core.MainDataContextInstance().SGW_Roles.Where(w => w.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(role);			
		}

		public IEnumerable<RoleDataContract> GetByResourceId(Guid resourceId)
		{
			var roles = (from rm in Core.MainDataContextInstance().SGW_RoleMembers
						 join r in Core.MainDataContextInstance().SGW_Roles on rm.RoleId equals r.RoleId
						 where rm.ResourceId.Equals(resourceId)
						 select GetDataContract(r)).ToList();
			return roles;
		}
	}
}
