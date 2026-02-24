using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;

namespace GovServe_Project.Services.Interfaces
{
	// Service interface for Grievance workflow
	public interface IGrievanceService
	{
		Task<Grievance> CreateGrievanceAsync(Grievance grievance);     // Citizen raises grievance
		Task ResolveGrievanceAsync(int grievanceId, string remarks);   // Officer/Supervisor resolves
		Task ForwardToSupervisorAsync(int grievanceId, int supervisorId); // Escalate to supervisor
		Task RejectGrievanceAsync(int grievanceId, string remarks);    // Supervisor rejects
	}
}
