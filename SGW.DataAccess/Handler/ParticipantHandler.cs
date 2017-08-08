using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class ParticipantHandler : BaseHandler<ParticipantDataContract, SGW_WorkflowParticipant>, IParticipantHandler
    {
		public override Common.OperationResult Add(Common.DataContract.ParticipantDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowParticipants.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		public override Common.OperationResult Update(ParticipantDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowParticipant obj = Core.MainDataContextInstance().SGW_WorkflowParticipants.Where(w => w.ParticipantId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ParticipantDataContract GetDataContract(SGW_WorkflowParticipant linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.ParticipantDataContract dataContract = new Common.DataContract.ParticipantDataContract();
			dataContract.Id = linqObj.ParticipantId;
			dataContract.Name = linqObj.Name;
			dataContract.HasResources = linqObj.HasResources;
			dataContract.HasRoles = linqObj.HasRoles;
			dataContract.HasWorkgroups = linqObj.HasWorkgroups;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			dataContract.UpdatedBy = linqObj.UpdatedBy;
			dataContract.UpdatedOn = linqObj.UpdatedOn;
			return dataContract;
		}


		public override SGW_WorkflowParticipant GetLinqObj(Common.DataContract.ParticipantDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowParticipant());
		}

		public override SGW_WorkflowParticipant GetLinqObj(Common.DataContract.ParticipantDataContract dataContract, SGW_WorkflowParticipant linq)
		{
			if (dataContract == null)
				return null;

			linq.ParticipantId = dataContract.Id;
			linq.Name = dataContract.Name;
			linq.HasResources = dataContract.HasResources;
			linq.HasRoles = dataContract.HasRoles;
			linq.HasWorkgroups = dataContract.HasWorkgroups;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;
			linq.UpdatedBy = dataContract.UpdatedBy;
			linq.UpdatedOn = dataContract.UpdatedOn;
			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.ParticipantDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				SGW_WorkflowParticipant obj = Core.MainDataContextInstance().SGW_WorkflowParticipants.Where(o => o.ParticipantId.Equals(dataContract.Id)).FirstOrDefault();
				if (obj == null)
					return new Common.OperationResult(Common.OperationResultStatus.ValidationFailure, "Status da Entidade não encontrado.");

				Core.MainDataContextInstance().SGW_WorkflowParticipants.DeleteOnSubmit(obj);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.ParticipantDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowParticipant obj = Core.MainDataContextInstance().SGW_WorkflowParticipants.Where(o => o.ParticipantId.Equals(id)).FirstOrDefault();
			return GetDataContract(obj);
		}

		public override IEnumerable<Common.DataContract.ParticipantDataContract> GetAll()
		{
			return Core.MainDataContextInstance().SGW_WorkflowParticipants.Select(o => GetDataContract(o)).ToList();
		}

		public IEnumerable<ParticipantDataContract> GetAll(Guid resourceId)
		{
			var result = (from p in Core.MainDataContextInstance().SGW_WorkflowParticipants
					join pm in Core.MainDataContextInstance().SGW_WorkflowParticipantMembers on p.ParticipantId equals pm.ParticipantId
					where pm.ResourceId.Equals(resourceId)
					select GetDataContract(p)).ToList();



			result.AddRange((from p in Core.MainDataContextInstance().SGW_WorkflowParticipants
						  join pm in Core.MainDataContextInstance().SGW_WorkflowParticipantMembers on p.ParticipantId equals pm.ParticipantId
						  join rm in Core.MainDataContextInstance().SGW_RoleMembers on pm.RoleId equals rm.RoleId
						  where rm.ResourceId.Equals(resourceId)
						  select GetDataContract(p)).ToList());


			result.AddRange((from p in Core.MainDataContextInstance().SGW_WorkflowParticipants
						  join pm in Core.MainDataContextInstance().SGW_WorkflowParticipantMembers on p.ParticipantId equals pm.ParticipantId
						  join wm in Core.MainDataContextInstance().SGW_WorkgroupMembers on pm.WorkgroupId equals wm.WorkgroupId
						  where wm.ResourceId.Equals(resourceId)
						  select GetDataContract(p)).ToList());


			return result.ToList();
		}

		public override ParticipantDataContract GetByDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
				throw new ArgumentException("Cannot be Null", "description");

			SGW_WorkflowParticipant obj = Core.MainDataContextInstance().SGW_WorkflowParticipants.Where(o => o.Name.Equals(description)).FirstOrDefault();
			return GetDataContract(obj);			
		}

	}
}
