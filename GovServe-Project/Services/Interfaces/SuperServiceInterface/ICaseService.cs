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
		Task<string> CreateCaseAsync(CreateCaseDto dto);
		Task<string> ReassignCaseAsync();
	    Task<string> ReassignEscalatedCaseAsync(int caseId, int newOfficerId);
		Task<string> UpdateCaseStatus(int caseId, string status);


		//Task<List<Case>> ViewAssignedCases(int AssignedOfficerId);
		//Task<string> ApproveCase(int caseId);
		//Task<string> Reject(int caseId, string reason);
		//Task<List<Case>> GetResubmittedCases(int AssignedOfficerId);
		//Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId);
		//Task OpenCase(int caseId);


		Task<IEnumerable<Case>> GetAssignedCasesAsync(int officerId);
		Task<Case?> GetCaseByIdAsync(int caseId);
		Task<bool> ApproveCaseAsync(int caseId, string remarks);
		Task<bool> RejectCaseAsync(int caseId, string reason);
		Task<IEnumerable<Case>> GetResubmittedCasesAsync(int officerId);
		Task<object> GetOfficerDashboardAsync(int officerId);



	}
}