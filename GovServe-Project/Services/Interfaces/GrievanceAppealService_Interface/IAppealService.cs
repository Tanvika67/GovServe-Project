using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.DTOs;

namespace GovServe_Project.Services.Interfaces
{
	// Service interface for Appeal workflow
	public interface IAppealService
	{
		Task CreateAppealAsync(AppealCreateDTO dto);

		Task<List<Appeal>> GetMyAppealsAsync(int citizenId);

		Task<List<Appeal>> GetAllAppealsAsync();

		Task<int> GetActiveCountAsync();
		Task<int> GetInactiveCountAsync();

		Task ApproveAsync(int id, string remarks);
		Task RejectAsync(int id, string remarks);
	}
}