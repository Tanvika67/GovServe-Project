using GovServe_Project.Models.SuperModels;
using GovServe_Project.DTOs.SupervisorDTO;
﻿using GovServe_Project.Models;
using GovServe_Project.DTOs.OfficerDTO;

namespace GovServe_Project.Services.Interfaces
{
	public interface ICaseService
	{
		Task<string> CreateCaseAsync(CreateCaseDto dto);
		Task<int> GetAvailableOfficer(int departmentID);
		Task<List<CaseDto>> GetAllCasesAsync();
		Task<CaseDetailsDto> GetCaseDetails(int caseId);
		Task<IEnumerable<Case>> GetActiveCasesAsync();
		Task<List<Case>> GetSLABreachedCasesAsync();
		Task<object> GetDashboardAsync();
		Task<string> ReassignCaseAsync();
	    Task<string> ReassignEscalatedCaseAsync(int caseId, int newOfficerId);
		Task<List<OfficerStatisticsDto>> GetOfficerStatisticsAsync();
		Task<DashboardStatsDto> GetDashboardStatsAsync();
		Task<string> UpdateCaseStatus(int caseId, string status);

		Task<IEnumerable<Case>> GetAssignedCasesAsync(int officerId);
		Task<Case?> GetCaseByIdAsync(int caseId);
		Task<string> ApproveCaseAsync(int caseId);
		Task<string> RejectCaseAsync(int caseId, string reason);
		Task<IEnumerable<Case>> GetResubmittedCasesAsync(int officerId);
		Task<object> GetOfficerDashboardAsync(int officerId);



	}
}