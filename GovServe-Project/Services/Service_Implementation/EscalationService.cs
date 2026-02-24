using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
namespace GovServe_Project.Services.Service_Implementation
{
	public class EscalationService : IEscalationService
	{
		private readonly ICaseRepository _caseRepo;
		private readonly IEscalationRepository _escRepo;

		public EscalationService(ICaseRepository caseRepo, IEscalationRepository escRepo)
		{
			_caseRepo = caseRepo;
			_escRepo = escRepo;
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
				Status = "Open",
				EscalationDate = DateTime.Now,
				EscalationLevel = 1
			};

			await _escRepo.CreateEscalationAsync(escalation);

			// Reassign
			caseData.AssignedOfficerId = newOfficerId;
			caseData.Status = "Reassigned";
			caseData.LastUpdated = DateTime.Now;

			await _caseRepo.UpdateAsync(caseData);

			return "Escalated successfully";
		}
	}

}
