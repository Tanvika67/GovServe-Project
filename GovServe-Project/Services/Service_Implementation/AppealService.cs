using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	// Implementation of Appeal workflow logic
	public class AppealService : IAppealService
	{
		private readonly IAppealRepository _appealRepo;
		private readonly IGrievanceRepository _grievanceRepo;

		public AppealService(IAppealRepository appealRepo, IGrievanceRepository grievanceRepo)
		{
			_appealRepo = appealRepo;
			_grievanceRepo = grievanceRepo;
		}

		// Citizen files appeal
		public async Task<Appeal> FileAppealAsync(Appeal appeal)
		{
			appeal.AppealStatus = AppealStatus.Pending;
			appeal.AppealDate = DateTime.UtcNow;
			await _appealRepo.AddAsync(appeal);
			return appeal;
		}

		// Approve appeal → reopen linked grievance
		public async Task ApproveAppealAsync(int appealId)
		{
			var appeal = await _appealRepo.GetByIdAsync(appealId);
			appeal.AppealStatus = AppealStatus.Approved;
			await _appealRepo.UpdateAsync(appeal);

			var grievance = await _grievanceRepo.GetByIdAsync(appeal.GrievanceID);
			grievance.Status = GrievanceStatus.Reopened;
			await _grievanceRepo.UpdateAsync(grievance);
		}

		// Reject appeal → final closure
		public async Task RejectAppealAsync(int appealId)
		{
			var appeal = await _appealRepo.GetByIdAsync(appealId);
			appeal.AppealStatus = AppealStatus.Rejected;
			await _appealRepo.UpdateAsync(appeal);
		}
	}
}
