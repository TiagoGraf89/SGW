using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
    internal class ResourceHandler : BaseHandler<ResourceDataContract, SGW_Resource>, IResourceHandler
    {
		public override Common.OperationResult Add(Common.DataContract.ResourceDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_Resources.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.OperationResult Update(Common.DataContract.ResourceDataContract updatedDataContract)
		{
			if (updatedDataContract == null)
				throw new ArgumentException("Cannot be Null", "updatedDataContract");

			try
			{
				SGW_Resource res = Core.MainDataContextInstance().SGW_Resources.Where(r => r.ResourceId.Equals(updatedDataContract.Id)).FirstOrDefault();
				if (res == null)
					return new Common.OperationResult(Common.OperationResultStatus.UnexpectedError, string.Format("Resource {0} not found.", updatedDataContract.Id));

				this.GetLinqObj(updatedDataContract, res);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public Common.OperationResult UpdateResourceRoles(Common.DataContract.ResourceDataContract updatedDataContract)
		{
			try
			{
				var roles = Core.MainDataContextInstance().SGW_RoleMembers.Where(rm => rm.ResourceId.Equals(updatedDataContract.Id));
				Core.MainDataContextInstance().SGW_RoleMembers.DeleteAllOnSubmit(roles);
				Core.MainDataContextInstance().SGW_RoleMembers.InsertAllOnSubmit(
					updatedDataContract.ResourceRoles.Select(rr => new SGW_RoleMember()
					{
						ResourceId = updatedDataContract.Id,
						RoleId = rr.Id,
						CreatedBy = updatedDataContract.CreatedBy,
						CreatedOn = DateTime.Now
					}));

				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Common.OperationResult UpdateResourceWorkgroup(Common.DataContract.ResourceDataContract updatedDataContract)
		{
			try
			{
				var wg = Core.MainDataContextInstance().SGW_WorkgroupMembers.Where(rm => rm.ResourceId.Equals(updatedDataContract.Id));
				Core.MainDataContextInstance().SGW_WorkgroupMembers.DeleteAllOnSubmit(wg);
				Core.MainDataContextInstance().SGW_WorkgroupMembers.InsertOnSubmit(new SGW_WorkgroupMember()
					{
						ResourceId = updatedDataContract.Id,
						WorkgroupId = updatedDataContract.WorkgroupId,
						CreatedBy = updatedDataContract.CreatedBy,
						CreatedOn = DateTime.Now
					});

				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public override Common.DataContract.ResourceDataContract GetDataContract(SGW_Resource linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.ResourceDataContract dataContract = new Common.DataContract.ResourceDataContract();
			dataContract.Id = linqObj.ResourceId;
			dataContract.Name = linqObj.Name;
			dataContract.Password = linqObj.LoginPassword;
			dataContract.Email = linqObj.EmailAddress;
			dataContract.Active = linqObj.Active;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;

			dataContract.IsAdmin = dataContract.Id.Equals(Guid.Parse("6B80AFFC-3B47-4742-A788-370912EC8E0A"));

			return dataContract;
		}

		public override SGW_Resource GetLinqObj(Common.DataContract.ResourceDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_Resource());
		}
		public override SGW_Resource GetLinqObj(Common.DataContract.ResourceDataContract dataContract, SGW_Resource linq)
		{
			if (dataContract == null)
				return null;

			linq.Name = dataContract.Description;
			linq.ResourceId = dataContract.Id;
			linq.EmailAddress = dataContract.Email;
			linq.Active = dataContract.Active;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			linq.LoginPassword = dataContract.Password;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.ResourceDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				Core.MainDataContextInstance().SGW_Resources.DeleteOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ResourceDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_Resource wf = Core.MainDataContextInstance().SGW_Resources.Where(w => w.ResourceId.Equals(id)).FirstOrDefault();
			return GetDataContract(wf);
		}

		public override IEnumerable<Common.DataContract.ResourceDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_Resources.Select(o=> GetDataContract(o)).ToList();
		}

		public override ResourceDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_Resource wf = Core.MainDataContextInstance().SGW_Resources.Where(w => w.EmailAddress.Equals(description)).FirstOrDefault();
			return GetDataContract(wf);
		}

		public ResourceDataContract GetByEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
				throw new ArgumentException("Cannot be Null", "email");

			SGW_Resource wf = Core.MainDataContextInstance().SGW_Resources.Where(w => w.EmailAddress.Equals(email)).FirstOrDefault();
			return GetDataContract(wf);			
		}
	}
}
