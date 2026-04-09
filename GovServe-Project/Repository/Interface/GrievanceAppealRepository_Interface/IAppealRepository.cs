using System;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
namespace GovServe_Project.Repository.Interface
{
	public interface IAppealRepository
	{
		// Add new appeal into database
		Task AddAsync(Appeal appeal);

		// Get appeals by UserId
		Task<List<Appeal>> GetByUserAsync(int userId);

		// Get appeal by ID (Status view)
		Task<Appeal?> GetByIdAsync(int id);

		// Get appeals by Status (Officer dashboard)
		Task<List<Appeal>> GetByStatusAsync(AppealStatus status);

		// Update appeal (Approve/Reject)
		Task UpdateAsync(Appeal appeal);

		Task<int> GetPendingAppealCountAsync();
		Task<int> GetResolvedAppealCountAsync();
	}
}
