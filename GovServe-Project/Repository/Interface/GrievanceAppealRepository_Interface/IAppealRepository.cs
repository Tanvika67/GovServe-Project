using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;

namespace GovServe_Project.Repository.Interface
{
	public interface IAppealRepository
	{
		Task AddAsync(Appeal appeal);
		Task<Appeal> GetByIdAsync(int id);

		// Citizen dashboard
		Task<List<Appeal>> GetByCitizenIdAsync(int citizenId);

		// Admin list
		Task<List<Appeal>> GetAllAsync();

		Task<int> GetActiveCountAsync();
		Task<int> GetInactiveCountAsync();

		Task UpdateAsync(Appeal appeal);
	}
}