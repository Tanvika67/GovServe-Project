using GovServe_Project.Data;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.SuperRepositoryImplementation
{
	public class CaseRepository : ICaseRepository
	{
		private readonly GovServe_ProjectContext _context;

		public CaseRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Case>> GetAllAsync()
		{
			return await _context.Case.ToListAsync();
		}

		public async Task<IEnumerable<Case>> GetByStatusAsync(string status)
		{
			return await _context.Case
				.Where(x => x.Status == status)
				.ToListAsync();
		}
		public async Task<List<Case>> GetActiveCasesAsync()
		{
			return await _context.Case
				.Where(c => c.Status != "Closed") // or your logic
				.ToListAsync();
		}
		public async Task<Case> GetByIdAsync(int id)
		{
			return await _context.Case.FirstOrDefaultAsync(c => c.CaseId == id);
		}
		public async Task<int> GetCaseCountByOfficerAsync(int officerId)
		{
			return await _context.Case
				.CountAsync(c => c.AssignedOfficerId == officerId && c.Status != "Completed");
		}
		public async Task<Case> GetCaseWithDocuments(int caseId)
		{
			return await _context.Case
				.Include(c => c.Application)
				.ThenInclude(a => a.CitizenDocuments)
				.FirstOrDefaultAsync(c => c.CaseId == caseId);
		}
		public async Task AddAsync(Case c)
		{
			await _context.Case.AddAsync(c);
		}
		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
		public void Update(Case c)
		{
			_context.Case.Update(c);
		}
		// It calculates the SLA status of the case from slarecords
		public async Task<List<Case>> GetSLABreachedCasesAsync()
		{
			return await _context.Case
				.Where(c => c.Status == "Escalated")
				.ToListAsync();
		}
		// For supervisor dashboard I need this
		public async Task<List<OfficerStatisticsDto>> GetOfficerStatisticsAsync()
		{
			return await _context.User
				.Where(u => u.Role.RoleName == "Officer")
				.Select(u => new OfficerStatisticsDto
				{
					OfficerId = u.UserId,
					OfficerName = u.FullName,
					Department = u.Department.DepartmentName,

					TotalCases = _context.Case
						.Count(c => c.AssignedOfficerId == u.UserId),

					ActiveCases = _context.Case
						.Count(c => c.AssignedOfficerId == u.UserId && c.Status == "Assigned"),

					PendingCases = _context.Case
						.Count(c => c.AssignedOfficerId == u.UserId && c.Status == "Pending"),

					CompletedCases = _context.Case
						.Count(c => c.AssignedOfficerId == u.UserId && c.Status == "Completed"),

					EscalatedCases = _context.Case
						.Count(c => c.AssignedOfficerId == u.UserId && c.Status == "Escalated")
				})
				.ToListAsync();
		}

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()

        {

            var stats = new DashboardStatsDto
            {

                TotalCases = await _context.Case.CountAsync(),


                PendingCases = await _context.Case

            .CountAsync(c => c.Status == "Pending"),


                AssignedCases = await _context.Case

            .CountAsync(c => c.Status == "Assigned"),


                CompletedCases = await _context.Case

            .CountAsync(c => c.Status == "Completed"),


                EscalatedCases = await _context.Case

            .CountAsync(c => c.Status == "Escalated")

            };


            return stats;

        }

        //officers work
        // Get assigned cases
        public async Task<List<Case>> GetAssignedCases(int officerId)
		{
			throw new NotImplementedException();
		}

		public async Task<int> GetActiveCaseCountByOfficerAsync(int officerId)
		{
			return await _context.Case
				 .CountAsync(c => c.AssignedOfficerId == officerId && c.Status != "Completed");
		}

		//New Code for officer dashboard

		public async Task<IEnumerable<Case>> GetAssignedCasesAsync(int officerId)
		{
			return await _context.Case
				.Include(c => c.Application)
				.Include(c => c.Department)
				.Where(c => c.AssignedOfficerId == officerId)
				.ToListAsync();
		}

		public async Task<Case?> GetCaseByIdAsync(int caseId)
		{
			return await _context.Case
				.Include(c => c.Application)
				.Include(c => c.Department)
				.FirstOrDefaultAsync(c => c.CaseId == caseId);
		}

		public async Task<string> ApproveCaseAsync(int caseId)
		{
			var caseEntity = await _context.Case
				.Include(c => c.Application)
				.FirstOrDefaultAsync(c => c.CaseId == caseId);

			if (caseEntity == null) return "Approved";

			// Case update
			caseEntity.Status = "Completed";
			caseEntity.CompletedDate = DateTime.Now;
			caseEntity.LastUpdated = DateTime.Now;

			// Application update
			if (caseEntity.Application != null)
			{
				caseEntity.Application.ApplicationStatus = "Approved";
				caseEntity.Application.CompletedDate = DateTime.Now;
			}

			await _context.SaveChangesAsync();
			return "Case approved successfully";
		}

		public async Task<string> RejectCaseAsync(int caseId, string reason)
		{
			var caseEntity = await _context.Case
				.Include(c => c.Application)
				.FirstOrDefaultAsync(c => c.CaseId == caseId);

			if (caseEntity == null) return "Rejected";

			// Case update
			caseEntity.Status = "Completed";
			caseEntity.RejectionReason = reason;
			caseEntity.CompletedDate = DateTime.Now;
			caseEntity.LastUpdated = DateTime.Now;

			// Application update
			if (caseEntity.Application != null)
			{
				caseEntity.Application.ApplicationStatus = "Rejected";
				caseEntity.Application.CompletedDate = DateTime.Now;
			}

			await _context.SaveChangesAsync();
			return "Case rejected successfully";
		}

		public async Task<IEnumerable<Case>> GetResubmittedCasesAsync(int officerId)
		{
			return await _context.Case
				.Include(c => c.Application)
				.Where(c => c.AssignedOfficerId == officerId && c.Application.ApplicationStatus == "Resubmitted")
				.ToListAsync();
		}

		public async Task<object> GetOfficerDashboardAsync(int officerId)
		{
			var cases = await _context.Case
				.Include(c => c.Application)
				.Where(c => c.AssignedOfficerId == officerId)
				.ToListAsync();

			return new
			{
				PendingCount = cases.Count(c => c.Status == "Pending"),
				AssignedCount = cases.Count(c => c.Status == "Assigned"),
				CompletedCount = cases.Count(c => c.Status == "Completed"),
				RejectedCount = cases.Count(c => c.Application.ApplicationStatus == "Rejected"),
				ApprovedCount = cases.Count(c => c.Application.ApplicationStatus == "Approved")
			};
		}

		public async Task<Case> GetCaseById(int caseId)
		{
			return await _context.Case

			.Include(c => c.Application) 
			.FirstOrDefaultAsync(c => c.CaseId == caseId);
		}

		public async Task UpdateCase(Case caseObj)
		{
			_context.Case.Update(caseObj);
			await _context.SaveChangesAsync();
		}

		// Implemented missing interface method: GetDashboardCountsAsync
		public async Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId)
		{
			var cases = await _context.Case
				.Include(c => c.Application)
				.Where(c => c.DepartmentID == departmentId)
				.ToListAsync();

			var result = new DashboardCountcs
			{
				Assigned = cases.Count(c => c.Status == "Assigned"),
				Approved = cases.Count(c => c.Application != null && c.Application.ApplicationStatus == "Approved"),
				Pending = cases.Count(c => c.Status == "Pending"),
				Rejected = cases.Count(c => c.Application != null && c.Application.ApplicationStatus == "Rejected"),
				Resubmitted = cases.Count(c => c.Application != null && c.Application.ApplicationStatus == "Resubmitted")
			};

			return result;
		}
	}
}