using GovServe_Project.Models;
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	public class SupervisorService : ISupervisorService
	{
		private readonly ISupervisorRepository _repo;

		public SupervisorService(ISupervisorRepository repo)
		{
			_repo = repo;
		}

		public List<Case> GetUnassignedCases()
		{
			return _repo.GetUnassignedCases();
		}

		public List<Case> GetSlaBreachedCases()
		{
			return _repo.GetSlaBreachedCases();
		}

		public string AssignOfficer(int caseId, int officerId)
		{
			var caseData = _repo.GetCaseById(caseId);
			if (caseData == null)
				return "Case not found";

			var officer = _repo.GetOfficerById(officerId);
			if (officer == null)
				return "Officer not found";

			// CORE LOGIC
			if (caseData.DepartmentId != officer.DepartmentId)
			{
				return "Cannot assign: Officer and Case department mismatch";
			}

			// Assign officer
			caseData.AssignedOfficerId = officerId;
			caseData.Status = "Assigned";
			caseData.IsAssigned = true;
			caseData.LastUpdated = DateTime.Now;

			_repo.UpdateCase(caseData);

			return "Officer assigned successfully";
		}
		public string ReassignOfficer(int caseId, int newOfficerId, string reason)
		{
			var caseData = _repo.GetCaseById(caseId);

			if (caseData == null)
				return "Case not found";

			var escalation = new Escalation
			{
				CaseId = caseId,
				EscalatedByUserId = caseData.SupervisorId,
				PreviousOfficerId = caseData.AssignedOfficerId,
				NewOfficerId = newOfficerId,
				Reason = reason,
				Status = "Reassigned",
				EscalationDate = DateTime.Now,
				EscalationLevel = 1
			};

			caseData.AssignedOfficerId = newOfficerId;
			caseData.IsEscalated = true;
			caseData.Status = "Reassigned";
			caseData.LastUpdated = DateTime.Now;

			_repo.CreateEscalation(escalation);
			_repo.UpdateCase(caseData);

			return "Reassigned Successfully";
		}

		public List<Escalation> GetEscalations(int caseId)
		{
			return _repo.GetEscalations(caseId);
		}

		public List<Notification> GetNotifications(int userId)
		{
			return _repo.GetNotifications(userId);
		}

		public string MarkNotificationRead(int notificationId)
		{
			_repo.MarkNotificationRead(notificationId);
			return "Marked as Read";
		}
	}
}