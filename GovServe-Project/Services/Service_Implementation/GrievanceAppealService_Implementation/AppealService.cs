using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	public class AppealService : IAppealService
	{
		private readonly IAppealRepository _repository;

		// Constructor injection
		public AppealService(IAppealRepository repository)
		{
			_repository = repository;
		}

		// File Appeal (Citizen fills reason)
		public async Task FileAppealAsync(AppealDTO dto)
		{
			var appeal = new Appeal
			{
				ApplicationID = dto.ApplicationID,
				Reason = dto.Reason,
				FiledDate = DateTime.Now,
				Status = "Submitted"
			};

			await _repository.AddAsync(appeal);
		}


		// My Appeals (by Application ID)
		public async Task<List<Appeal>> MyAppealsAsync(int applicationId)
		{
			return await _repository.GetByApplicationAsync(applicationId);
		}

		// Appeal Status (view only)
		public async Task<Appeal?> GetAppealStatusAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}
	}
}
