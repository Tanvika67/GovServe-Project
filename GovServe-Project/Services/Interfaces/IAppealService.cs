using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;

namespace GovServe_Project.Services.Interfaces
{
	// Service interface for Appeal workflow
	public interface IAppealService
	{
		Task<Appeal> FileAppealAsync(Appeal appeal);       // Citizen files appeal
		Task ApproveAppealAsync(int appealId);            // Supervisor/Admin approves appeal
		Task RejectAppealAsync(int appealId);             // Supervisor/Admin rejects appeal
	}
}