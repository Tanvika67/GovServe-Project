using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;

namespace GovServe_Project.Repository.Interface
{
	// Interface for Grievance CRUD operations
	public interface IGrievanceRepository
	{
		Task AddAsync(Grievance grievance);

		Task<Grievance> GetByIdAsync(int id);

		// Citizen dashboard
		Task<List<Grievance>> GetByCitizenIdAsync(int citizenId);

		// Officer pending list
		Task<List<Grievance>> GetPendingAsync();

		// Supervisor list
		Task<List<Grievance>> GetForwardedAsync();

		// All grievances 
		Task<List<Grievance>> GetAllAsync();

		// Dashboard counts
		Task<int> GetActiveCountAsync();
		Task<int> GetInactiveCountAsync();

		Task UpdateAsync(Grievance grievance);
	}
}
