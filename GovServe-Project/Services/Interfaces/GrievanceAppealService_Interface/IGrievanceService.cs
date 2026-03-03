using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
namespace GovServe_Project.Services.Interfaces
{
	public interface IGrievanceService
	{
		// Citizen raises grievance

	

		Task RaiseGrievanceAsync(RaiseGrievanceDTO dto);

		// Citizen views own grievances
		Task<List<Grievance>> MyGrievancesAsync(int citizenId);

		// Get grievance details
		Task<Grievance?> GrievanceStatusAsync(int id);


		// Officer Actions 


		// Get all grievances
		Task<List<Grievance>> GetAllGrievancesAsync();

		// Resolve grievance
		Task ResolveGrievanceAsync(GrievanceActionDTO dto);

		// Reject grievance
		Task RejectGrievanceAsync(GrievanceActionDTO dto);

		// Forward grievance to supervisor
		Task ForwardToSupervisorAsync(GrievanceActionDTO dto);

		Task<int> GetPendingGrievanceCountAsync();

		Task<int> GetResolvedGrievanceCountAsync();

	}
}
