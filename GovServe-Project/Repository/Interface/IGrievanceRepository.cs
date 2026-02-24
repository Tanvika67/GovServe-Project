using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;

namespace GovServe_Project.Repository.Interface
{
	// Interface for Grievance CRUD operations
	public interface IGrievanceRepository
	{
		Task<Grievance> GetByIdAsync(int id);   // Fetch grievance by ID
		Task<List<Grievance>> GetAllAsync();    // Fetch all grievances
		Task AddAsync(Grievance grievance);     // Add new grievance
		Task UpdateAsync(Grievance grievance);  // Update existing grievance
	}
}
