using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	using GovServe_Project.DTOs.SupervisorDTO;
	using GovServe_Project.Models;
	using GovServe_Project.Repository;
	using Microsoft.CodeAnalysis.Operations;

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

		public async Task<string> CreateCaseAsync(CreateCaseDto dto)
		{
			var model = new Case
			{
				ApplicationId = dto.ApplicationId,
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
			var cases = await _repo.GetAllAsync();

			foreach (var c in cases)
			{
				if (c.AssignedDate.HasValue && c.Status != "Completed")
				{
					var days = (DateTime.Now - c.AssignedDate.Value).TotalDays;

					double warningThreshold = c.Sladays * 0.8;

					//  SLA WARNING (80%)
					if (days >= warningThreshold && !c.IsWarningSent)
					{
						await _notificationService.SendNotificationAsync(
							c.AssignedOfficerId,
							" SLA is about to breach. Please take action.",
							c.CaseId
						);

						c.IsWarningSent = true;
						_repo.Update(c);
					}

					//  SLA BREACH (100%)
					if (days > c.Sladays)
					{
						c.Status = "Escalated";
						c.IsEscalated = true;
						c.LastUpdated = DateTime.Now;

						_repo.Update(c);

						await _notificationService.SendNotificationAsync(
							c.SupervisorId,
							" Case escalated due to SLA breach",
							  c.CaseId
				);
					}
				}
			}

			await _repo.SaveAsync();
			return "SLA Check Completed";
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
	}
}

 