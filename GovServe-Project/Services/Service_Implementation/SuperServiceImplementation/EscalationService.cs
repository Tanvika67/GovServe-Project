using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Data;

namespace GovServe_Project.Services.Service_Implementation.SuperServiceImplementation
{
	public class EscalationService : IEscalationService
	{
		private readonly IEscalationRepository _repo;
		private readonly ICaseRepository _caseRepo;
		private readonly INotificationService _notificationService;
		private readonly ISLARecordRepository _slaRepo;
		private readonly GovServe_ProjectContext _context;

		public EscalationService(IEscalationRepository repo,ICaseRepository caseRepo,INotificationService notificationService,ISLARecordRepository slaRepo, GovServe_ProjectContext context)   
		{
			_repo = repo;
			_caseRepo = caseRepo;
			_notificationService = notificationService;
			_slaRepo = slaRepo; 
			_context= context;

		}

		public async Task SendSLAWarningsAsync()
		{
			var activeCases = await _caseRepo.GetActiveCasesAsync();

			foreach (var c in activeCases)
			{
				if (c.AssignedOfficerId == 0) continue; // safety check

				var sla = await _slaRepo.GetByCaseIdAsync(c.CaseId);
				if (sla == null) continue;

				var breachTime = sla.EndDate;
				var warningTime = breachTime.AddMinutes(-30);

				if (DateTime.Now >= warningTime && DateTime.Now < breachTime)
				{
					await _notificationService.SendNotificationAsync(
						c.AssignedOfficerId,
						"SLA is about to breach. Please take action",
						c.CaseId
					);
				}
			}
		}
		public async Task<string> AutoEscalateAsync()
		{
			var breachedCases = await _repo.GetSLABreachedCasesAsync();

			// fetch supervisor automatically
			var supervisor = await _context.User
				.Where(u => u.Role.RoleName == "Supervisor")
				.FirstOrDefaultAsync();

			if (supervisor == null)
				throw new Exception("Supervisor not found");

			foreach (var sla in breachedCases)
			{
				var c = await _caseRepo.GetByIdAsync(sla.CaseId);

				if (c == null || c.IsEscalated)
					continue;

				int oldOfficerId = c.AssignedOfficerId;
				int citizenId = c.UserId;

				var escalation = new Escalation
				{
					CaseId = c.CaseId,
					SupervisorId = supervisor.UserId,   // ADD THIS
					PreviousOfficerId = oldOfficerId,
					NewOfficerId = 0,
					Reason = "Auto escalation due to SLA breach",
					Status = "Open",
					EscalationDate = DateTime.Now,
					EscalationLevel = 1
				};

				await _repo.CreateAsync(escalation);

				c.Status = "Escalated";
				c.IsEscalated = true;
				c.LastUpdated = DateTime.Now;

				_caseRepo.Update(c);

				await _notificationService.SendNotificationAsync(
					citizenId,
					"Your application is delayed and escalated",
					c.CaseId
				);

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
 