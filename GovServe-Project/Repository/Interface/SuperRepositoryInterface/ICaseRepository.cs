using GovServe_Project.DTOs.OfficerDTO;
﻿using GovServe_Project.Models;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Repository_Implentation;


namespace GovServe_Project.Repository.Interface.SuperRepositoryInterface
{
	public interface ICaseRepository
	{
		Task<IEnumerable<Case>> GetAllAsync();
		Task<IEnumerable<Case>> GetByStatusAsync(string status);
		Task<Case> GetByIdAsync(int id);
		Task<List<Case>> GetActiveCasesAsync();
		Task AddAsync(Case c);
		void Update(Case c);

		Task SaveAsync();

		Task<List<Case>> GetSLABreachedCasesAsync();

		//officer work
		//Task<List<Case>> GetAssignedCases(int officerId);
		//Task<Case?> GetCaseById(int caseId);

		//Task UpdateCase(Case caseObj);
		//Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId);

		//Task<string> Reject(int caseId, string reason);






		//Task<List<Case>> GetResubmittedCases(int AssignedOfficerId);

		Task<IEnumerable<Case>> GetAssignedCasesAsync(int officerId);
		Task<Case?> GetCaseByIdAsync(int caseId);
		Task<bool> ApproveCaseAsync(int caseId, string remarks);
		Task<bool> RejectCaseAsync(int caseId, string reason);
		Task<IEnumerable<Case>> GetResubmittedCasesAsync(int officerId);
		Task<object> GetOfficerDashboardAsync(int officerId);

	}
}



