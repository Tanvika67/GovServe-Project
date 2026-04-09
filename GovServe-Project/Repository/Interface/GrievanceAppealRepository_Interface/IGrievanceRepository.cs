using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;

namespace GovServe_Project.Repository.Interface
{
	public interface IGrievanceRepository
	{
		// Add new grievance
		Task AddAsync(Grievance grievance);

		// Get grievances by Citizen ID
		Task<List<Grievance>> GetByCitizenAsync(int citizenId);

		// Get grievance by ID
		Task<Grievance?> GetByIdAsync(int grievanceId);


		// Officer Methods


		// Get all grievances
		Task<List<Grievance>> GetAllAsync();

		// Update grievance 
		Task UpdateAsync(Grievance grievance);

		Task<int> GetPendingGrievanceCountAsync();

		Task<int> GetResolvedGrievanceCountAsync();


	}
}
