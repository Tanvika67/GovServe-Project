using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.DTOs.SupervisorDTO;
﻿using GovServe_Project.Models;
using GovServe_Project.Models.SuperModels;


namespace GovServe_Project.Repository.Interface.SuperRepositoryInterface
{
	public interface ICaseRepository
	{
		Task<IEnumerable<Case>> GetAllAsync();
		Task<IEnumerable<Case>> GetByStatusAsync(string status);
		Task<Case> GetByIdAsync(int id);
		Task<List<Case>> GetActiveCasesAsync();
		Task<int> GetCaseCountByOfficerAsync(int officerId);
		Task<Case> GetCaseWithDocuments(int caseId);
		Task<List<OfficerStatisticsDto>> GetOfficerStatisticsAsync();
		Task<DashboardStatsDto> GetDashboardStatsAsync();
		Task AddAsync(Case c);
		void Update(Case c);
		Task SaveAsync();

		//officer work
		Task<List<Case>> GetAssignedCases(int officerId);
		Task<Case?> GetCaseById(int caseId);

		Task UpdateCase(Case caseObj);
		Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId);

		Task<string> Reject(int caseId, string reason);
		Task<List<Case>> GetSLABreachedCasesAsync();

		
	}
}



