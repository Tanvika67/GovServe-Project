using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	public class CaseService : ICaseService
	{
		private readonly ICaseRepository _repo;

		public CaseService(ICaseRepository repo)
		{
			_repo = repo;
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

		// Assign with department check
		public async Task<string> AssignCaseAsync(int caseId, int officerId, int officerDeptId)
		{
			var caseData = await _repo.GetByIdAsync(caseId);

			if (caseData == null)
				return "Case not found";

			if (caseData.DepartmentId != officerDeptId)
				return "Department mismatch - cannot assign";

			caseData.AssignedOfficerId = officerId;
			caseData.Status = "InProgress";
			caseData.AssignedDate = DateTime.Now;
			caseData.LastUpdated = DateTime.Now;

			await _repo.UpdateAsync(caseData);

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