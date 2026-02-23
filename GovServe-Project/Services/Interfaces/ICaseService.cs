using GovServe_Project.Models;

namespace GovServe_Project.Services.Interfaces
{
		public interface ICaseService
		{
			// Get Cases
			Task<List<Case>> GetAllCasesAsync();
			Task<List<Case>> GetPendingCasesAsync();
			Task<List<Case>> GetActiveCasesAsync();
			Task<List<Case>> GetEscalatedCasesAsync();

			//  Assign / Reassign
			Task<string> AssignCaseAsync(int caseId, int officerId, int officerDeptId);
			Task<string> ReassignCaseAsync(int caseId, int newOfficerId);

			//  Auto Escalation
			Task<string> AutoEscalateAsync();

			//  Counts (Dashboard)
			Task<int> GetPendingCountAsync();
			Task<int> GetActiveCountAsync();
			Task<int> GetEscalatedCountAsync();

			// 🔹 Dashboard Summary
			Task<object> GetDashboardAsync();
		}
	
}
