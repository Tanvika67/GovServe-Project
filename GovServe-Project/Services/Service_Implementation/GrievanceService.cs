using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.Enum; // for GrievanceStatus enum
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	// Implementation of Grievance workflow logic
	public class GrievanceService : IGrievanceService
	{
		private readonly IGrievanceRepository _repo;

		public GrievanceService(IGrievanceRepository repo) => _repo = repo;

		// Citizen raises new grievance
		public async Task<Grievance> CreateGrievanceAsync(Grievance grievance)
		{
			grievance.Status = GrievanceStatus.Open;       // Default status
			grievance.FiledDate = DateTime.UtcNow;        // Set current date
			await _repo.AddAsync(grievance);
			return grievance;
		}

		// Officer/Supervisor resolves grievance
		public async Task ResolveGrievanceAsync(int grievanceId, string remarks)
		{
			var grievance = await _repo.GetByIdAsync(grievanceId);
			grievance.Status = GrievanceStatus.Resolved;
			grievance.Remarks = remarks;
			await _repo.UpdateAsync(grievance);
		}

		// Forward grievance to supervisor
		public async Task ForwardToSupervisorAsync(int grievanceId, int supervisorId)
		{
			var grievance = await _repo.GetByIdAsync(grievanceId);
			grievance.Status = GrievanceStatus.ForwardedToSupervisor;
			grievance.ForwardedDate = DateTime.UtcNow;
			await _repo.UpdateAsync(grievance);
		}

		// Supervisor rejects grievance → appeal can be filed
		public async Task RejectGrievanceAsync(int grievanceId, string remarks)
		{
			var grievance = await _repo.GetByIdAsync(grievanceId);
			grievance.Status = GrievanceStatus.Rejected;
			grievance.Remarks = remarks;
			await _repo.UpdateAsync(grievance);
		}
	}
}