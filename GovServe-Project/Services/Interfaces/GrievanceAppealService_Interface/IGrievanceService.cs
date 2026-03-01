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
	}
}
