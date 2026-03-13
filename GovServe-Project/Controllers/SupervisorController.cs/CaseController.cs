using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;
using Microsoft.AspNetCore.Mvc;
using GovServe_Project.DTOs.OfficerDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;


namespace GovServe_Project.Controllers.SupervisorController.cs
{
	[EnableCors]
	[ApiController]
	[Route("api/[controller]")]
	//[Authorize(Roles = "Supervisor")]

	public class CaseController : ControllerBase
	{
		private readonly ICaseService _service;

		public CaseController(ICaseService service)
		{
			_service = service;
		}

		//POST only I can create a case; API will create a new case in the system
		[HttpPost]
		[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> CreateCase(CreateCaseDto dto)
		{
			var result = await _service.CreateCaseAsync(dto);
			return Ok(result);
		}

		//GET only I can see all cases
		//Fetches complete list of case from the database
		[HttpGet("all")]
		//[Authorize(Roles = "Supervisor")]              

		public async Task<IActionResult> GetAll()
		{
			return Ok(await _service.GetAllCasesAsync());
		}

		//Returns only ongoing cases
		[HttpGet("active")]
		//[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> GetActive()
		{
			return Ok(await _service.GetActiveCasesAsync());
		}

		//Used to change status like: Assigned-->Inprogress-->Completed
		[HttpPut("update-status")]
		//[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto dto)
		{
			var result = await _service.UpdateCaseStatus(dto.CaseId, dto.Status);
			return Ok(result);
		}

		//Returns cases where SLA time has already exceeded
		//I can easily identify delayed cases
		[HttpGet("sla-breached")]
		//[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> GetSLABreached()
		{
			var result = await _service.GetSLABreachedCasesAsync();
			return Ok(result);
		}
		//GET Returns summary like Total cases;Active cases; SLA breached
        [HttpGet("dashboard")]
        //[Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Dashboard()
		{
			return Ok(await _service.GetDashboardAsync());
		}

		//POST to reassign a case to another officer
		//I can easily reassign a case to another officer if needed
		[HttpPost("reassign-escalated")]
		//[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> ReassignEscalated(int caseId, int newOfficerId)
		{
			var result = await _service.ReassignEscalatedCaseAsync(caseId, newOfficerId);
			return Ok(result);
		}

		//Supervisor,Officer,Grieviance needs this so I created this
		[HttpGet("case-details/{caseId}")]
		public async Task<IActionResult> GetCaseDetails(int caseId)
		{
			var result = await _service.GetCaseDetails(caseId);
			return Ok(result);
		}
		//For my dashboard I created this
		[HttpGet("officer-statistics")]
		public async Task<IActionResult> GetOfficerStatistics()
		//Returns summary like Total cases;Active cases;SLA breached
		[HttpGet("dashboard")]
		//[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> Dashboard()
		{
			var result = await _service.GetOfficerStatisticsAsync();
			return Ok(result);
		}

		//	officer work

		////  GET - View assigned cases
		//[HttpGet("Assigned/{AssignedOfficer}")]
		////[Authorize(Roles = "Officer")]
		//public async Task<IActionResult> GetAssignedCases(int AssignedOfficer)
		//{
		//	var cases = await _service.ViewAssignedCases(AssignedOfficer);
		//	return Ok(cases);
		//}

		////  PUT - Open case (InProgress)
		//[HttpPut("Open/{caseId}")]
		////[Authorize(Roles = "Officer")]
		//public async Task<IActionResult> OpenCase(int caseId)
		//{
		//	await _service.OpenCase(caseId);
		//	return Ok("Case opened succesfully");


		//}


		//	//  PUT - Approve case
		//	[HttpPut("Approve/{caseId}")]
		////[Authorize(Roles = "Officer")]
		//public async Task<IActionResult> ApproveCase(int caseId)
		//{
		//	var result = await _service.ApproveCase(caseId);
		//	return Ok(result);
		//}

		////  PUT - Reject case//used for notification also
		//[HttpPut("Reject/{caseId}")]
		////[Authorize(Roles = "Officer")]
		//public async Task<IActionResult> Reject(int caseId, [FromBody] string reason)
		//{
		//	var result = await _service.Reject(caseId, reason);
		//	return Ok(result);
		//}








		//		[HttpGet("Resubmitted/{AssignedOfficerId}")]
		////[Authorize(Roles = "Officer")]
		//public async Task<IActionResult> GetResubmitted(int AssignedOfficerId)
		//{
		//	var result = await _service.GetResubmittedCases(AssignedOfficerId);
		//	return Ok(result);

		//}

		////for getting application count on dashboard
		//[HttpGet("Dashboard/{departmentId}")]
		////[Authorize(Roles = "Officer")]
		//public async Task<IActionResult> DashboardCounts(int departmentId)
		//{
		//	var result = await _service.GetDashboardCountsAsync(departmentId);
		//	return Ok(result);
		//}




		[HttpGet("assigned/{officerId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetAssignedCases(int officerId)
		{
			var cases = await _service.GetAssignedCasesAsync(officerId);

			if (cases == null || !cases.Any())
				return NotFound(new { message = "No cases assigned to this officer." });

			return Ok(cases);
		}

		// Officer: View details of a specific case
		[HttpGet("{caseId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> ViewCase(int caseId)
		{
			var caseDetails = await _service.GetCaseByIdAsync(caseId);

			if (caseDetails == null)
				return NotFound(new { message = "Case not found." });

			return Ok(caseDetails);
		}

		// Officer: Approve a case
		[HttpPut("{caseId}/approve")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> ApproveCase(int caseId, [FromBody] string remarks)
		{
			var result = await _service.ApproveCaseAsync(caseId, remarks);

			if (!result)
				return BadRequest(new { message = "Unable to approve case." });

			return Ok(new { message = "Case approved successfully." });
		}

		// Officer: Reject a case
		[HttpPut("{caseId}/reject")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> RejectCase(int caseId, [FromBody] string reason)
		{
			var result = await _service.RejectCaseAsync(caseId, reason);

			if (!result)
				return BadRequest(new { message = "Unable to reject case." });

			return Ok(new { message = "Case rejected successfully." });
		}

		// Officer: Get resubmitted applications
		[HttpGet("resubmitted/{officerId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetResubmittedCases(int officerId)
		{
			var cases = await _service.GetResubmittedCasesAsync(officerId);

			if (cases == null || !cases.Any())
				return NotFound(new { message = "No resubmitted cases found." });

			return Ok(cases);
		}

		// Officer: Dashboard summary (counts)
		[HttpGet("dashboard/{officerId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> OfficerDashboard(int officerId)
		{
			var summary = await _service.GetOfficerDashboardAsync(officerId);

			return Ok(summary);
		}

	}



	






	}

