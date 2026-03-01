using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Controllers.SupervisorController.cs
{
	[ApiController]
	[Route("api/[controller]")]
	public class EscalationController : ControllerBase
	{
		private readonly IEscalationService _service;

		public EscalationController(IEscalationService service)
		{
			_service = service;
		}

		[HttpPost("escalate")]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> Escalate([FromBody] EscalateCaseDto dto)            //Tells json and convert it into DTO
		{
			return Ok(await _service.EscalateCaseAsync(
				dto.CaseId,
				dto.NewOfficerId,
				dto.SupervisorId,
				dto.Reason
			));
		}

		[HttpPost("check-sla/{caseId}")]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> CheckSLA(int caseId)
		{
			var result = await _service.CheckSLAAndEscalateAsync(caseId);
			return Ok(result);
		}
		[HttpPost("auto-escalate")]
		public async Task<IActionResult> AutoEscalate()
		{
			var result = await _service.AutoEscalateAsync();
			return Ok(result);
		}

		[HttpGet("count")]
		[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> Count()
		{
			return Ok(await _service.GetEscalationCountAsync());
		}
	}
}
