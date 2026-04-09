using GovServe_Project.Data;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
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

            return await _context.Case

            .Include(c => c.Application)

               .ThenInclude(a => a.Service)  // ✅ IMPORTANT        
               .Include(c => c.Department)

                .Include(c => c.AssignedOfficer)

               .ThenInclude(o => o.Department)

               .ToListAsync();

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

            .Where(c => c.AssignedOfficerId == officerId && c.Status != "Completed")

            .CountAsync();

        }

        public async Task<Case> GetCaseWithApplication(int caseId)

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
    public async Task<List<SLARecords>> GetSLABreachedCasesAsync()

    {

      return await _context.SLARecords

           .Include(s => s.Case)
    
      .Where(s=>s.Status.ToString()=="Breached")
    
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

                    // ✅ SINGLE SOURCE OF TRUTH
                    ActiveCases = _context.Case.Count(c =>
                        c.AssignedOfficerId == u.UserId &&
                        c.Status != "Completed" &&
                        c.Status != "Rejected" &&
                        c.Status != "Escalated")
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

    .CountAsync(c => c.Status == "Approved" || c.Status == "Completed"),


        EscalatedCases = await _context.Case

    .CountAsync(c => c.Status == "Escalated")

    };


    return stats;

}


//officers work// Get assigned cases
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

    .Include(c => c.User)

    .Include(c => c.Department)

    .Where(c => c.AssignedOfficerId == officerId && c.Status == "Assigned")

    .AsNoTracking() // Read-only query sathi he best ahe        
    .ToListAsync();


}


public async Task<Case?> GetCaseByIdAsync(int caseId)

{

    return await _context.Case

    .Include(c => c.Application)

    .Include(c => c.User)

    .Include(c => c.Department)

    .AsNoTracking() // Read-only query sathi he best ahe       
   .FirstOrDefaultAsync(c => c.CaseId == caseId);


}


public async Task<string> ApproveCaseAsync(int caseId)

{

    var caseEntity = await _context.Case

    .Include(c => c.Application)

    .FirstOrDefaultAsync(c => c.CaseId == caseId);


    if (caseEntity == null) return "Case not found";


    caseEntity.Status = "Approved";

    caseEntity.CompletedDate = DateTime.UtcNow;

    caseEntity.LastUpdated = DateTime.UtcNow;


    if (caseEntity.Application != null)

    {

        caseEntity.Application.ApplicationStatus = "Approved";

        caseEntity.Application.CompletedDate = DateTime.UtcNow;

    }


    await _context.SaveChangesAsync();

    return "Case approved successfully";

}


public async Task<string> RejectCaseAsync(int caseId, string reason)

{

    var caseEntity = await _context.Case

    .Include(c => c.Application)

    .FirstOrDefaultAsync(c => c.CaseId == caseId);


    if (caseEntity == null) return "Case not found";


    caseEntity.Status = "Rejected";

    caseEntity.RejectionReason = reason;

    caseEntity.CompletedDate = DateTime.UtcNow;

    caseEntity.LastUpdated = DateTime.UtcNow;


    if (caseEntity.Application != null)

    {

        caseEntity.Application.ApplicationStatus = "Rejected";

        caseEntity.Application.CompletedDate = DateTime.UtcNow;

    }


    await _context.SaveChangesAsync();

    return "Case rejected successfully";

}


public async Task<IEnumerable<Case>> GetResubmittedCasesAsync(int officerId) => await _context.Case

.Include(c => c.Application)

.Include(c => c.Department) // Department chi mahiti ghenyasathi        
.Where(c => c.AssignedOfficerId == officerId &&

c.Status == "Resubmitted")

.AsNoTracking() // Read-only query sathi he best ahe        
 .ToListAsync();




public async Task<Case> GetCaseById(int caseId) => await _context.Case

.Include(c => c.Application)

.FirstOrDefaultAsync(c => c.CaseId == caseId);


public async Task UpdateCase(Case caseObj)

{

    _context.Case.Update(caseObj);

    await _context.SaveChangesAsync();

}


public async Task<object> GetOfficerDashboardAsync(int officerId)

{

      // Optimized: No .Include needed for simple status counts
      var cases = await _context.Case

   .Where(c => c.AssignedOfficerId == officerId)

     .Select(c => new { c.Status })

     .ToListAsync();


    return new
    {

        AssignedCount = cases.Count(c => c.Status == "Assigned"),

        PendingVerificationCount = cases.Count(c => c.Status == "Under Verification"),

        ApprovedCount = cases.Count(c => c.Status == "Approved"),

        RejectedCount = cases.Count(c => c.Status == "Rejected"),

    };

}

		public async Task<IEnumerable<OfficerCaseDashboardDTO>> GetOfficerDetailedDashboardAsync(int officerId)
		{
			var today = DateTime.UtcNow;

			var query = from c in _context.Case
						join sla in _context.SLARecords on c.CaseId equals sla.CaseId
						join app in _context.Application on c.ApplicationID equals app.ApplicationID
						join svc in _context.Services on app.ServiceID equals svc.ServiceID  
						where c.AssignedOfficerId == officerId && c.Status != "Completed"
						select new OfficerCaseDashboardDTO
						{
							CaseId = c.CaseId,
							ApplicationName = svc.ServiceName,  
							Status = c.Status,
							DeadlineDate = sla.EndDate,
							DaysRemaining = (sla.EndDate - today).Days
						};

			return await query.ToListAsync();
		}


		public async Task<IEnumerable<Case>> GetCasesByStatusAsync(int officerId, string status)

{

    return await _context.Case

    .Include(c => c.Application)

    .Include(c => c.User)

    .Where(c => c.AssignedOfficerId == officerId && c.Application != null && c.Application.ApplicationStatus == status)

                    .AsNoTracking()

                    .ToListAsync();

}

	}

}