using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.GrievanceAppealService_Interface;

namespace GovServe_Project.Services.Service_Implementation.GrievanceAppealService_Implementation
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
		public async Task FileAppealAsync(Appeal appeal)
		{
			// Set default values
			appeal.FiledDate = DateTime.Now;
			appeal.Status = "Submitted";

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
