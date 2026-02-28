using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Services.Service_Implementation.SuperServiceImplementation
{
	public class EscalationService : IEscalationService
	{
		private readonly IEscalationRepository _repo;
		private readonly ICaseRepository _caseRepo;
		private readonly INotificationService _notificationService;

		public EscalationService(
			IEscalationRepository repo,
			ICaseRepository caseRepo,
			INotificationService notificationService)
		{
			_repo = repo;
			_caseRepo = caseRepo;
			_notificationService = notificationService;
		}

		public async Task<string> EscalateCaseAsync(int caseId, int newOfficerId, int supervisorId, string reason)
		{
			var c = await _caseRepo.GetByIdAsync(caseId);

			if (c == null)
				return "Case Not Found";

			//  Save escalation history
			var escalation = new Escalation
			{
				CaseId = caseId,
				SupervisorId = supervisorId,
				PreviousOfficerId = c.AssignedOfficerId,
				NewOfficerId = newOfficerId,
				Reason = reason,
				Status = "Open",
				EscalationDate = DateTime.Now,
				EscalationLevel = c.IsEscalated ? 2 : 1
			};

			await _repo.CreateAsync(escalation);

			// Update case
			c.AssignedOfficerId = newOfficerId;
			c.Status = "Escalated";
			c.IsEscalated = true;
			c.LastUpdated = DateTime.Now;

			_caseRepo.Update(c);
			await _caseRepo.SaveAsync();

			// Notify new officer
			await _notificationService.SendNotificationAsync(
				newOfficerId,
				" Case escalated and assigned to you",
				caseId
			);

			return "Case Escalated Successfully";
		}
		public async Task<string> AutoEscalateAsync()
		{
			// Step 1: Get SLA breached cases from repository
			var breachedCases = await _repo.GetSLABreachedCasesAsync();

			foreach (var sla in breachedCases)
			{
				var c = await _caseRepo.GetByIdAsync(sla.CaseId);

				if (c == null)
					continue;

				// Step 2: Avoid re-escalation
				if (c.IsEscalated)
					continue;

				int oldOfficerId = c.AssignedOfficerId;

				// Step 3: Update case
				c.Status = "Escalated";
				c.IsEscalated = true;
				c.LastUpdated = DateTime.Now;

				_caseRepo.Update(c);

				// Step 4: Notifications

				// Supervisor
				await _notificationService.SendNotificationAsync(
					c.SupervisorId,
					"Case escalated due to SLA breach",
					c.CaseId
				);

				// Citizen
				await _notificationService.SendNotificationAsync(
					c.UserId,
					"Your application is delayed and escalated to another officer",
					c.CaseId
				);

				// Old Officer
				await _notificationService.SendNotificationAsync(
					oldOfficerId,
					"Case escalated due to delay",
					c.CaseId
				);
			}

			await _caseRepo.SaveAsync();

			return "Auto escalation completed";
		}
		public async Task<string> CheckSLAAndEscalateAsync(int caseId)
		{
			var c = await _caseRepo.GetByIdAsync(caseId);

			if (c == null)
				return "Case not found";

			var sla = await _repo.GetByCaseIdAsync(caseId);

			if (sla != null && sla.Status == SLAStatus.Breached)
			{
				if (c.IsEscalated)
					return "Already escalated";

				c.Status = "Escalated";
				c.IsEscalated = true;
				c.LastUpdated = DateTime.Now;

				_caseRepo.Update(c);
				await _caseRepo.SaveAsync();

				return "Case escalated";
			}

			return "SLA within limit";
		}
		public async Task<int> GetEscalationCountAsync()
		{
			return await _repo.GetEscalationCountAsync();
		}
	}
}
 