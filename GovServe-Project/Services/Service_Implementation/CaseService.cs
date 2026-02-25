using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	public class CaseService : ICaseService
	{
		private readonly ICaseRepository _repo;
		private readonly INotificationService _notification;

		public CaseService(ICaseRepository repo, INotificationService notification)
		{
			_repo = repo;
			_notification = notification;
		}

		public async Task<List<Case>> GetAllCasesAsync()
		{
			return await _repo.GetAllAsync();
		}

		public async Task<List<Case>> GetPendingCasesAsync()
		{
			return await _repo.GetByStatusAsync("Pending");
		}

		public async Task<List<Case>> GetActiveCasesAsync()
		{
			return await _repo.GetByStatusAsync("InProgress");
		}

		public async Task<List<Case>> GetEscalatedCasesAsync()
		{
			return await _repo.GetEscalatedAsync();
		}
		public async Task<string> CreateCaseAsync(Case model)
		{
			if (model == null)
				return "Invalid case data";

			// Initial values
			model.Status = "Pending";
			model.AssignedOfficerId = 0; // not assigned yet
			model.IsEscalated = false;
			model.AssignedDate = DateTime.Now;
			model.LastUpdated = DateTime.Now;

			await _repo.CreateAsync(model);

			return "Case created successfully";
		}
		// Assign with department 
		public async Task<string> AssignCaseAsync(int caseId, int officerId, int officerDeptId)
		{
			var caseData = await _repo.GetByIdAsync(caseId);

			if (caseData == null)
				return "Case not found";

			if (caseData.DepartmentID != officerDeptId)
				return "Department mismatch - cannot assign";

			caseData.AssignedOfficerId = officerId;
			caseData.Status = "InProgress";
			caseData.AssignedDate = DateTime.Now;
			caseData.LastUpdated = DateTime.Now;

			await _repo.UpdateAsync(caseData); 
			await _notification.SendNotificationAsync(
				officerId,
				"New case assigned to you",
				caseId
			);

			return "Case assigned successfully";
		}

		public async Task<string> ReassignCaseAsync(int caseId, int newOfficerId)
		{
			var caseData = await _repo.GetByIdAsync(caseId);

			if (caseData == null)
				return "Case not found";

			caseData.AssignedOfficerId = newOfficerId;
			caseData.Status = "Reassigned";
			caseData.LastUpdated = DateTime.Now;

			await _repo.UpdateAsync(caseData);
			await _notification.SendNotificationAsync(newOfficerId,
	          "Case reassigned to you",caseId);

			return "Case reassigned successfully";
		}

		// AUTO ESCALATION (MAIN LOGIC)
		public async Task<string> AutoEscalateAsync()
		{
			var breachedCases = await _repo.GetSlaBreachedCasesAsync();

			foreach (var c in breachedCases)
			{
				c.IsEscalated = true;
				c.Status = "Escalated";
				c.LastUpdated = DateTime.Now;

				// simple auto reassignment logic
				c.AssignedOfficerId = c.AssignedOfficerId + 1;

				await _repo.UpdateAsync(c);
			}

			return "Auto escalation completed";
		}

		// COUNTS
		public async Task<int> GetPendingCountAsync()
		{
			return await _repo.CountByStatusAsync("Pending");
		}

		public async Task<int> GetActiveCountAsync()
		{
			return await _repo.CountByStatusAsync("InProgress");
		}

		public async Task<int> GetEscalatedCountAsync()
		{
			return await _repo.CountEscalatedAsync();
		}

		// DASHBOARD
		public async Task<object> GetDashboardAsync()
		{
			var pending = await GetPendingCountAsync();
			var active = await GetActiveCountAsync();
			var escalated = await GetEscalatedCountAsync();

			return new
			{
				PendingCases = pending,
				ActiveCases = active,
				EscalatedCases = escalated
			};
		}
	}
}