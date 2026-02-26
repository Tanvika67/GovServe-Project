using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.DTOs;
using GovServe_Project.Models;

namespace GovServe_Project.Services.Interfaces
{
	// Service interface for Grievance workflow
	public interface IGrievanceService
	{
		Task CreateGrievanceAsync(GrievanceCreateDTO dto);

		Task<List<Grievance>> GetMyGrievancesAsync(int citizenId);

		Task<List<Grievance>> GetPendingGrievancesAsync();

		Task<List<Grievance>> GetForwardedGrievancesAsync();

		Task<List<Grievance>> GetAllGrievancesAsync();

		Task<int> GetActiveCountAsync();
		Task<int> GetInactiveCountAsync();

		Task ResolveAsync(int id, string remarks);
		Task RejectAsync(int id, string remarks);
		Task ForwardAsync(int id, string remarks);
	}
}
