using Microsoft.EntityFrameworkCore;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Enum;
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using GovServe_Project.Repository.Interface;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.Models;

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
			return await _context.Case.FindAsync(id);
		}
		public async Task<int> GetCaseCountByOfficerAsync(int officerId)
		{
			return await _context.Case
				.CountAsync(c => c.AssignedOfficerId == officerId && c.Status != "Completed");
		}

		public async Task AddAsync(Case c)
		{
			await _context.Case.AddAsync(c);
		}

		public void Update(Case c)
		{
			_context.Case.Update(c);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		//officers work

		// Get assigned cases
		public async Task<List<Case>> GetAssignedCases(int officerId)
		{
			return await _context.Case
				.Where(c => c.AssignedOfficerId == officerId)
				.ToListAsync();
		}

		// Get single case

		public async Task<Case?> GetCaseById(int caseId)
		{
			return await _context.Case.FindAsync(caseId);
		}

		// Update case
		public async Task UpdateCase(Case caseObj)
		{
			_context.Case.Update(caseObj);
			await _context.SaveChangesAsync();
		}

		//count application for officer dashboard

		public async Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId)
		{
			var result = new DashboardCountcs();

			// Assigned Cases
			result.Assigned = await _context.Case
				.CountAsync(a => a.DepartmentID == departmentId
	&& a.Status == "Assigned");


			// Approved Cases
			result.Approved = await _context.Case
				.CountAsync(a => a.DepartmentID == departmentId
	&& a.Status == "Approved");


			// Pending Cases
			result.Pending = await _context.Case
				.CountAsync(a => a.DepartmentID == departmentId
	&& a.Status == "Pending");


			// Rejected Cases
			result.Rejected = await _context.Case
				.CountAsync(a => a.DepartmentID == departmentId
	&& a.Status == "Rejected");


			// Resubmitted Cases
			result.Resubmitted = await _context.Case
				.CountAsync(a => a.DepartmentID == departmentId
	&& a.Status == "Resubmitted");


			return result;


		}
		//Notification purpose 
		public async Task<string> Reject(int caseId, string reason)
		{
			var caseObj = await _context.Case.FindAsync(caseId);

			if (caseObj == null)
				return "Case Not Found";

			caseObj.Status = "Rejected";
			caseObj.RejectionReason = reason;

			await _context.SaveChangesAsync();

			return "Case Rejected Successfully";
		}
		// It calculates the SLA Days
		public async Task<List<Case>> GetSLABreachedCasesAsync()
		{
			var slaLimit = DateTime.Now.AddDays(-2);  
			return await _context.Case
				.Where(c=>
				c.Status !="Completed" &&
				c.Status!="Escalated" &&
				c.AssignedDate<=slaLimit)
				.ToListAsync();
		}

        public Task<List<Users>> GetOfficersByDepartmentAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetActiveCaseCountByOfficerAsync(int officerId)
        {
            throw new NotImplementedException();
        }
    }
}