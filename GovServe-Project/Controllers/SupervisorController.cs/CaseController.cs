using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;
using Microsoft.AspNetCore.Mvc;
using GovServe_Project.DTOs.OfficerDTO;
using Microsoft.AspNetCore.Authorization;


namespace GovServe_Project.Controllers.SupervisorController.cs
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "Supervisor")]

	public class CaseController : ControllerBase
	{
		private readonly ICaseService _service;

		public CaseController(ICaseService service)
		{
			_service = service;
		}

		[HttpPost]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> CreateCase(CreateCaseDto dto)
		{
			var result = await _service.CreateCaseAsync(dto);
			return Ok(result);
		}

		[HttpGet("all")]
		[Authorize(Roles = "Supervisor")]               //admin also needs

		public async Task<IActionResult> GetAll()
		{
			return Ok(await _service.GetAllCasesAsync());
		}

		[HttpGet("active")]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> GetActive()
		{
			return Ok(await _service.GetActiveCasesAsync());
		}
		[HttpPut("update-status")]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto dto)
		{
			var result = await _service.UpdateCaseStatus(dto.CaseId, dto.Status);
			return Ok(result);
		}
		[HttpGet("sla-breached")]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> GetSLABreached()
		{
			var result = await _service.GetSLABreachedCasesAsync();
			return Ok(result);
		}
		[HttpPost("reassign-escalated")]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> ReassignEscalated(int caseId, int newOfficerId)
		{
			var result = await _service.ReassignEscalatedCaseAsync(caseId, newOfficerId);
			return Ok(result);
		}

		[HttpGet("dashboard")]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> Dashboard()
		{
			return Ok(await _service.GetDashboardAsync());
		}

		//officer work

		//  GET - View assigned cases
		[HttpGet("assigned/{officerId}")]
		public async Task<IActionResult> GetAssignedCases(int officerId)
		{
			var cases = await _service.ViewAssignedCases(officerId);
			return Ok(cases);
		}

		//  PUT - Open case (InProgress)
		[HttpPut("open/{caseId}")]
		public async Task<IActionResult> OpenCase(int caseId)
		{
			var result = await _service.OpenCase(caseId);
			return Ok(result);
		}

		//  PUT - Approve case
		[HttpPut("approve/{caseId}")]
		public async Task<IActionResult> ApproveCase(int caseId)
		{
			var result = await _service.ApproveCase(caseId);
			return Ok(result);
		}

		//  PUT - Reject case//used for notification also
		[HttpPut("reject/{caseId}")]
		public async Task<IActionResult> Reject(int caseId, [FromBody] string reason)
		{
			var result = await _service.Reject(caseId, reason);
			return Ok(result);
		}

		//for getting application count on dashboard
		[HttpGet("dashboard/{departmentId}")]
		public async Task<IActionResult> DashboardCounts(int departmentId)
		{
			var result = await _service.GetDashboardCountsAsync(departmentId);
			return Ok(result);
		}

	}
}