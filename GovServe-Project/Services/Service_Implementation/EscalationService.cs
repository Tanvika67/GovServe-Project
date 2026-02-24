using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
namespace GovServe_Project.Services.Service_Implementation
{
	public class EscalationService : IEscalationService
	{
		private readonly ICaseRepository _caseRepo;
		private readonly IEscalationRepository _escRepo;
		private readonly INotificationService _notification;

		public EscalationService(
			ICaseRepository caseRepo,
			IEscalationRepository escRepo,
			INotificationService notification)
		{
			_caseRepo = caseRepo;
			_escRepo = escRepo;
			_notification = notification;
		}

		public async Task<string> EscalateCaseAsync(int caseId, int newOfficerId, int supervisorId, string reason)
		{
			var caseData = await _caseRepo.GetByIdAsync(caseId);

			if (caseData == null)
				return "Case not found";

			var escalation = new Escalation
			{
				CaseId = caseId,
				EscalatedByUserId = supervisorId,
				PreviousOfficerId = caseData.AssignedOfficerId,
				NewOfficerId = newOfficerId,
				Reason = reason,
				Status = "Escalated",
				EscalationDate = DateTime.Now
			};

			await _escRepo.CreateAsync(escalation);

			// update case
			caseData.AssignedOfficerId = newOfficerId;
			caseData.Status = "Escalated";
			caseData.IsEscalated = true;
			caseData.LastUpdated = DateTime.Now;

			await _caseRepo.UpdateAsync(caseData);

			// notify officer
			await _notification.SendNotificationAsync(
				newOfficerId,
				"Case escalated and assigned to you",
				caseId
			);

			return "Case escalated successfully";
		}

		public async Task<int> GetEscalationCountAsync()
		{
			return await _escRepo.GetEscalationCountAsync();
		}
	}
}