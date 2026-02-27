using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models.SuperModels;
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

		public CaseService(ICaseRepository repo, INotificationService notificationService)
		{
			_repo = repo;
			_notificationService = notificationService;
		}

		public async Task<IEnumerable<Case>> GetAllCasesAsync()
		{
			return await _repo.GetAllAsync();
		}

		public async Task<IEnumerable<Case>> GetPendingCasesAsync()
		{
			return await _repo.GetByStatusAsync("Pending");
		}

		public async Task<IEnumerable<Case>> GetActiveCasesAsync()
		{
			return await _repo.GetByStatusAsync("Assigned");
		}

		public async Task<IEnumerable<Case>> GetEscalatedCasesAsync()
		{
			return await _repo.GetByStatusAsync("Escalated");
		}
		public async Task<List<Case>> GetSLABreachedCasesAsync()
		{
			return await _repo.GetSLABreachedCasesAsync();
		}

		public async Task<string> CreateCaseAsync(CreateCaseDto dto)
		{
			var model = new Case
			{
				ApplicationID = dto.ApplicationId,
				DepartmentID = dto.DepartmentId,
				SupervisorId = dto.SupervisorId,          // track supervisor
				AssignedOfficerId = dto.AssignedOfficerId, // actual officer
				Status = "Pending",
				AssignedDate = DateTime.Now,
				LastUpdated = DateTime.Now
			};

			await _repo.AddAsync(model);
			await _repo.SaveAsync();

			return "Case Created Successfully";
		}

		public async Task<string> AssignCaseAsync(int caseId, int officerId, int officerDeptId)
		{
			var c = await _repo.GetByIdAsync(caseId);

			if (c == null) return "Case Not Found";

			c.AssignedOfficerId = officerId;
			c.DepartmentID = officerDeptId;
			c.Status = "Assigned";
			c.AssignedDate = DateTime.Now;
			c.LastUpdated = DateTime.Now;

			_repo.Update(c);
			await _repo.SaveAsync();

			return "Case Assigned";
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

		public async Task<string> AutoEscalateAsync()
		{
			var cases = await _repo.GetSLABreachedCasesAsync();

			foreach (var c in cases)
			{
				if (!c.IsEscalated)
				{
					int oldOfficer = c.AssignedOfficerId;

					c.Status = "Escalated";
					c.IsEscalated = true;
					c.LastUpdated = DateTime.Now;

					_repo.Update(c);

					// Notify Supervisor
					await _notificationService.SendNotificationAsync(
						c.SupervisorId,
						"Case escalated due to SLA breach",
						c.CaseId
					);

					// Notify Citizen
					await _notificationService.SendNotificationAsync(
						c.UserId,
						"Your application was delayed and escalated",
						c.CaseId
					);

					// Notify Old Officer
					await _notificationService.SendNotificationAsync(
						oldOfficer,
						"Case escalated due to SLA breach",
						c.CaseId
					);
				}
			}

			await _repo.SaveAsync();
			return "SLA escalation completed";
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


		public async Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId)
		{
			return await _repo.GetDashboardCountsAsync(departmentId);
		}

		private readonly INotificationService notificationService;


	}
}


 