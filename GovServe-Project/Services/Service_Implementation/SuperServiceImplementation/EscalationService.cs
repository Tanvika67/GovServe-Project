using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Data;
using GovServe_Project.Repository.Repository_Implentation;
using GovServe_Project.Repository.Interface;

namespace GovServe_Project.Services.Service_Implementation.SuperServiceImplementation
{
	public class EscalationService : IEscalationService
	{
		private readonly IEscalationRepository _repo;
		private readonly ICaseRepository _caseRepo;
		private readonly INotificationService _notificationService;
		private readonly ISLARecordRepository _slaRepo;
		private readonly GovServe_ProjectContext _context;
		private readonly IUserRepository _userRepo;
		private readonly ICaseService _caseService;

		public EscalationService(IEscalationRepository repo,ICaseRepository caseRepo,INotificationService notificationService,ISLARecordRepository slaRepo, GovServe_ProjectContext context, IUserRepository userRepo, ICaseService caseService)
		{
			_repo = repo;
			_caseRepo = caseRepo;
			_notificationService = notificationService;
			_slaRepo = slaRepo; 
			_context= context;
			_userRepo = userRepo;
			_caseService = caseService;

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

				// Store old values
				int oldOfficerId = c.AssignedOfficerId;
				int citizenId = c.UserId;

				// Get new officer (least busy)
				int newOfficerId = await _caseService.GetAvailableOfficer(c.DepartmentID);

				// Update case
				c.Status = "Escalated";
				c.IsEscalated = true;
				c.AssignedOfficerId = newOfficerId;
				c.LastUpdated = DateTime.Now;

				_caseRepo.Update(c);
				await _caseRepo.SaveAsync();


				// 1. Notify New Officer
				await _notificationService.SendNotificationAsync(
					newOfficerId,
					$"New case {c.CaseId} assigned after SLA breach",
					c.CaseId,
					"Escalation"
				);


				// 2. Notify Old Officer
				await _notificationService.SendNotificationAsync(
					oldOfficerId,
					$"Case {c.CaseId} escalated due to SLA breach",
					c.CaseId,
					"Escalation"
				);


				// 3. Notify Citizen
				await _notificationService.SendNotificationAsync(
					citizenId,
					$"Your case {c.CaseId} escalated to another officer due to delay",
					c.CaseId,
					"Escalation"
				);


				// 4. Notify Admin
				var adminId = await _userRepo.GetAdminIdAsync();

				await _notificationService.SendNotificationAsync(
					adminId,
					$"Case {c.CaseId} escalated and reassigned",
					c.CaseId,
					"Escalation"
				);


				// 5. Notify Grievance Officer (Optional but recommended)
				var grievanceOfficerId = await _userRepo.GetGrievanceOfficerIdAsync();

				await _notificationService.SendNotificationAsync(
					grievanceOfficerId,
					$"Case {c.CaseId} breached SLA. Please resolve within 1 day",
					c.CaseId,
					"SLA Breach"
				);

				return "Case escalated successfully";
			}

			return "SLA within limit";
		}

		public async Task<int> GetEscalationCountAsync()
		{
			return await _repo.GetEscalationCountAsync();
		}
	}
}
 