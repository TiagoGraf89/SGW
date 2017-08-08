using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGW.Common.DataContract;

namespace SGW.DataAccess.Handler
{
	public class WorkflowAssignmentHandler : BaseHandler<WorkflowAssignmentDataContract, SGW_WorkflowInstanceAssignment>, IWorkflowAssignmentHandler
    {
		public override Common.OperationResult Add(Common.DataContract.WorkflowAssignmentDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				Core.MainDataContextInstance().SGW_WorkflowInstanceAssignments.InsertOnSubmit(GetLinqObj(dataContract));
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.OperationResult Update(WorkflowAssignmentDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");

			try
			{
				SGW_WorkflowInstanceAssignment workflowState = Core.MainDataContextInstance().SGW_WorkflowInstanceAssignments.Where(w => w.WorkflowInstanceAssignmentId.Equals(dataContract.Id)).FirstOrDefault();
				this.GetLinqObj(dataContract, workflowState);
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowAssignmentDataContract GetDataContract(SGW_WorkflowInstanceAssignment linqObj)
		{
			if (linqObj == null)
				return null;

			Common.DataContract.WorkflowAssignmentDataContract dataContract = new Common.DataContract.WorkflowAssignmentDataContract();
			dataContract.Id = linqObj.WorkflowInstanceAssignmentId;
			dataContract.WorkflowStepInstanceId = linqObj.WorkflowStepInstanceId;
			dataContract.WorkflowInstanceId = linqObj.WorkflowInstanceId;
			dataContract.ParticipantId = linqObj.ParticipantId;
			dataContract.Escalated = linqObj.Mandatory;
			dataContract.ParticipantEntity = linqObj.ParticipantEntity;
			dataContract.StepId = linqObj.StepId;
			dataContract.EntityInstanceId = linqObj.EntityInstanceId;
			dataContract.CreatedBy = linqObj.CreatedBy;
			dataContract.CreatedOn = linqObj.CreatedOn;
			return dataContract;
		}


		public override SGW_WorkflowInstanceAssignment GetLinqObj(Common.DataContract.WorkflowAssignmentDataContract dataContract)
		{
			return GetLinqObj(dataContract, new SGW_WorkflowInstanceAssignment());
		}
		public override SGW_WorkflowInstanceAssignment GetLinqObj(Common.DataContract.WorkflowAssignmentDataContract dataContract, SGW_WorkflowInstanceAssignment linq)
		{
			if (dataContract == null)
				return null;

			linq.WorkflowInstanceAssignmentId = dataContract.Id;
			linq.WorkflowStepInstanceId = dataContract.WorkflowStepInstanceId;
			linq.WorkflowInstanceId = dataContract.WorkflowInstanceId;
			linq.ParticipantId = dataContract.ParticipantId;
			linq.Mandatory = dataContract.Escalated;
			linq.ParticipantEntity = dataContract.ParticipantEntity;
			linq.StepId = dataContract.StepId;
			linq.EntityInstanceId = dataContract.EntityInstanceId;
			linq.CreatedBy = dataContract.CreatedBy;
			linq.CreatedOn = dataContract.CreatedOn;

			return linq;
		}


		public override Common.OperationResult Delete(Common.DataContract.WorkflowAssignmentDataContract dataContract)
		{
			if (dataContract == null)
				throw new ArgumentException("Cannot be Null", "dataContract");
			
			try
			{
				Core.MainDataContextInstance().SGW_WorkflowInstanceAssignments.DeleteOnSubmit(
					Core.MainDataContextInstance().SGW_WorkflowInstanceAssignments.Where(o => o.WorkflowInstanceAssignmentId.Equals(dataContract.Id)).First());
				Core.MainDataContextInstance().SubmitChanges();
				return new Common.OperationResult();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override Common.DataContract.WorkflowAssignmentDataContract GetById(Guid id)
		{
			if (id == null || id == Guid.Empty)
				throw new ArgumentException("Cannot be Null", "id");

			SGW_WorkflowInstanceAssignment wf = Core.MainDataContextInstance().SGW_WorkflowInstanceAssignments.Where(w => w.WorkflowInstanceAssignmentId.Equals(id)).FirstOrDefault();

			return GetDataContract(wf);
		}

		public override IEnumerable<Common.DataContract.WorkflowAssignmentDataContract> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<WorkflowAssignmentDataContract> GetAll(Guid resourceId)
		{
			var pHandler = DataAccess.Core.GetFactory().GetInstance<IParticipantHandler>();
			var list = pHandler.GetAll(resourceId);
			List<Guid> pList = new List<Guid>();
			if (list != null)
				pList = list.Select(o=>o.Id).ToList();

			return (from a in Core.MainDataContextInstance().SGW_WorkflowInstanceAssignments
					where (a.ParticipantId.Equals(resourceId) && a.ParticipantEntity == "R") || pList.Contains(a.ParticipantId)
					select GetDataContract(a)).ToList();
		}

		public override WorkflowAssignmentDataContract GetByDescription(string description)
		{
			throw new NotImplementedException();
		}

		public WorkflowAssignmentDataContract GetByStepInstanceId(Guid stepInstanceId)
		{
			SGW_WorkflowInstanceAssignment wf = Core.MainDataContextInstance().SGW_WorkflowInstanceAssignments.Where(w => w.WorkflowStepInstanceId.Equals(stepInstanceId)).FirstOrDefault();

			return GetDataContract(wf);
		}

		public IEnumerable<WorkflowAssignmentDataContract> GetByEntityInstanceId(Guid entityInstanceId)
		{
			var h = DataAccess.Core.GetFactory().GetInstance<IParticipantHandler>();
			var list = Core.MainDataContextInstance().SGW_WorkflowInstanceAssignments
				.Where(w => w.EntityInstanceId.Equals(entityInstanceId))
				.Select(o=> GetDataContract(o)).ToList();

			if (list == null)
				return list;

			foreach (var item in list)
				item.ParticipantDescription = h.GetById(item.ParticipantId).Name;
			
			return list;
		}
	}
}
