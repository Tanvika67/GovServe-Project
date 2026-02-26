using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers.SupervisorController.cs
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
		[HttpGet("sla-breached")]
		public async Task<IActionResult> GetSLABreached()
		{
			var result = await _service.GetSLABreachedCasesAsync();
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
	}
}