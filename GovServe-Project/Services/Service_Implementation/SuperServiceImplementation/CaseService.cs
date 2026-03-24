using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface;
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

		
		public async Task<string> UpdateCaseStatus(int caseId, string status)
		{
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

			if (c == null) return "Case Not Found";

			c.AssignedOfficerId = newOfficerId;
			c.LastUpdated = DateTime.Now;

			_repo.Update(c);
			await _repo.SaveAsync();

			await _notificationService.SendNotificationAsync(
				newOfficerId,
				"Case reassigned to you",
				caseId
			);

			return "Case Reassigned";
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

			//  Notifications
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

		//officer work

		public async Task<List<Case>> ViewAssignedCases(int officerId)
		{
			return await _repo.GetAssignedCases(officerId);
		}

		// Officer opens case → InProgress
		public async Task OpenCase(int caseId)
		{
			var caseObj = await _repo.GetCaseById(caseId);

			if (caseObj == null)
				return;

			caseObj.Status = "InProgress";
			await _repo.UpdateCase(caseObj);

			return;
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


		public async Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId)
		{
			return await _repo.GetDashboardCountsAsync(departmentId);
		}

	}
}