using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.DTOs;
using GovServe_Project.Enum; // for GrievanceStatus enum
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	// Service layer handles business logic
	public class GrievanceService : IGrievanceService
	{
		private readonly IGrievanceRepository _repo;

		public GrievanceService(IGrievanceRepository repo)
		{
			_repo = repo;
		}

		// Citizen raises grievance
		public async Task CreateGrievanceAsync(GrievanceCreateDTO dto)
		{
			// DTO → Entity mapping
			var entity = new Grievance
			{
				ApplicationId = dto.ApplicationID,
				UserId = dto.CitizenID,
				Reason = dto.Reason,
				Description = dto.Description,

				// Default workflow values
				Status = GrievanceStatus.Open,
				FiledDate = DateTime.UtcNow
			};

			await _repo.AddAsync(entity);
		}

		// Citizen dashboard
		public async Task<List<Grievance>> GetMyGrievancesAsync(int citizenId)
			=> await _repo.GetByCitizenIdAsync(citizenId);

		public async Task<List<Grievance>> GetPendingGrievancesAsync()
			=> await _repo.GetPendingAsync();

		public async Task<List<Grievance>> GetForwardedGrievancesAsync()
			=> await _repo.GetForwardedAsync();

		public async Task<List<Grievance>> GetAllGrievancesAsync()
			=> await _repo.GetAllAsync();

		public async Task<int> GetActiveCountAsync()
			=> await _repo.GetActiveCountAsync();

		public async Task<int> GetInactiveCountAsync()
			=> await _repo.GetInactiveCountAsync();

		// Officer resolves grievance
		public async Task ResolveAsync(int id, string remarks)
		{
			var grievance = await _repo.GetByIdAsync(id);

			grievance.Status = GrievanceStatus.Resolved;
			grievance.Remarks = remarks;

			await _repo.UpdateAsync(grievance);
		}

		// Reject grievance
		public async Task RejectAsync(int id, string remarks)
		{
			var grievance = await _repo.GetByIdAsync(id);

			grievance.Status = GrievanceStatus.Rejected;
			grievance.Remarks = remarks;

			await _repo.UpdateAsync(grievance);
		}

		// Forward to supervisor
		public async Task ForwardAsync(int id, string remarks)
		{
			var grievance = await _repo.GetByIdAsync(id);

			grievance.Status = GrievanceStatus.ForwardedToSupervisor;
			grievance.Remarks = remarks;

			await _repo.UpdateAsync(grievance);
		}
	}
}