using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;

namespace GovServe_Project.Repository.Interface
{
	// Interface for Appeal CRUD operations
	public interface IAppealRepository
	{
		Task<Appeal> GetByIdAsync(int id);      // Fetch appeal by ID
		Task<List<Appeal>> GetAllAsync();       // Fetch all appeals
		Task AddAsync(Appeal appeal);           // Add new appeal
		Task UpdateAsync(Appeal appeal);        // Update appeal status or remarks
	}
}