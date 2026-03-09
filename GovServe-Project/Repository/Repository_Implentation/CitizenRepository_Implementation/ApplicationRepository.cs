using System;
using GovServe_Project.Data;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.Models;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.CitizenRepository_Implementation
{
	public class ApplicationRepository : IApplicationRepository
	{
		private readonly GovServe_ProjectContext _context;

		public ApplicationRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

	
		// Create Application
		public async Task CreateAsync(Application application)
		{
			await _context.Application.AddAsync(application);
			await _context.SaveChangesAsync();
		}


		// Get By Id
		public async Task<Application> GetByIdAsync(int ApplicationId)
		{
			return await _context.Application.FindAsync(ApplicationId);
		}

		// Get By UserId
		public async Task<List<Application>> GetByUserIdAsync(int userId)
		{
			return await _context.Application
				.Include(a=>a.Service)
			    .Where(a => a.UserId == userId)
				.ToListAsync();
		}

		// Delete Application
		public async Task DeleteAsync(Application ApplicationId)
		{
			_context.Application.Remove(ApplicationId);
			await _context.SaveChangesAsync();
		}

		//get assigned applications for officer

		public async Task<List<Case>> GetAssignedCases(int officerId)

		{

			return await _context.Case

				.Where(c => c.AssignedOfficerId == officerId
		&& c.Status == "Assigned")

				.ToListAsync();

		}

		public async Task<List<Case>> GetApprovedCases(int officerId)

		{

			return await _context.Case

			.Include(c => c.Application)

				.Where(c => c.AssignedOfficerId == officerId
		&& c.Status == "Approved")

				.ToListAsync();

		}

		public async Task<List<Case>> GetPendingCases(int officerId)

		{

			return await _context.Case

				.Include(c => c.Application)

				.Where(c => c.AssignedOfficerId == officerId
		&& c.Status == "Pending")

				.ToListAsync();

		}

		public async Task<List<Case>> GetRejectedCases(int officerId)

		{

			return await _context.Case

				.Include(c => c.Application)

				.Where(c => c.AssignedOfficerId == officerId
		&& c.Status == "Rejected")

				.ToListAsync();

		}

		public async Task<List<Case>> GetResubmittedCases(int officerId)

		{

			return await _context.Case

				.Include(c => c.Application)

				.Where(c => c.AssignedOfficerId == officerId
		&& c.Status == "Resubmitted")

				.ToListAsync();

		}

		public async Task<Case?> GetCaseById(int caseId)

		{

			return await _context.Case

				.Include(c => c.Application)

				.FirstOrDefaultAsync(c => c.CaseId == caseId);

		}

		public async Task UpdateCase(Case casedata)

		{

			_context.Case.Update(casedata);

			await _context.SaveChangesAsync();

		}

		//for officer to view application details

		public async Task<ApplicationDetails> GetApplicationDetails(int applicationId)

		{

			var data = await _context.Application


			.Where(a => a.ApplicationID == applicationId)

			.Select(a => new ApplicationDetails

			{

				ApplicationID = a.ApplicationID,

				//ApplicantName = a.CitizenDocuments.,

				ApplicationStatus = a.ApplicationStatus.ToString(),

				SubmittedDate = a.SubmittedDate

			})

			.FirstOrDefaultAsync();

			return data;

		}


	}
}
