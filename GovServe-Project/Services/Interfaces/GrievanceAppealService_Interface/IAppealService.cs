using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
namespace GovServe_Project.Services.Interfaces.GrievanceAppealService_Interface
{
	public interface IAppealService
	{
		// Citizen files appeal
		Task FileAppealAsync(Appeal appeal);

		// Citizen views own appeals
		Task<List<Appeal>> MyAppealsAsync(int applicationId);

		// Citizen views appeal status
		Task<Appeal?> GetAppealStatusAsync(int id);
	}
}
