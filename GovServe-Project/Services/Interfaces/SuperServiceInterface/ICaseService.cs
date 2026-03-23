using GovServe_Project.Models.SuperModels;
using GovServe_Project.DTOs.SupervisorDTO;
﻿using GovServe_Project.Models;
using GovServe_Project.DTOs.OfficerDTO;

namespace GovServe_Project.Services.Interfaces
{
	public interface ICaseService
	{
		Task<IEnumerable<Case>> GetAllCasesAsync();
		Task<IEnumerable<Case>> GetActiveCasesAsync();
		Task<List<Case>> GetSLABreachedCasesAsync();
		Task<object> GetDashboardAsync();
		Task<string> ReassignCaseAsync();
	    Task<string> ReassignEscalatedCaseAsync(int caseId, int newOfficerId);
		Task<string> UpdateCaseStatus(int caseId, string status);
		Task<List<Case>> ViewAssignedCases(int officerId);
        
        Task<string> OpenCase(int caseId);
		Task<string> ApproveCase(int caseId);
		Task<string> Reject(int caseId, string reason);
		Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId);


	}
}