using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;

namespace GovServe_Project.Services.Interfaces
{
	public interface IAppealService
	{
		// Citizen files appeal
		Task FileAppealAsync(AppealDTO dto);

		// Citizen views own appeals
		Task<List<Appeal>> MyAppealsByUserAsync(int userId);

		// Citizen views appeal status
		Task<Appeal?> GetAppealStatusAsync(int id);

		Task ApproveAppealAsync(AppealActionDTO dto);
		// Officer rejects appeal
		Task RejectAppealAsync(AppealActionDTO dto);

		// Officer dashboard - view submitted appeals
		Task<List<Appeal>> GetAllSubmittedAppealsAsync();
		Task<int> GetPendingAppealCountAsync();

		Task<int> GetResolvedAppealCountAsync();
	}
}
