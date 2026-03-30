using GovServe_Project.Data;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repositories.Citizen;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Services.Service_Implementation.SuperServiceImplementation
{
	public class CaseService : ICaseService
	{
		private readonly ICaseRepository _repo;
		private readonly INotificationService _notificationService;
		private readonly IUserRepository _userRepo;
		private readonly IApplicationRepository _applicationRepo;
		private readonly ICitizenDetailsRepository _citizenRepo;

		public CaseService(ICaseRepository repo,INotificationService notificationService,IUserRepository userRepo,IApplicationRepository applicationRepo,ICitizenDetailsRepository citizenRepo)
		{
			_repo = repo;
			_notificationService = notificationService;
			_userRepo = userRepo;
			_applicationRepo = applicationRepo;
			_citizenRepo = citizenRepo;
		}
		public async Task<IEnumerable<Case>> GetAllCasesAsync()
		{
			return await _repo.GetAllAsync();
		}	
		public async Task<IEnumerable<Case>> GetActiveCasesAsync()
		{
			return await _repo.GetByStatusAsync("Assigned");
		}
		public async Task<List<Case>> GetSLABreachedCasesAsync()
		{
			return await _repo.GetSLABreachedCasesAsync();
		}
		public async Task<List<OfficerStatisticsDto>> GetOfficerStatisticsAsync()
		{
			return await _repo.GetOfficerStatisticsAsync();
		}
		public async Task<DashboardStatsDto> GetDashboardStatsAsync()
		{
			return await _repo.GetDashboardStatsAsync();
		}
		public async Task<string> CreateCaseAsync(CreateCaseDto dto)
		{
			var application = await _applicationRepo
				.GetApplicationWithDocuments(dto.ApplicationId);

			if (application == null)
				return "Application not found";

			int officerId = await GetAvailableOfficer(dto.DepartmentId);

			if (officerId == 0)
				return "No officer available";

			var caseModel = new Case
			{
				ApplicationID = application.ApplicationID,
				UserId = application.UserId,
				DepartmentID = dto.DepartmentId,
				AssignedOfficerId = officerId,
				Status = "Assigned",
				AssignedDate = DateTime.Now,
				LastUpdated = DateTime.Now
			};

			await _repo.AddAsync(caseModel);
			await _repo.SaveAsync();

			// Officer notification
			await _notificationService.SendNotificationAsync(
				officerId,
				$"New case {caseModel.CaseId} assigned to you",
				caseModel.CaseId,
				"Assignment"
			);

			// Admin notification
			var adminId = await _userRepo.GetAdminIdAsync();

			await _notificationService.SendNotificationAsync(
			adminId,
			$"Case {caseModel.CaseId} assigned to Officer {officerId}",
			caseModel.CaseId,
			"Assignment"
			);
			return "Case assigned successfully";
		}
		public async Task<int> GetAvailableOfficer(int departmentId)
		{
			var officers = await _userRepo
				.GetOfficersByDepartmentAsync(departmentId);

			if (!officers.Any())
				return 0;

			int selectedOfficer = 0;
			int minCases = int.MaxValue;

			foreach (var officer in officers)
			{
				int count = await _repo
					.GetCaseCountByOfficerAsync(officer.UserId);

				if (count < minCases)
				{
					minCases = count;
					selectedOfficer = officer.UserId;
				}
			}

			return selectedOfficer;
		}
		public async Task<CaseDetailsDto> GetCaseDetails(int caseId)
		{
			var caseData = await _repo.GetCaseWithApplication(caseId);

			if (caseData == null)
				return null;

			var citizen = await _citizenRepo
				.GetByApplicationIdAsync(caseData.ApplicationID);

			return new CaseDetailsDto
			{
				CaseId = caseData.CaseId,
				ApplicationID = caseData.ApplicationID,
				Status = caseData.Status,
				AssignedOfficerId = caseData.AssignedOfficerId,
				AssignedDate = caseData.AssignedDate ?? DateTime.Now,

				FullName = citizen?.FullName,
				Gender = citizen?.Gender,
				DateOfBirth = citizen?.DateOfBirth ?? DateTime.Now,
				FatherName = citizen?.FatherName,
				MotherName = citizen?.MotherName,

				Email = citizen?.Email,
				Phone = citizen?.Phone,

				AddressLine1 = citizen?.AddressLine1,
				AddressLine2 = citizen?.AddressLine2,
				City = citizen?.City,
				State = citizen?.State,
				Pincode = citizen?.Pincode,

				AadhaarNumber = citizen?.AadhaarNumber,

				Documents = caseData.Application.CitizenDocuments
		        .Select(d => d.URI)
		        .ToList()
			};

		}

		public async Task<string> UpdateCaseStatus(int caseId, string status)
		{
			var validStatuses = new[] { "Pending", "Assigned", "Escalated", "Completed" };
			if (!validStatuses.Contains(status))
				return "Invalid status";

			var c = await _repo.GetByIdAsync(caseId);
			if (c == null)
				return "Case not found";

			c.Status = status;
			c.LastUpdated = DateTime.Now;

			if (status == "Completed")
				c.CompletedDate = DateTime.Now;

			_repo.Update(c);
			await _repo.SaveAsync();

			return "Status updated";
		}

		public async Task<string> ReassignCaseAsync(int caseId, int newOfficerId)
		{
			var c = await _repo.GetByIdAsync(caseId);
			if (c == null) return "Case not found";

			int oldOfficerId = c.AssignedOfficerId;

			c.AssignedOfficerId = newOfficerId;
			c.AssignedDate = DateTime.Now;   
			c.LastUpdated = DateTime.Now;

			_repo.Update(c);
			await _repo.SaveAsync();

			await _notificationService.SendNotificationAsync(
				newOfficerId,
				"Case reassigned to you",
				caseId
			);

			await _notificationService.SendNotificationAsync(
				oldOfficerId,
				"This case was reassigned",
				caseId
			);

			return "Case reassigned";
		}

		public async Task<string> ReassignEscalatedCaseAsync(int caseId, int newOfficerId)
		{
			var c = await _repo.GetByIdAsync(caseId);
			if (c == null)
				return "Case not found";

			if (!c.IsEscalated)
				return "Case is not escalated";

			int oldOfficerId = c.AssignedOfficerId;
			int citizenId = c.UserId;

			// Reassign
			c.AssignedOfficerId = newOfficerId;
			c.AssignedDate = DateTime.Now;
			c.LastUpdated = DateTime.Now;

			_repo.Update(c);
			await _repo.SaveAsync();

			// Notifications
			await _notificationService.SendNotificationAsync(
				newOfficerId,
				"New case assigned to you after escalation",
				caseId
			);

			await _notificationService.SendNotificationAsync(
				oldOfficerId,
				"This case was reassigned due to SLA breach",
				caseId
			);

			await _notificationService.SendNotificationAsync(
				citizenId,
				"Your case has been reassigned to another officer",
				caseId
			);

			return "Case reassigned successfully";
		}  
		public async Task<object> GetDashboardAsync()
		{
			var all = await _repo.GetAllAsync();

			return new
			{
				Total = all.Count(),
				Pending = all.Count(x => x.Status == "Pending"),
				Assigned = all.Count(x => x.Status == "Assigned"),
				Escalated = all.Count(x => x.Status == "Escalated"),
				Completed = all.Count(x => x.Status == "Completed")
			};
		}
		public Task<string> ReassignEscalatedCaseAsync()
		{
			throw new NotImplementedException();
		}

		private readonly INotificationService notificationService;


		public Task<string> ReassignCaseAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Case>> GetAssignedCasesAsync(int officerId)
		{
			var cases = await _repo.GetAssignedCasesAsync(officerId);
			return cases ?? Enumerable.Empty<Case>();
		}


		public async Task<Case?> GetCaseByIdAsync(int caseId)
		{
			return await _repo.GetCaseByIdAsync(caseId);
		}
		public async Task<string> ApproveCaseAsync(int caseId)
		{
			var caseObj = await _repo.GetCaseById(caseId);
			if (caseObj == null) return "Case not found";

			
			caseObj.Status = "Approved";
			caseObj.CompletedDate = DateTime.Now;

			if (caseObj.Application == null)
			{
				return "Application data not linked to this case";
				caseObj.Application.ApplicationStatus = "Approved";
				caseObj.Application.CompletedDate = DateTime.Now;
			}


			// --- Notification Logic Start ---
			await _notificationService.SendNotificationAsync(
		     caseObj.Application.UserId,
		     $" Your case #{caseId} has been approved.",caseId);

			await _repo.UpdateCase(caseObj);
			return "Case approved successfully";
		}

		public async Task<string> RejectCaseAsync(int caseId, string reason)
		{
			var caseObj = await _repo.GetCaseById(caseId);
			if (caseObj == null) return "Case not found";

			caseObj.Status = "Rejected";
			caseObj.RejectionReason = reason; 

			if (caseObj.Application != null)
			{
				caseObj.Application.ApplicationStatus = "Rejected";
				
			}

			await _notificationService.SendNotificationAsync(
		     caseObj.Application.UserId,
		     $"Your application was rejected. Reason: {reason}",caseId);

			await _repo.UpdateCase(caseObj);

			return "Case rejected successfully";

		}


		public async Task<IEnumerable<Case>> GetResubmittedCasesAsync(int officerId)
		{
			// Take data from Repository 
			var cases = await _repo.GetResubmittedCasesAsync(officerId);

			// Jar cases null asatil tar empty list return karane (Error prevent sathi)
			if (cases == null)
			{
				return Enumerable.Empty<Case>();
			}

			return cases;

		}
			public async Task<object> GetOfficerDashboardAsync(int officerId)
		{
			var summary = await _repo.GetOfficerDashboardAsync(officerId);
			return summary;
		}
	}

}



 