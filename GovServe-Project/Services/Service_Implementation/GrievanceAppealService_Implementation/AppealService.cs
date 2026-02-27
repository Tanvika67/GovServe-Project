using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.DTOs;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	// Implementation of Appeal workflow logic
	public class AppealService : IAppealService
	{
		private readonly IAppealRepository _repo;

		public AppealService(IAppealRepository repo)
		{
			_repo = repo;
		}

		// Citizen files appeal
		public async Task CreateAppealAsync(AppealCreateDTO dto)
		{
			var entity = new Appeal
			{
				UserId = dto.CitizenID,
				Reason = dto.Reason,
				Description = dto.Description,

				Status = AppealStatus.Open,
				FiledDate = DateTime.UtcNow
			};

			await _repo.AddAsync(entity);
		}

		public async Task<List<Appeal>> GetMyAppealsAsync(int citizenId)
			=> await _repo.GetByCitizenIdAsync(citizenId);

		public async Task<List<Appeal>> GetAllAppealsAsync()
			=> await _repo.GetAllAsync();

		public async Task<int> GetActiveCountAsync()
			=> await _repo.GetActiveCountAsync();

		public async Task<int> GetInactiveCountAsync()
			=> await _repo.GetInactiveCountAsync();

		// Approve appeal
		public async Task ApproveAsync(int id, string remarks)
		{
			var appeal = await _repo.GetByIdAsync(id);

			appeal.Status = AppealStatus.Approved;
			appeal.Remarks = remarks;

			await _repo.UpdateAsync(appeal);
		}

		// Reject appeal
		public async Task RejectAsync(int id, string remarks)
		{
			var appeal = await _repo.GetByIdAsync(id);

			appeal.Status = AppealStatus.Rejected;
			appeal.Remarks = remarks;

			await _repo.UpdateAsync(appeal);
		}
	}
}

