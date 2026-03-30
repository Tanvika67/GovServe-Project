using GovServe_Project.DTOs;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;


namespace GovServe_Project.Services.Service_Implementation
{
	public class AppealService : IAppealService
	{
		private readonly IAppealRepository _repository;
		private readonly INotificationService _notificationService;

		// Constructor injection
		public AppealService(IAppealRepository repository, INotificationService notificationService)
		{
			_repository = repository;
			_notificationService = notificationService;
		}


		// Citizen Action - File Appeal

		public async Task FileAppealAsync(Appeal appeal)
		{
			// Set default workflow values
			appeal.FiledDate = DateTime.Now;
			appeal.Status = AppealStatus.Submitted;
		}

		// File Appeal (Citizen fills reason)
		public async Task FileAppealAsync(AppealDTO dto)
		{
			var appeal = new Appeal
			{
				ApplicationID = dto.ApplicationID,
				Reason = dto.Reason,
				Description = dto.Description,
				Remarks = "null",
				FiledDate = DateTime.Now,
				Status = AppealStatus.Submitted,
			};


			await _repository.AddAsync(appeal);

		}


		// Citizen - View Appeals
		// My Appeals
		public async Task<List<Appeal>> MyAppealsAsync(int applicationId)
		{
			return await _repository.GetByApplicationAsync(applicationId);
		}


		// Citizen - View Status Only
		public async Task<Appeal?> GetAppealStatusAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		// Officer Action - Approve Appeal

		public async Task ApproveAppealAsync(AppealActionDTO dto)
		{
			var appeal = await _repository.GetByIdAsync(dto.AppealID);

			if (appeal == null)
				throw new Exception("Appeal not found");

			appeal.Status = AppealStatus.Approved;
			appeal.Remarks = dto.Remarks;

			await _repository.UpdateAsync(appeal);

			await _notificationService.SendNotificationAsync(appeal.UserId,
				"Your appeal has been approved.",
				appeal.ApplicationID);
		}


		// Officer Action - Reject Appeal

		public async Task RejectAppealAsync(AppealActionDTO dto)
		{
			var appeal = await _repository.GetByIdAsync(dto.AppealID);

			if (appeal == null)
				throw new Exception("Appeal not found");

			appeal.Status = AppealStatus.Rejected;
			appeal.Remarks = dto.Remarks;

			await _repository.UpdateAsync(appeal);

			await _notificationService.SendNotificationAsync(appeal.UserId,
				"Your appeal has been rejected. Remarks: " + dto.Remarks,
				appeal.ApplicationID);
		}


		// Officer Dashboard - View Submitted Appeals

		public async Task<List<Appeal>> GetAllSubmittedAppealsAsync()
		{
			return await _repository.GetByStatusAsync(AppealStatus.Submitted);
		}

		async Task<int> GetPendingAppealCountAsync()
		{
			return await _repository.GetPendingAppealCountAsync();
		}

		async Task<int> GetResolvedAppealCountAsync()
		{
			return await _repository.GetResolvedAppealCountAsync();
		}

		Task<int> IAppealService.GetPendingAppealCountAsync()
		{
			return GetPendingAppealCountAsync();
		}

		Task<int> IAppealService.GetResolvedAppealCountAsync()
		{
			return GetResolvedAppealCountAsync();
		}
	}
}
