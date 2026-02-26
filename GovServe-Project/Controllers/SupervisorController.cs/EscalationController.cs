using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;

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
		public async Task<IActionResult> Escalate([FromBody] EscalateCaseDto dto)            //Tells json and convert it into DTO
		{
			return Ok(await _service.EscalateCaseAsync(
				dto.CaseId,
				dto.NewOfficerId,
				dto.SupervisorId,
				dto.Reason
			));
		}

		[HttpGet("count")]
		public async Task<IActionResult> Count()
		{
			return Ok(await _service.GetEscalationCountAsync());
		}
	}
}
