using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
namespace GovServe_Project.Services.Service_Implementation
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
				EscalatedByUserId = supervisorId,
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

		public async Task<int> GetEscalationCountAsync()
		{
			return await _repo.GetEscalationCountAsync();
		}
	}
}
 