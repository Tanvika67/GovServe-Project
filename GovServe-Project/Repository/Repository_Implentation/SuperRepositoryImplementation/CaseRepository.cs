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
			return await _context.Case.FirstOrDefaultAsync(c => c.CaseId == id);
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
		//public async Task<List<Case>> GetAssignedCases(int AssignedOfficerId)
		//{
		//	return await _context.Case
		//		.Include(c => c.Application)
		//		//.Include(a => a.CitizenDocuments) // FIX: Documents link karnyathi
		//		.Include(c => c.User)
		//		.Where(c => c.AssignedOfficerId == AssignedOfficerId)
		//		.ToListAsync();
		//}

		// Get single case

		//	public async Task<Case?> GetCaseById(int caseId)

		//	{
		//		return await _context.Case
		//		.Include(c => c.Application)
		//			.FirstOrDefaultAsync(c => c.CaseId == caseId);

		//	}


		//	// Update case
		//	public async Task UpdateCase(Case caseObj)
		//	{
		//		_context.Case.Update(caseObj);
		//		await _context.SaveChangesAsync();
		//	}

		//	//count application for officer dashboard

		//	public async Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId)
		//	{
		//		var result = new DashboardCountcs();


		//		// Assigned Cases
		//		result.Assigned = await _context.Case
		//			.CountAsync(a => a.DepartmentID == departmentId
		//&& a.Status == "Assigned");


		//		// Approved Cases
		//		result.Approved = await _context.Case
		//	.CountAsync(a => a.DepartmentID == departmentId && a.Status == "Completed");


		//		// Pending Cases
		//		result.Pending = await _context.Case
		//			.CountAsync(a => a.DepartmentID == departmentId
		//&& a.Status == "Pending");


		//		// Rejected Cases
		//		result.Rejected = await _context.Case
		//			.CountAsync(a => a.DepartmentID == departmentId
		//&& a.Status == "Rejected");


		//		// Resubmitted Cases
		//		result.Resubmitted = await _context.Case
		//			.CountAsync(a => a.DepartmentID == departmentId
		//&& a.Status == "Resubmitted");


		//		return result;


		//	}

		//	public async Task<string> Reject(int caseId, string reason)
		//	{
		//		var caseObj = await _context.Case.FindAsync(caseId);

		//		if (caseObj == null)
		//			return "Case Not Found";

		//		caseObj.Status = "Rejected";
		//		caseObj.RejectionReason = reason;

		//		await _context.SaveChangesAsync();

		//		return "Case Rejected Successfully";
		//	}
		//	public async Task<List<Case>> GetResubmittedCases(int AssignedOfficerId)

		//	{

		//		return await _context.Case

		//			.Include(c => c.Application)

		//			.Where(c => c.AssignedOfficerId == AssignedOfficerId
		//	&& c.Status == "Resubmitted")

		//			.ToListAsync();

		//	}





		public Task<List<Case>> GetSLABreachedCasesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<Users>> GetOfficersByDepartmentAsync(int departmentId)
		{
			throw new NotImplementedException();
		}

		public async Task<int> GetActiveCaseCountByOfficerAsync(int officerId)
		{
			return await _context.Case
				 .CountAsync(c => c.AssignedOfficerId == officerId && c.Status != "Completed");
		}



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

		public async Task<bool> ApproveCaseAsync(int caseId, string remarks)
		{
			var caseEntity = await _context.Case
				.Include(c => c.Application)
				.FirstOrDefaultAsync(c => c.CaseId == caseId);

			if (caseEntity == null) return false;

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
			return true;
		}

		public async Task<bool> RejectCaseAsync(int caseId, string reason)
		{
			var caseEntity = await _context.Case
				.Include(c => c.Application)
				.FirstOrDefaultAsync(c => c.CaseId == caseId);

			if (caseEntity == null) return false;

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
			return true;
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
	}
}