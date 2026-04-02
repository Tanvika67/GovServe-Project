<<<<<<<<< Temporary merge branch 1
﻿using GovServe_Project.Data;
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
using Microsoft.EntityFrameworkCore;
using GovServe_Project.DTOs.OfficerDTO;

namespace GovServe_Project.Services.Service_Implementation.SuperServiceImplementation
{
	public class CaseService : ICaseService
	{
		private readonly ICaseRepository _repo;
		private readonly INotificationService _notificationService;
		private readonly IUserRepository _userRepo;

		public CaseService(ICaseRepository repo, INotificationService notificationService,IUserRepository userRepo)
		{
			_repo = repo;
			_notificationService = notificationService;
			_userRepo=userRepo;
<<<<<<<<< Temporary merge branch 1
			_applicationRepo = applicationRepo;
=========
>>>>>>>>> Temporary merge branch 2
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
<<<<<<<<< Temporary merge branch 1
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

            return cases.Select(c => new CaseResponseDto
            {
                CaseId = c.CaseId,
                ApplicationNumber = $"APP-{c.ApplicationID}",

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
=========

		
>>>>>>>>> Temporary merge branch 2
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

			// For Notification
			await _notificationService.NotifyCaseAssigned(caseId, newOfficerId);

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
<<<<<<<<< Temporary merge branch 1
=========

		//officer work

		public async Task<List<Case>> ViewAssignedCases(int officerId)
		{
			return await _repo.GetAssignedCases(officerId);
		}

		// Officer opens case → InProgress
		public async Task<string> OpenCase(int caseId)
		{
			var caseObj = await _repo.GetCaseById(caseId);

			if (caseObj == null)
				return "Case not found";

			caseObj.Status = "InProgress";
			await _repo.UpdateCase(caseObj);

			return "Case marked as In Progress";
		}

		public async Task<string> ApproveCase(int caseId)
		{
			var caseObj = await _repo.GetCaseById(caseId);

			if (caseObj == null)
				return "Case not found";

			caseObj.Status = "Approved";
			caseObj.RejectionReason = null;

			await _repo.UpdateCase(caseObj);

			return "Case Approved Successfully";
		}

		public async Task<string> Reject(int caseId, string reason)
		{
			var caseObj = await _repo.GetCaseById(caseId);

			if (caseObj == null)
				return "Case not found";

			caseObj.Status = "Rejected";
			caseObj.RejectionReason = reason;

			await _repo.UpdateCase(caseObj);

			return "Case Rejected with reason: ";

			//add notification

		}


		public async Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId) => await _repo.GetDashboardCountsAsync(departmentId);

		public Task<string> ReassignCaseAsync()
		{
			throw new NotImplementedException();
		}

>>>>>>>>> Temporary merge branch 2
		public Task<string> ReassignEscalatedCaseAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Case> GetCaseDetails(int caseId)
		{
			throw new NotImplementedException();
		}

		public Task<List<OfficerStatisticsDto>> GetOfficerStatisticsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<DashboardStatsDto> GetDashboardStatsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Case>> GetAssignedCasesAsync(int officerId)
		{
			throw new NotImplementedException();
		}

		public Task<Case?> GetCaseByIdAsync(int caseId)
		{
			throw new NotImplementedException();
		}

		public Task<string> ApproveCaseAsync(int caseId)
		{
			throw new NotImplementedException();
		}

		public Task<string> RejectCaseAsync(int caseId, string reason)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Case>> GetResubmittedCasesAsync(int officerId)
		{
			throw new NotImplementedException();
		}

		public Task<object> GetOfficerDashboardAsync(int officerId)
		{
			throw new NotImplementedException();
		}

        public async Task<string> CreateCaseAsync(CreateCaseDto dto)
        {
            var application = await _applicationRepo
                             .GetApplicationWithDocuments(dto.ApplicationId);

            if (application == null)
                return "Application not found";

<<<<<<<<< Temporary merge branch 1
		public Task<string> ReassignCaseAsync()
		{
			throw new NotImplementedException();
		}

		//public async Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId)
		//{
		//	return await _repo.GetDashboardCountsAsync(departmentId);
		//}



		//New code


		public async Task<IEnumerable<Case>> GetAssignedCasesAsync(int officerId)
		{
			var cases = await _repo.GetAssignedCasesAsync(officerId);
			return cases ?? Enumerable.Empty<Case>();
		}

            // Admin notification
            var adminId = await _userRepo.GetAdminIdAsync();
            await _notificationService.SendNotificationAsync(
            adminId,
            $"Case {caseModel.CaseId} assigned to Officer {officerId}",
            caseModel.CaseId,
            "Assignment");
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



=========
	}
>>>>>>>>> Temporary merge branch 2
}



 