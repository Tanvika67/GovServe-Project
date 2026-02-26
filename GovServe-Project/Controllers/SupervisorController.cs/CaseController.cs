using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GovServe_Project.DTOs.OfficerDTO;

namespace GovServe_Project.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CaseController : ControllerBase
	{
		private readonly ICaseService _service;

		public CaseController(ICaseService service)
		{
			_service = service;
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] CreateCaseDto dto)
		{
			if (dto == null)
				return BadRequest("Invalid data");

			var result = await _service.CreateCaseAsync(dto);
			return Ok(result);
		}


		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _service.GetAllCasesAsync());
		}

		[HttpGet("pending")]
		public async Task<IActionResult> GetPending()
		{
			return Ok(await _service.GetPendingCasesAsync());
		}

		[HttpGet("active")]
		public async Task<IActionResult> GetActive()
		{
			return Ok(await _service.GetActiveCasesAsync());
		}

		[HttpGet("escalated")]
		public async Task<IActionResult> GetEscalated()
		{
			return Ok(await _service.GetEscalatedCasesAsync());
		}


		[HttpPost("assign")]
		public async Task<IActionResult> Assign(int caseId, int officerId, int officerDeptId)
		{
			var result = await _service.AssignCaseAsync(caseId, officerId, officerDeptId);
			return Ok(result);
		}

		[HttpPost("reassign")]
		public async Task<IActionResult> Reassign(int caseId, int newOfficerId)
		{
			var result = await _service.ReassignCaseAsync(caseId, newOfficerId);
			return Ok(result);
		}

		[HttpPost("auto-escalate")]
		public async Task<IActionResult> AutoEscalate()
		{
			var result = await _service.AutoEscalateAsync();
			return Ok(result);
		}

		[HttpGet("dashboard")]
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