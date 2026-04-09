using GovServe_Project.DTOs;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
namespace GovServe_Project.Services.Service_Implementation.GrievanceAppealService_Implementation
{
	namespace GovServe_Project.Services.Service_Implementation
	{
		public class GrievanceService : IGrievanceService
		{
			private readonly IGrievanceRepository _repository;
			private readonly INotificationService _notificationService;

			// Constructor injection
			public GrievanceService(IGrievanceRepository repository, INotificationService notificationService)

			{
				_repository = repository;
				_notificationService = notificationService;

			}

			// Citizen raises grievance

			public async Task RaiseGrievanceAsync(RaiseGrievanceDTO dto)
			{
				var grievance = new Grievance
				{
					ApplicationID = dto.ApplicationID,
					UserId = dto.UserId,
					Reason = dto.Reason,
					Description = dto.Description ,
					FiledDate = DateTime.Now,
					Status = Enum.GrievanceStatus.Submitted,
				};

				await _repository.AddAsync(grievance);
				
			}


			// Get grievances for logged citizen
			public async Task<List<Grievance>> MyGrievancesAsync(int citizenId)
			{
				return await _repository.GetByCitizenAsync(citizenId);
			}

			// Get grievance details/Status by GrievanceId
			public async Task<Grievance?> GrievanceStatusAsync(int grievanceId)
			{
				return await _repository.GetByIdAsync(grievanceId);
			}

			// Officer: Get All Grievances

			public async Task<List<Grievance>> GetAllGrievancesAsync()
			{
				return await _repository.GetAllAsync();
			}

			// Officer: Resolve Grievance

			public async Task ResolveGrievanceAsync(GrievanceActionDTO dto)
			{
				var grievance = await _repository.GetByIdAsync(dto.GrievanceId);

				if (grievance == null)
					throw new Exception("Grievance not found.");

				grievance.Status = GrievanceStatus.Resolved;
				grievance.Remarks = dto.Remarks;

				await _repository.UpdateAsync(grievance);
				await _notificationService.SendNotificationAsync(grievance.UserId,
				"Your Grievance has been Resolved. Remarks: " + dto.Remarks,
				grievance.ApplicationID);
			}


			// Officer: Reject Grievance

			public async Task RejectGrievanceAsync(GrievanceActionDTO dto)
			{
				var grievance = await _repository.GetByIdAsync(dto.GrievanceId);

				if (grievance == null)
					throw new Exception("Grievance not found.");

				grievance.Status = GrievanceStatus.Rejected;
				grievance.Remarks = dto.Remarks;
				await _repository.UpdateAsync(grievance);

				await _notificationService.SendNotificationAsync(grievance.UserId,
				"Your Grievance has been Rejected. Remarks: " + dto.Remarks,
				grievance.ApplicationID);
			}


			

			public async Task<int> GetPendingGrievanceCountAsync()
			{
				return await _repository.GetPendingGrievanceCountAsync();
			}

			public async Task<int> GetResolvedGrievanceCountAsync()
			{
				return await _repository.GetResolvedGrievanceCountAsync();
			}

			
		}
	}
}
